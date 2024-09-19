using Screenshot_Saver.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screenshot_Saver.ViewModels
{
    public class SettingsWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public SettingsManager Manager;

        private int _windowWidth;
        public int WindowWidth
        {
            get
            {
                return _windowWidth;
            }
            set
            {
                _windowWidth= value;
                OnPropertyChanged(nameof(WindowWidth));
            }
        }

        private bool _autoSave = false;
        public bool AutoSave
        {
            get
            {
                return _autoSave;
            }
            set
            {
                _autoSave= value;
                OnPropertyChanged(nameof(AutoSave));
                SettingsManager Manager = new SettingsManager();
                Manager.ReadFile();
                
                Manager.AddSetting("AutoSave", value.ToString());
                Manager.WriteFile();
            }
        }

        private bool _darkTheme;
        public bool DarkTheme
        {
            get
            {
                return _darkTheme;
            }
            set
            {
                _darkTheme= value;

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

        private bool _saveCopies;

        public bool SaveCopies
        {
            get
            {
                return _saveCopies;
            }
            set
            {
                _saveCopies= value;
                OnPropertyChanged(nameof(SaveCopies));
                Manager.ReadFile();
                Manager.AddSetting("SaveCopies", value.ToString());
                Manager.WriteFile();
            }
        }

        private bool _enableSystemTrayIcon;

        public bool EnableSystemTrayIcon
        {
            get
            {
                return _enableSystemTrayIcon;
            }
            set
            {
                _enableSystemTrayIcon = value;
                OnPropertyChanged(nameof(EnableSystemTrayIcon));
                Manager.ReadFile();
                Manager.AddSetting("EnableSystemTrayIcon", value.ToString());
                Manager.WriteFile();
            }
        }
  
        public SettingsWindowViewModel()
        {
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
    }
}
