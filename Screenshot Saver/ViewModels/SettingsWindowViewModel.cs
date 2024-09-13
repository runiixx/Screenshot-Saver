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

  
        public SettingsWindowViewModel()
        {
            WindowWidth=300;
            Manager=new SettingsManager();
            Manager.ReadFile();
            AutoSave = Manager.getSettingValue("AutoSave").Equals("True");
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
