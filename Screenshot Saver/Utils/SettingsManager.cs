using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Screenshot_Saver.Utils
{
    public class SettingsManager
    {
        private string FileName;
        private Dictionary<string, string> SettingsDictionary = new Dictionary<string, string>();

        public SettingsManager(string FileName)
        {
            this.FileName=FileName;
        }
        public SettingsManager() {
            this.FileName="Settings.txt";
        }
        public void AddSetting(string Key, string Value)
        {
            if (SettingsDictionary.ContainsKey(Key))
            {
                SettingsDictionary[Key] = Value;
            }
            else
            {
                SettingsDictionary.Add(Key, Value);
            }
        }

        /// <summary>
        /// Gets a setting item
        /// </summary>
        /// <param name="Key"></param>
        /// <returns>Returns the value of a setting</returns>
        public string getSettingValue(string Key)
        {
            try
            {
                return SettingsDictionary[Key];
            }
            catch (Exception)
            {
                return "default";
            }
        }

        /// <summary>
        /// Reads the file and extracts the settings
        /// </summary>
        public void ReadFile()
        {

            string[] fullPathArray = { Directory.GetCurrentDirectory(), "Data", FileName };
            if (!Directory.Exists(Path.Combine(fullPathArray[0], fullPathArray[1])))

                Directory.CreateDirectory(Path.Combine(fullPathArray[0], fullPathArray[1]));
            if (!File.Exists(Path.Combine(fullPathArray)))
            {
                File.Create(Path.Combine(fullPathArray));
            }
            string fullPath = Path.Combine(fullPathArray);
            StreamReader streamReader = new StreamReader(fullPath);
            try
            {
                string line = streamReader.ReadLine();

                while (line != null)
                {
                    string[] parts = line.Split('=');

                    AddSetting(parts[0], parts[1]);
                    line = streamReader.ReadLine();
                }
                streamReader.Close();
            }
            catch (IOException) { }
        }


        /// <summary>
        /// Writes Settings to the file
        /// </summary>
        public void WriteFile()
        {
            string[] fullPathArray = { Directory.GetCurrentDirectory(), "Data",FileName };
            StreamWriter streamWriter = new StreamWriter(Path.Combine(fullPathArray), false);
            try
            {
                foreach (KeyValuePair<string, string> setting in SettingsDictionary)
                {
                    streamWriter.WriteLine(setting.Key + "=" + setting.Value);
                }
                streamWriter.Close();
            }
            catch (IOException) { }
        }


    }


}
