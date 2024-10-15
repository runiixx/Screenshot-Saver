using Screenshot_Saver.Models;
using Screenshot_Saver.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Internal;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screenshot_Saver.ViewModels
{
    public class SettingsWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public SettingsManager Manager;

        private SettingsModel SettingsModel;

        public int WindowWidth
        {
            get
            {
                return SettingsModel.WindowWidth;
            }
            set
            {
                SettingsModel.WindowWidth= value;
                OnPropertyChanged(nameof(WindowWidth));
            }
        }
        public bool AutoSave
        {
            get
            {
                return SettingsModel.AutoSave;
            }
            set
            {
                SettingsModel.AutoSave= value;
                OnPropertyChanged(nameof(AutoSave));
                SettingsManager Manager = new SettingsManager();
                Manager.ReadFile();
                
                Manager.AddSetting("AutoSave", value.ToString());
                Manager.WriteFile();
            }
        }

        
        public bool DarkTheme
        {
            get
            {
                return SettingsModel.DarkTheme;
            }
            set
            {
                SettingsModel.DarkTheme= value;

                OnPropertyChanged(nameof(DarkTheme));
                Manager.ReadFile();
                Manager.AddSetting("DarkTheme", value.ToString());
                Manager.WriteFile();

                if (value)
                {
                    //AppTheme.ChangeTheme(new Uri("Dictionaries/DarkThemeColors.xaml", UriKind.Relative));
                    AppTheme.DarkTheme();
                }
                else
                {
                    //AppTheme.ChangeTheme(new Uri("Dictionaries/LightThemeColors.xaml", UriKind.Relative));
                    AppTheme.LightTheme();
                }
            }
        }

        public bool SaveCopies
        {
            get
            {
                return SettingsModel.SaveCopies;
            }
            set
            {
                SettingsModel.SaveCopies= value;
                OnPropertyChanged(nameof(SaveCopies));
                Manager.ReadFile();
                Manager.AddSetting("SaveCopies", value.ToString());
                Manager.WriteFile();
            }
        }
        public bool EnableSystemTrayIcon
        {
            get
            {
                return SettingsModel.EnableSystemTrayIcon;
            }
            set
            {
                SettingsModel.EnableSystemTrayIcon= value;
                OnPropertyChanged(nameof(EnableSystemTrayIcon));
                Manager.ReadFile();
                Manager.AddSetting("EnableSystemTrayIcon", value.ToString());
                Manager.WriteFile();
            }
        }
  
        public SettingsWindowViewModel()
        {
            SettingsModel = new SettingsModel();
            WindowWidth=300;
            Manager=new SettingsManager();
            Manager.ReadFile();
            AutoSave = Manager.getSettingValue("AutoSave").Equals("True");
            DarkTheme = Manager.getSettingValue("DarkTheme").Equals("True");
            SaveCopies = Manager.getSettingValue("SaveCopies").Equals("True");
            EnableSystemTrayIcon = Manager.getSettingValue("EnableSystemTrayIcon").Equals("True");
            //DarkTheme = true;
        }

      
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OnWindowClosing(object sender,CancelEventArgs e)
        {


            SettingsManager fileManager = new SettingsManager("WindowInstances.txt");
            fileManager.ReadFile();
            fileManager.AddSetting("OpenedSettingsWindow", false.ToString());
            fileManager.WriteFile();
        }
    }
}
