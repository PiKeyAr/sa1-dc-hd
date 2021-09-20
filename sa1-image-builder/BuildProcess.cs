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
        // Step 1 - Find GDI file and extract track03
        private void ExtractGDI()
        {
            // Check if destination folder exists
            if (!Directory.Exists(Path.Combine(textBoxOutputPath.Text)))
                Directory.CreateDirectory(Path.Combine(textBoxOutputPath.Text));
            string filename;
            string workdir_main = textBoxOutputPath.Text;
            string workdir_data = Path.Combine(textBoxOutputPath.Text, "data");
            string args;
            RefreshProgress("Step 1/6: Extracting original image");
            string[] gdis = Directory.GetFiles(textBoxOriginalPath.Text, "*.gdi");
            if (gdis.Length == 0)
            {
                Console.WriteLine("GDI file not found! Aborting.");
                return;
            }
            else if (gdis.Length > 1)
            {
                Console.WriteLine("Multiple GDI files found. Please use only one GDI file. Aborting.");
                return;
            }
            Directory.CreateDirectory(workdir_data);
            filename = "gditools.exe";
            args = "-i \"" + gdis[0] + "\"" +  " -o \"" + workdir_main + "\"" + " --extract-all -b ip.bin";
            Process prc = CreateProcessInfo(filename, textBoxOutputPath.Text, args, ApplyMods);
        }

        // Step 2 - Apply mods
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
            RefreshProgress("Step 2/6: Patching files");
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

        // Step 3 - Copy back original data tracks
        private void CopyOriginalDataTracks()
        {
            RefreshProgress("Step 3/6: Copying original data tracks");
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

        // Step 4 - Build track03.bin
        private void BuildTrack03()
        {
            string workdir = Path.Combine(textBoxOutputPath.Text);
            string dataDir = Path.Combine(workdir, "data");
            string ipBin = Path.Combine(workdir, "ip.bin");
            string gdiOutput = workdir;
            RefreshProgress("Step 4/6: Building a modified image");
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

        // Step 5 - Apply codes & cleanup
        private void Cleanup()
        {
            // Apply codes
            RefreshProgress("Step 5/6: Applying codes");
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

            // Step 6 - Cleanup
            RefreshProgress("Step 6/6: Cleaning up");
            string directoryPath = Path.Combine(textBoxOutputPath.Text, "data");
            int retries = 0;
            while (true)
                try
                {
                    File.Delete(Path.Combine(textBoxOutputPath.Text, "ip.bin"));
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
