using IniParser;
using IniParser.Model;
using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUIPatcher
{
    public partial class Form1 : Form
    {
        public class Mod
        {
            public string Name;
            public string Author;
            public string Version;
            public string Description;
            public string[] FileReplacements;
            public IniData PatchData;

            public Mod(string inifile)
            {
                var ini = new FileIniDataParser();
                PatchData = ini.ReadFile(inifile);
                Name = PatchData.Global["Name"];
                Author = PatchData.Global["Author"];
                Version = PatchData.Global["Version"];
                Description = PatchData.Global["Description"];
                string modpath = Path.GetDirectoryName(inifile);
                var files = Directory.GetFiles(Path.GetFullPath(modpath), "*.*", SearchOption.TopDirectoryOnly).Where(name => !name.EndsWith(".ini", StringComparison.OrdinalIgnoreCase));
                FileReplacements = files.ToArray();
            }
        }

        public IniData settings;
        public List<Mod> mods;
        public List<string> activeMods;
        public string currentDir;
        public TextBoxWriter writer;

        public Form1()
        {
            InitializeComponent();
            currentDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            activeMods = new List<string>();
            tabMods.Enabled = false; if (CheckSA1Version(textBoxOriginalPath.Text))
                tabBuild.Enabled = false;
            mods = new List<Mod>();
            if (File.Exists("settings.ini"))
                LoadSettings("settings.ini");
            else
                settings = new IniData();
            InitializeMods();
            CheckMods();
            if (CheckSA1Version(textBoxOriginalPath.Text))
                tabControl1.SelectedIndex = 0;
            else
                tabControl1.SelectedIndex = 1;
        }

        private void CheckMods()
        {
            // Load mods
            if (settings.Global["Mods"] != null)
            {
                string[] activeModListINI = settings.Global["Mods"].Split(',');
                for (int i = 0; i < activeModListINI.Length; i++)
                {
                    bool missing = true;
                    if (activeModListINI[i] != "")
                        foreach (Mod mod in mods)
                            if (mod.Name == activeModListINI[i])
                            {
                                missing = false;
                                activeMods.Add(activeModListINI[i]);
                            }
                    if (missing)
                    {
                        MessageBox.Show("Mod '" + activeModListINI[i] + "' is missing.\n\nIt will be removed from the active mod list.");
                    }
                }
            }
            // Check mods
            foreach (Mod mod in mods)
                modListView.Items.Add(new ListViewItem(new[] { mod.Name, mod.Author, mod.Version }) { Checked = activeMods.Contains(mod.Name) ? true : false });
        }

        private void LoadSettings(string file)
        {
            // Load INI
            var ini = new FileIniDataParser();
            settings = ini.ReadFile(Path.Combine(currentDir, "settings.ini"));
            // Load paths
            textBoxOriginalPath.Text = settings.Global["OriginalPath"];
            textBoxOutputPath.Text = settings.Global["OutputPath"];
        }

        private void SaveSettings()
        {
            IniData settings = new IniData();
            // Save paths
            settings.Global["OriginalPath"] = textBoxOriginalPath.Text;
            settings.Global["OutputPath"] = textBoxOutputPath.Text;
            // Save mods
            activeMods.Clear();
            for (int c = 0; c < modListView.Items.Count; c++)
            {
                if (modListView.Items[c].Checked)
                    activeMods.Add(modListView.Items[c].Text);
            }
            settings.Global["Mods"] = string.Join(",", activeMods);
            var ini = new FileIniDataParser();
            // Write INI
            ini.WriteFile(Path.Combine(currentDir, "settings.ini"), settings);
        }

        private void InitializeMods()
        {
            modListView.Items.Clear();
            var ini = new FileIniDataParser();
            string[] inifiles = Directory.GetFiles(Path.GetFullPath(Path.Combine(currentDir, "mods")), "mod.ini", SearchOption.AllDirectories);
            for (int i = 0; i < inifiles.Length; i++)
                mods.Add(new Mod(inifiles[i]));
        }

        private void modListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (modListView.SelectedItems.Count > 0)
                modDescription.Text = mods[modListView.SelectedIndices[0]].Description.Replace("\\n", System.Environment.NewLine);
        }

        enum SA1Version
        {
            US_1005,
            JP_International,
            US_1004,
            JP_Original,
            EU_1003,
            US_Limited,
            E3_Trial,
            Autodemo,
            JP_Taikenban,
            Unknown
        }

        private bool CheckSA1Version(string path)
        {
            SA1Version result;
            bool correct = false;
            string track01path = Path.Combine(path, "track01.bin");
            if (!File.Exists(track01path))
            {
                labelGameCheckResult.Text = "Could not find track01.bin in the selected folder.\n\nMake sure the selected folder contains a GDI file with all tracks.";
                return false;
            }
            byte[] track01 = File.ReadAllBytes(track01path);
            byte[] ver_b = new byte[13];
            for (int n = 0; n < 13; n++)
            {
                ver_b[n] = track01[0x5B + n];
            }
            string ver = Encoding.ASCII.GetString(ver_b);
            switch (ver)
            {
                case "1.00419990812":
                    result = SA1Version.US_1004;
                    break;
                case "1.00519991005":
                    result = SA1Version.US_1005;
                    correct = true;
                    break;
                case "1.00219990302":
                    result = SA1Version.JP_Taikenban;
                    break;
                case "1.00219990604":
                    result = SA1Version.US_Limited;
                    break;
                case "1.00719981210":
                    result = SA1Version.JP_Original;
                    break;
                case "1.00319990920":
                    result = SA1Version.JP_International;
                    break;
                case "1.00319990909":
                    result = SA1Version.EU_1003;
                    break;
                case "1.00019990608":
                    result = SA1Version.E3_Trial;
                    break;
                case "1.00019981016":
                    result = SA1Version.Autodemo;
                    break;
                default:
                    result = SA1Version.Unknown;
                    break;
            }
            string correctstring = correct ? "This is the correct version of the game." : "This is the wrong version of the game.";
            labelGameCheckResult.Text = (correctstring + System.Environment.NewLine + System.Environment.NewLine + "Game version: " + result.ToString() + System.Environment.NewLine + "Version string: " + ver);
            return correct;
        }

        private void textBoxOriginalPath_TextChanged(object sender, EventArgs e)
        {
            if (CheckSA1Version(textBoxOriginalPath.Text))
            {
                tabMods.Enabled = true;
                tabBuild.Enabled = true;
            }
            else
            {
                tabMods.Enabled = false;
                tabBuild.Enabled = false;
            }
            CheckBuildButton();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            VistaFolderBrowserDialog dlg = new VistaFolderBrowserDialog();
            if (textBoxOriginalPath.Text != "")
                dlg.SelectedPath = textBoxOriginalPath.Text;
            else
                dlg.SelectedPath = Environment.CurrentDirectory;
            dlg.ShowNewFolderButton = false;
            if (dlg.ShowDialog() == true)
                textBoxOriginalPath.Text = dlg.SelectedPath;
        }

        private void CheckBuildButton()
        {
            if (textBoxOutputPath.Text != "" && CheckSA1Version(textBoxOriginalPath.Text))
                buttonBuild.Enabled = true;
            else
                buttonBuild.Enabled = false;
        }

        private void textBoxOutputPath_TextChanged(object sender, EventArgs e)
        {
            CheckBuildButton();
        }

        private Process CreateProcessInfo(string file, string workdir, string arguments, EventHandler exit)
        {
            var proc = new Process();
            proc.StartInfo.FileName = Path.Combine(currentDir, "utils", file);
            proc.StartInfo.WorkingDirectory = workdir;
            proc.StartInfo.Arguments = arguments;
            //Console.WriteLine(proc.StartInfo.Arguments);
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.Exited += exit;
            proc.EnableRaisingEvents = true;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.UseShellExecute = false;
            proc.Start();
            var _ = ConsumeReader(proc.StandardOutput);
            _ = ConsumeReader(proc.StandardError);
            return proc;
        }

        async static Task ConsumeReader(TextReader reader)
        {
            string text;

            while ((text = await reader.ReadLineAsync()) != null)
            {
                Console.WriteLine(text);
            }
        }

        private void EnableBuildButton()
        {
            if (buttonBuild.InvokeRequired)
                buttonBuild.Invoke(new MethodInvoker(() => { EnableBuildButton(); }));
            else
                buttonBuild.Enabled = true;
        }

        private void RefreshProgress(string step, bool console = true)
        {
            if (labelStatus.InvokeRequired)
            {
                labelStatus.Invoke(new MethodInvoker(() => { RefreshProgress(step, console); }));
            }
            else
            {
                labelStatus.Text = step;
                if (console)
                {
                    Console.WriteLine();
                    Console.WriteLine(step);
                }
            }
        }

        private void buttonBuild_Click(object sender, EventArgs e)
        {
            string filename;
            string workdir = Path.Combine(textBoxOutputPath.Text, "data");
            string args;
            tabControl1.SelectedIndex = 2;
            textBoxLog.Text = "";
            buttonBuild.Enabled = false;
            // Recheck current mods
            activeMods.Clear();
            for (int c = 0; c < modListView.Items.Count; c++)
            {
                if (modListView.Items[c].Checked)
                    activeMods.Add(modListView.Items[c].Text);
            }
            writer = new TextBoxWriter(textBoxLog);
            timer1.Enabled = true;
            Console.SetOut(writer);
            Console.WriteLine("Build process started");
            RefreshProgress("Step 1/6: Running bin2iso on track 3");
            Directory.CreateDirectory(Path.Combine(textBoxOutputPath.Text, "data"));
            // bin2iso
            filename = "bin2iso.exe";
            args = "\"" + Path.Combine(textBoxOriginalPath.Text, "track03.bin") + "\" \"" + Path.Combine(textBoxOutputPath.Text, "track03.iso") + "\"";
            Process prc = CreateProcessInfo(filename, workdir, args, Step1_bin2iso_exit);
        }

        void Step1_bin2iso_exit(object sender, System.EventArgs e)
        {
            string filename;
            string workdir = Path.Combine(textBoxOutputPath.Text, "data");
            string args;
            if (!File.Exists(Path.Combine(textBoxOutputPath.Text, "track03.iso")))
            {
                Console.WriteLine("Error converting BIN to ISO: destination file {0} doesn't exist", Path.Combine(textBoxOutputPath.Text, "track03.iso"));
                return;
            }
            // extract
            RefreshProgress("Step 2/6: Extracting track03.iso");
            filename = "extract.exe";
            args = "\"" + Path.Combine(textBoxOutputPath.Text, "track03.iso") + "\"";
            CreateProcessInfo(filename, workdir, args, Step2_extract_exit);
        }

        void Step2_extract_exit(object sender, System.EventArgs e)
        {
            string filename;
            string workdir = Path.Combine(textBoxOutputPath.Text);
            string dataDir = "\"" + Path.Combine(workdir, "data") + "\"";
            string ipBin = "\"" + Path.Combine(currentDir, "utils", "ip.bin") + "\"";
            string gdiOutput = "\"" + workdir + "\"";
            string args;
            if (!File.Exists(Path.Combine(textBoxOutputPath.Text, "data", "1ST_READ.BIN")))
            {
                Console.WriteLine("Error: Extracted file {0} not found", Path.Combine(textBoxOutputPath.Text, "data", "1ST_READ.BIN"));
                return;
            }
            if (File.Exists(Path.Combine(textBoxOutputPath.Text, "track03.iso")))
                File.Delete(Path.Combine(textBoxOutputPath.Text, "track03.iso"));
            RefreshProgress("Step 3/6: Patching files");
            bool errors = false;
            // Select active mods to apply
            List<Mod> applyMods = new List<Mod>();
            for (int i = 0; i < mods.Count; i++)
            {
                for (int u = 0; u < activeMods.Count; u++)
                    if (mods[i].Name == activeMods[u])
                        applyMods.Add(mods[i]);
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
                Console.WriteLine("Error applying patches. Check the log and try again.");
                return;
            }
            RefreshProgress("Step 4/6: Copying original data tracks");
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
            RefreshProgress("Step 5/6: Building a modified image");
            filename = "buildgdi.exe";
            args = "-data " + dataDir + " -ip " + ipBin + " -output " + gdiOutput + " -raw";
            CreateProcessInfo(filename, workdir, args, Step4_gdi_exit);
        }

        void Step4_gdi_exit(object sender, System.EventArgs e)
        {
            RefreshProgress("Step 6/6: Cleaning up");
            string directoryPath = Path.Combine(textBoxOutputPath.Text, "data");
            Task<bool> task = TryDeleteDirectory(directoryPath, 3, 30);
            task.Wait();
            Console.WriteLine("The modified image is located at: {0}", textBoxOutputPath.Text);
            if (File.Exists(Path.Combine(currentDir, "codes", "SA1-DC-HD.cht")))
            {
                File.Copy(Path.Combine(currentDir, "codes", "SA1-DC-HD.cht"), Path.Combine(textBoxOutputPath.Text, "SA1-DC-HD.cht"), true);
                Console.WriteLine("Cheat file: {0}", Path.Combine(textBoxOutputPath.Text, "SA1-DC-HD.cht"));
            }
            Console.WriteLine("DONE!");
            RefreshProgress("Click Build to create a modified GDI image. Progress will be shown below.", false);
            EnableBuildButton();
        }

        void proc_DataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }

        private void buttonBrowseOutput_Click(object sender, EventArgs e)
        {
            VistaFolderBrowserDialog dlg = new VistaFolderBrowserDialog();
            if (textBoxOutputPath.Text != "")
                dlg.SelectedPath = textBoxOutputPath.Text;
            else
                dlg.SelectedPath = Environment.CurrentDirectory;
            dlg.ShowNewFolderButton = true;
            if (dlg.ShowDialog() == true)
                textBoxOutputPath.Text = dlg.SelectedPath;
            textBoxOutputPath_TextChanged(sender, e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (writer != null) writer.WriteOut();
        }

        private bool InstallMod(string datapath, Mod data)
        {
            Console.WriteLine("Processing files:");
            foreach (string file in data.FileReplacements)
            {
                Console.WriteLine(Path.GetFileName(file));
                string replFilePath = (Path.Combine(datapath, "SONICADV", Path.GetFileName(file)));
                File.Copy(file, replFilePath, true);
            }
            foreach (SectionData section in data.PatchData.Sections)
            {
                string destFilePath = (Path.Combine(datapath, section.SectionName));
                if (!File.Exists(destFilePath))
                {
                    Console.WriteLine("File {0} doesn't exist", destFilePath);
                    return false;
                }
                bool isPRS = Path.GetExtension(section.SectionName.ToLowerInvariant()) == ".prs";
                Console.Write("Processing patch data for {0}", section.SectionName);
                byte[] fileData = File.ReadAllBytes(destFilePath);
                if (isPRS)
                {
                    Console.Write(" (PRS)");
                    fileData = csharp_prs.Prs.Decompress(fileData);
                }
                Console.WriteLine();
                foreach (KeyData key in section.Keys)
                {
                    int address = int.Parse(key.KeyName, System.Globalization.NumberStyles.HexNumber);
                    string value = key.Value.Replace(" ", "");
                    //Console.WriteLine("{0}:{1}", address.ToString("X"), value);
                    if (value.Length % 2 != 0)
                    {
                        Console.WriteLine("Incorrect length of the hex string - must be divisible by 2");
                        return false;
                    }
                    int cnt = 0;
                    for (int i = 0; i < value.Length; i += 2)
                    {
                        byte toWrite = byte.Parse(value.Substring(i, 2), System.Globalization.NumberStyles.HexNumber);
                        //#if DEBUG
                        //Console.WriteLine("Changing {0} from {1} to {2}", (address + cnt).ToString("X"), fileData[address + i].ToString("X"), toWrite.ToString("X"));
                        //#endif
                        fileData[address + cnt] = byte.Parse(value.Substring(i, 2), System.Globalization.NumberStyles.HexNumber);
                        cnt++;
                    }
                }
                if (isPRS)
                {
                    Console.WriteLine("Compressing PRS...");
                    //#if DEBUG
                    //File.WriteAllBytes(Path.ChangeExtension(destFilePath, "BIN"), fileData);
                    //#endif
                    fileData = csharp_prs.Prs.Compress(ref fileData, 255);
                }
                if (!Directory.Exists(Path.GetDirectoryName(destFilePath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(destFilePath));
                File.WriteAllBytes(destFilePath, fileData);
            }
            Console.WriteLine("Patch applied.");
            Console.WriteLine();
            return true;
        }

        // From https://stackoverflow.com/a/44324346
        public static async Task<bool> TryDeleteDirectory(
        string directoryPath,
        int maxRetries = 10,
        int millisecondsDelay = 30)
        {
            if (directoryPath == null)
                throw new ArgumentNullException(directoryPath);
            if (maxRetries < 1)
                throw new ArgumentOutOfRangeException(nameof(maxRetries));
            if (millisecondsDelay < 1)
                throw new ArgumentOutOfRangeException(nameof(millisecondsDelay));

            for (int i = 0; i < maxRetries; ++i)
            {
                try
                {
                    if (Directory.Exists(directoryPath))
                    {
                        Directory.Delete(directoryPath, true);
                    }

                    return true;
                }
                catch (IOException)
                {
                    await Task.Delay(millisecondsDelay);
                }
                catch (UnauthorizedAccessException)
                {
                    await Task.Delay(millisecondsDelay);
                }
            }

            return false;
        }

        private void buttonSaveSettings_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }
    }


}
