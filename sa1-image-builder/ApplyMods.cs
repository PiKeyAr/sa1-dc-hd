using IniParser.Model;
using System;
using System.IO;

namespace GUIPatcher
{
    public partial class MainForm
    {
        private static bool InstallMod(string datapath, Mod data)
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
                Console.WriteLine("Processing patch data for {0}", section.SectionName);
                byte[] fileData = File.ReadAllBytes(isPRS ? Path.ChangeExtension(destFilePath, ".bin") : destFilePath);
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
                if (!Directory.Exists(Path.GetDirectoryName(destFilePath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(destFilePath));
                File.WriteAllBytes(isPRS ? Path.ChangeExtension(destFilePath, ".bin") : destFilePath, fileData);
            }
            Console.WriteLine("Patch applied.");
            Console.WriteLine();
            return true;
        }
    }
}
