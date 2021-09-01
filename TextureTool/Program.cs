using System;
using System.Diagnostics;
using System.IO;
using IniParser;
using IniParser.Model;

namespace TextureTool
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            Console.WriteLine("Step 1: Extracting HD GUI");
            if (!Directory.Exists(Path.GetFullPath(Path.Combine(currentDir, "textures"))))
            {
                Console.WriteLine("Folder doesn't exist: {0}", Path.GetFullPath(Path.Combine(currentDir, "textures")));
                return;
            }
            string[] pvmx = Directory.GetFiles(Path.GetFullPath(Path.Combine(currentDir, "textures")), "*.pvmx", SearchOption.AllDirectories);
            Console.WriteLine("Found {0} PVMX files", pvmx.Length);
            for (int i = 0; i < pvmx.Length; i++)
            {
                var proc = new Process();
                proc.StartInfo.FileName = Path.Combine(currentDir, "utils", "pvmx.exe");
                proc.StartInfo.WorkingDirectory = Path.Combine(currentDir, "textures");
                proc.StartInfo.Arguments = "-e " + Path.GetFileName(pvmx[i]);
                Console.WriteLine("Extracting: {0}", Path.GetFileName(pvmx[i]));
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.UseShellExecute = false;
                proc.Start();
                proc.WaitForExit();
                File.Delete(pvmx[i]);
            }
            string[] indices = Directory.GetFiles(Path.GetFullPath(Path.Combine(currentDir, "textures")), "*.txt", SearchOption.AllDirectories);
            Console.WriteLine("Deleting {0} indices...", indices.Length);
            for (int i = 0; i < indices.Length; i++)
            {
                File.Delete(indices[i]);
            }
            Console.WriteLine();
            Console.WriteLine("Step 2: Creating matched textures");
            if (!File.Exists(Path.Combine(currentDir, "textures.ini")))
            {
                Console.WriteLine("File doesn't exist: {0}", Path.Combine(currentDir, "textures.ini"));
                return;
            }
            var ini = new FileIniDataParser();
            IniData settings = ini.ReadFile(Path.Combine(currentDir, "textures.ini"));
            Directory.CreateDirectory(Path.Combine(currentDir, "MK-51000"));
            foreach (KeyData key in settings.Global)
            {
                string origname = Path.Combine(currentDir, "textures", key.Value);
                if (!File.Exists(origname))
                {
                    Console.WriteLine("File doesn't exist: {0}", origname);
                    return;
                }
                string destname = Path.Combine(currentDir, "MK-51000", key.KeyName);
                Console.WriteLine("{0}:{1}", Path.GetFileName(destname), origname);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(origname);
                bitmap.RotateFlip(System.Drawing.RotateFlipType.RotateNoneFlipY);
                bitmap.Save(destname);
            }
            Console.WriteLine();
            Console.WriteLine("Output folder: {0}", Path.Combine(currentDir, "MK-51000"));
            Console.WriteLine("Put it in flycast/data/textures");
            Console.WriteLine("Done!");
        }
    }
}
