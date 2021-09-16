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
    public partial class MainForm : Form
    {
        public class Mod
        {
            public string Name;
            public string Author;
            public string Version;
            public string Description;
            public string[] FileReplacements;
            public string[] RequiredCodes;
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
                if (PatchData.Global["RequiredCodes"] != null)
                    RequiredCodes = PatchData.Global["RequiredCodes"].Split(',');
            }
        }

        public IniData settings;
        public IniData codes;
        public List<Mod> mods;
        public List<string> activeMods;
        public List<string> activeCodes;
        public string currentDir;
        public string currentDirAss;
        public TextBoxWriter writer;

        public MainForm()
        {
            InitializeComponent();
            // Get the path where the assembly is loaded with built-in stuff
            currentDirAss = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            // Use external mods folder if available, otherwise use built-in mods
            if (Directory.Exists(Path.Combine(Environment.CurrentDirectory, "mods")))
                currentDir = Environment.CurrentDirectory;
            else
                currentDir = currentDirAss;
            activeMods = new List<string>(); 
            activeCodes = new List<string>();
            tabMods.Enabled = tabCodes.Enabled = false; 
            if (CheckSA1Version(textBoxOriginalPath.Text))
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
                tabControl1.SelectedIndex = 2;
        }

        private void CheckMods()
        {
            // Load codes
            if (settings.Global["Codes"] != null)
                activeCodes = settings.Global["Codes"].Split(',').ToList();
            // Load mods
            if (settings.Global["Mods"] != null)
            {
                string[] activeModListINI = settings.Global["Mods"].Split(',');
                for (int i = 0; i < activeModListINI.Length; i++)
                {
                    bool missing = true;
                    if (activeModListINI[i] != "")
                    {
                        foreach (Mod mod in mods)
                            if (mod.Name == activeModListINI[i])
                            {
                                missing = false;
                                activeMods.Add(activeModListINI[i]);
                                if (mod.RequiredCodes != null)
                                    for (int c = 0; c < mod.RequiredCodes.Length; c++)
                                    {
                                        if (!activeCodes.Contains(mod.RequiredCodes[c]))
                                            activeCodes.Add(mod.RequiredCodes[c]);
                                    }
                            }
                    }
                    else
                        missing = false;
                    if (missing)
                    {
                        MessageBox.Show("Mod '" + activeModListINI[i] + "' is missing.\n\nIt will be removed from the active mod list.");
                    }
                }
            }
            // Check mods
            foreach (Mod mod in mods)
                modListView.Items.Add(new ListViewItem(new[] { mod.Name, mod.Author, mod.Version }) { Checked = activeMods.Contains(mod.Name) ? true : false });
            // Initialize and check codes
            InitializeCodes();
        }

        private void LoadSettings(string file)
        {
            // Load INI
            var ini = new FileIniDataParser();
            settings = ini.ReadFile(Path.Combine(Environment.CurrentDirectory, "settings.ini"));
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
            activeCodes.Clear();
            for (int c = 0; c < modListView.Items.Count; c++)
            {
                if (modListView.Items[c].Checked)
                    activeMods.Add(modListView.Items[c].Text);
            }
            settings.Global["Mods"] = string.Join(",", activeMods);
            // Save codes
            for (int c = 0; c < codesListView.Items.Count; c++)
            {
                if (codesListView.Items[c].Checked)
                    activeCodes.Add(codesListView.Items[c].Text);
            }
            settings.Global["Codes"] = string.Join(",", activeCodes);
            // Write INI
            var ini = new FileIniDataParser();
            ini.WriteFile(Path.Combine(Environment.CurrentDirectory, "settings.ini"), settings);
        }

        private void InitializeMods()
        {
            modListView.Items.Clear();
            var ini = new FileIniDataParser();
            string[] inifiles = Directory.GetFiles(Path.GetFullPath(Path.Combine(currentDir, "mods")), "mod.ini", SearchOption.AllDirectories);
            for (int i = 0; i < inifiles.Length; i++)
                mods.Add(new Mod(inifiles[i]));
        }

        private void InitializeCodes()
        {
            codesListView.Items.Clear();
            var ini = new FileIniDataParser();
            // Default to internal codes
            string codesPath = Path.Combine(currentDirAss, "mods", "codes.cht");
            // If external codes exists, load them instead
            if (File.Exists(Path.Combine(Environment.CurrentDirectory, "mods", "codes.cht")))
                codesPath = Path.Combine(Environment.CurrentDirectory, "mods", "codes.cht");
            codes = ini.ReadFile(codesPath);
            for (int c = 0; c < 99; c++)
            {
                string keyName = "cheat" + c.ToString() + "_desc";
                if (codes.Global.ContainsKey(keyName))
                {
                    string codeDesc = codes.Global[keyName].Replace("\"", "");
                    //MessageBox.Show(codes.Global[keyName]);
                    codesListView.Items.Add(new ListViewItem(new[] { codeDesc }) { Checked = activeCodes.Contains(codeDesc) ? true : false });
                }
                else
                    break;
            }
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
            string correctstring = correct ? "This is the correct version of the game. You can now use the Mods and Build Image tabs." : "This is the wrong version of the game.";
            labelGameCheckResult.Text = (correctstring + System.Environment.NewLine + System.Environment.NewLine + "Game version: " + result.ToString() + System.Environment.NewLine + "Version string: " + ver);
            return correct;
        }

        private void textBoxOriginalPath_TextChanged(object sender, EventArgs e)
        {
            if (CheckSA1Version(textBoxOriginalPath.Text))
                tabMods.Enabled = tabCodes.Enabled = tabBuild.Enabled = true;
            else
                tabMods.Enabled = tabCodes.Enabled = tabBuild.Enabled = false;
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
            proc.StartInfo.FileName = Path.Combine(currentDirAss, "utils", file);
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

        private void EnableCode(string code)
        {
            activeCodes.Add(code);
            for (int c = 0; c < codesListView.Items.Count; c++)
            {
                if (codesListView.Items[c].Text == code)
                {
                    codesListView.Items[c].Checked = true;
                    break;
                }
            }
        }

        private void buttonBuild_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;
            textBoxLog.Text = "";
            buttonBuild.Enabled = false;
            // Recheck current mods
            activeMods.Clear();
            for (int c = 0; c < modListView.Items.Count; c++)
            {
                if (modListView.Items[c].Checked)
                    activeMods.Add(modListView.Items[c].Text);
            }
            // Recheck current codes
            activeCodes.Clear();
            for (int c = 0; c < codesListView.Items.Count; c++)
            {
                if (codesListView.Items[c].Checked)
                    activeCodes.Add(codesListView.Items[c].Text);
            }
            // Force enable codes
            List<Mod> applyMods = new List<Mod>();
            for (int i = 0; i < mods.Count; i++)
                for (int u = 0; u < activeMods.Count; u++)
                    if (mods[i].Name == activeMods[u])
                        applyMods.Add(mods[i]);
            foreach (Mod data in applyMods)
                if (data.RequiredCodes != null)
                    for (int c = 0; c < data.RequiredCodes.Length; c++)
                        EnableCode(data.RequiredCodes[c]);
            writer = new TextBoxWriter(textBoxLog);
            timer1.Enabled = true;
            Console.SetOut(writer);
            Console.WriteLine("Build process started");
            StartBin2Iso();
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

        private void buttonSaveSettings_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }
    }
}
