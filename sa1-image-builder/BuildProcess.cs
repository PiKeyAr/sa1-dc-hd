using DiscUtils.Gdrom;
using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace GUIPatcher
{
    public partial class MainForm
    {
        // Step 1 - Convert track03 using bin2iso
        private void StartBin2Iso()
        {
            string filename;
            string workdir = Path.Combine(textBoxOutputPath.Text, "data");
            string args;
            RefreshProgress("Step 1/7: Running bin2iso on track 3");
            Directory.CreateDirectory(Path.Combine(textBoxOutputPath.Text, "data"));
            // bin2iso
            filename = "bin2iso.exe";
            args = "\"" + Path.Combine(textBoxOriginalPath.Text, "track03.bin") + "\" \"" + Path.Combine(textBoxOutputPath.Text, "track03.iso") + "\"";
            Process prc = CreateProcessInfo(filename, workdir, args, Bin2IsoExit);
        }

        // Step 2 - Extract track03
        private void Bin2IsoExit(object sender, System.EventArgs e)
        {
            string filename;
            string workdir = Path.Combine(textBoxOutputPath.Text, "data");
            string args;
            if (!File.Exists(Path.Combine(textBoxOutputPath.Text, "track03.iso")))
            {
                Console.WriteLine("Error converting BIN to ISO: destination file {0} doesn't exist", Path.Combine(textBoxOutputPath.Text, "track03.iso"));
                return;
            }
            // Extract
            RefreshProgress("Step 2/7: Extracting track03.iso");
            filename = "extract.exe";
            args = "\"" + Path.Combine(textBoxOutputPath.Text, "track03.iso") + "\"";
            CreateProcessInfo(filename, workdir, args, ApplyMods);
        }

        // Step 3 - Apply mods
        private void ApplyMods(object sender, System.EventArgs e)
        {
            // Check if 1ST_READ exists
            if (!File.Exists(Path.Combine(textBoxOutputPath.Text, "data", "1ST_READ.BIN")))
            {
                Console.WriteLine("Error: Extracted file {0} not found", Path.Combine(textBoxOutputPath.Text, "data", "1ST_READ.BIN"));
                return;
            }

            // Delete track03.iso
            if (File.Exists(Path.Combine(textBoxOutputPath.Text, "track03.iso")))
                File.Delete(Path.Combine(textBoxOutputPath.Text, "track03.iso"));
            RefreshProgress("Step 3/7: Patching files");
            bool errors = false;

            // Select active mods to apply
            List<Mod> applyMods = new List<Mod>();
            for (int i = 0; i < mods.Count; i++)
            {
                for (int u = 0; u < activeMods.Count; u++)
                    if (mods[i].Name == activeMods[u])
                        applyMods.Add(mods[i]);
            }

            // Extract PRS files that will be patched
            List<string> PRSFiles = new List<string>();
            foreach (Mod mod in applyMods)
            {
                // Make a list of PRS files changed by mods
                foreach (SectionData section in mod.PatchData.Sections)
                {
                    if (Path.GetExtension(section.SectionName.ToLowerInvariant()) == ".prs")
                        if (!PRSFiles.Contains(section.SectionName))
                        PRSFiles.Add(section.SectionName);
                }
            }
            // Extract all PRS files in the list
            Console.WriteLine("Extracting PRS files...");
            foreach (string prsFile in PRSFiles)
            {
                string prsFullPath = Path.Combine(Path.Combine(textBoxOutputPath.Text, "data"), prsFile);
                byte[] prsFileData = csharp_prs.Prs.Decompress(File.ReadAllBytes(prsFullPath));
                File.WriteAllBytes(Path.ChangeExtension(prsFullPath, ".bin"), prsFileData);
            }

            // Apply mods
            foreach (Mod mod in applyMods)
            {
                Console.WriteLine("Applying mods: {0}", mod.Name);
                bool result = InstallMod(Path.Combine(textBoxOutputPath.Text, "data"), mod);
                if (result == false)
                    errors = true;
            }
            if (errors)
            {
                Console.WriteLine("Error applying mods. Check the log and try again.");
                return;
            }

            // Recompress PRS files
            Console.WriteLine("Compressing PRS files...");
            foreach (string prsFile in PRSFiles)
            {
                string prsFullPath = Path.Combine(Path.Combine(textBoxOutputPath.Text, "data"), prsFile);
                byte[] binFileData = File.ReadAllBytes(Path.ChangeExtension(prsFullPath, ".bin"));
                byte[] compressedData = csharp_prs.Prs.Compress(ref binFileData, 255);
                File.WriteAllBytes(prsFullPath, compressedData);
                File.Delete(Path.ChangeExtension(prsFullPath, ".bin"));
            }

            // Proceed to the next step
            CopyOriginalDataTracks();
        }

        // Step 4 - Copy back original data tracks
        private void CopyOriginalDataTracks()
        {
            RefreshProgress("Step 4/7: Copying original data tracks");
            string[] originalFiles = Directory.GetFiles(textBoxOriginalPath.Text, "*.*", SearchOption.TopDirectoryOnly);
            for (int u = 0; u < originalFiles.Length; u++)
            {
                if (Path.GetFileNameWithoutExtension(originalFiles[u]).ToLowerInvariant() != "track03")
                    switch (Path.GetExtension(originalFiles[u].ToLowerInvariant()))
                    {
                        case ".gdi":
                        case ".bin":
                        case ".raw":
                        case ".iso":
                            string destpath = Path.Combine(textBoxOutputPath.Text, Path.GetFileName(originalFiles[u]));
                            if (File.Exists(destpath))
                                Console.WriteLine("File already exists: {0}", Path.GetFileName(originalFiles[u]));
                            else
                            {
                                Console.WriteLine("Copying file: {0}", Path.GetFileName(originalFiles[u]));
                                File.Copy(originalFiles[u], destpath, false);
                            }
                            break;
                        default:
                            Console.WriteLine("File not needed: {0}", Path.GetFileName(originalFiles[u]));
                            break;
                    }
            }
            BuildTrack03();
        }

        // Step 5 - Build track03.bin
        private void BuildTrack03()
        {
            string workdir = Path.Combine(textBoxOutputPath.Text);
            string dataDir = Path.Combine(workdir, "data");
            string ipBin = Path.Combine(currentDirAss, "utils", "ip.bin");
            string gdiOutput = workdir;
            RefreshProgress("Step 5/7: Building a modified image");
            GDromBuilder builder = new GDromBuilder();
            builder.ReportProgress += GDIProgressReport;
            builder.RawMode = true;
            builder.TruncateData = false;
            List<DiscTrack> tracks = null;
            tracks = builder.BuildGDROM(dataDir, ipBin, null, gdiOutput);
            Console.Write("Track details: " + builder.GetGDIText(tracks));
            Cleanup();
        }

        private static void GDIProgressReport(int amount)
        {
            if (amount % 10 == 0)
            {
                Console.WriteLine(amount.ToString() + "%");
            }
        }

        // Step 6 - Apply codes & cleanup
        private void Cleanup()
        {
            // Apply codes
            RefreshProgress("Step 6/7: Applying codes");
            for (int c = 0; c < 99; c++)
            {
                string keyName = "cheat" + c.ToString() + "_desc";
                if (!codes.Global.ContainsKey(keyName))
                    break;
                string codeDesc = codes.Global[keyName].Replace("\"", "");
                foreach (string code in activeCodes)
                    if (codeDesc == code)
                    {
                        codes.Global["cheat" + c.ToString() + "_enable"] = "\"true\"";
                        Console.WriteLine("Code: {0}", codeDesc);
                        continue;
                    }
            }
            var saveCodes = new FileIniDataParser();
            string codesOutPath = Path.Combine(textBoxOutputPath.Text, "SA1-DC-HD.cht");
            saveCodes.WriteFile(codesOutPath, codes, System.Text.Encoding.ASCII);

            // Cleanup
            RefreshProgress("Step 7/7: Cleaning up");
            string directoryPath = Path.Combine(textBoxOutputPath.Text, "data");
            int retries = 0;
            while (true)
                try
                {
                    Directory.Delete(directoryPath, true);
                    Console.WriteLine("Data folder deleted successfully.");
                    break;
                }
                catch (Exception ex)
                {
                    if (retries > 9)
                    {
                        Console.WriteLine("Could not delete data folder: {0}", ex.Message.ToString());
                        goto end;
                    }
                    retries++;
                    Console.WriteLine("Unable to delete data folder. Trying again (attempt {0} of 10)", retries.ToString());
                    Thread.Sleep(1000);
                }
            end:
            Console.WriteLine();
            Console.WriteLine("The modified image is located at: {0}", textBoxOutputPath.Text);
            Console.WriteLine("Cheat file: {0}", codesOutPath);

            // Finish
            Console.WriteLine("DONE!");
            RefreshProgress("Click Build to create a modified GDI image. Progress will be shown below.", false);
            EnableBuildButton();
        }
    }
}
