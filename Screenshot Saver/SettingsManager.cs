using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Screenshot_Saver
{
    internal class SettingsManager
    {
        private Dictionary<string, string> SettingsDictionary = new Dictionary<string, string>();

        public void AddSetting(string Key, string Value)
        {
            SettingsDictionary.Add(Key, Value);
        }

        public string getSettingValue(string Key)
        {
            try
            {
                return SettingsDictionary[Key];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public  void  ReadFile()
        {
            StreamReader streamReader = new StreamReader(Directory.GetCurrentDirectory() + "\\Settings.txt");
            try
            {
                string line = streamReader.ReadLine();

                while (line !=null)
                {
                    string[] parts = line.Split('=');
                    SettingsDictionary.Add(parts[0], parts[1]);
                    line = streamReader.ReadLine();
                }
                streamReader.Close();
            }
            catch (IOException) { }
        }

        public  void WriteFile()
        {
            StreamWriter streamWriter = new StreamWriter(Directory.GetCurrentDirectory()+ "\\Settings.txt",false);
            try
            {
                foreach(KeyValuePair<string,string> setting in SettingsDictionary)
                {
                    streamWriter.WriteLine(setting.Key + "=" +setting.Value);
                }
                streamWriter.Close();
            }
            catch (IOException) { }
        }

        
    }


}
