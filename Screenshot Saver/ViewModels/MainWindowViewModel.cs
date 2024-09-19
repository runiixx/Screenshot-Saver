using Screenshot_Saver.Utils;
using Screenshot_Saver.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Screenshot_Saver.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private bool autosave;

        private string path;
        private SettingsManager Manager;
        private string _fileName;
        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName=value;
                OnPropertyChanged(nameof(FileName));
                SettingsManager Manager = new SettingsManager();
                Manager.ReadFile();
                if (Manager.getSettingValue("AutoSave").Equals("True"))
                {
                    Manager.AddSetting("File name", value);
                    Manager.WriteFile();
                }
            }
        }
        private BitmapSource CurrentPhoto;
        private BitmapSource LastPhoto;

       
        private GlobalKeyInterceptor.KeyInterceptor KeyInterceptor;

        public ICommand ISelectDirectory => new RelayCommand(SelectDirectory);
        public ICommand ISaveSettings => new RelayCommand(SaveSettings);
        public ICommand IOpenSettingsWindow => new RelayCommand(OpenSettingsWindow);
        public ICommand ISaveImage => new RelayCommand(SaveImage);
        private void SelectDirectory()
        {
            Microsoft.Win32.OpenFolderDialog FolderDialog= new Microsoft.Win32.OpenFolderDialog(); ;
            if (FolderDialog.ShowDialog()==true)
            {
                path=FolderDialog.FolderName;
            }
            if (Manager.getSettingValue("AutoSave").Equals("True"))
            {
                Manager.AddSetting("Path", path);
                Manager.WriteFile();
            }
            
            //path=FilesystemManipulation.CorrectPath(path);
        }

        private void OpenSettingsWindow()
        {
            SettingsWindow SettingsWindow=new SettingsWindow();
            SettingsWindow.Show();

            
        }
        private void SaveSettings()
        {
            Manager.AddSetting("Path", path);
            Manager.AddSetting("File name", FileName);
            Manager.WriteFile();
        }

        private void SaveImage()
        {
            
            int Copy = FilesystemManipulation.GetLastCopy(path, FileName);
            string FullPath=(Copy!=0) ? Path.Combine(path, $"{FileName}({Copy}).png") : Path.Combine(path, $"{FileName}.png");

            if (System.Windows.Clipboard.ContainsImage())
            {
                CurrentPhoto = System.Windows.Clipboard.GetImage();
                if (!SimilarTwoImages())
                {
                    using(var FileStream=new FileStream(FullPath, FileMode.Create))
                    {
                        BitmapEncoder Encoder = new PngBitmapEncoder();
                        Encoder.Frames.Add(BitmapFrame.Create(CurrentPhoto as BitmapSource));
                        Encoder.Save(FileStream);
                    }
                }
            }
            LastPhoto=CurrentPhoto;
            
        }


        private bool SimilarTwoImages()
        {
            if(LastPhoto is null)
                return false;


            Bitmap LastPhotoBitmap = BitmapSourceToBitmap(LastPhoto);
            Bitmap CurrentPhotoBitmap=BitmapSourceToBitmap(CurrentPhoto);

            bool ok = true;
            for (int i = 0; i<LastPhoto.Height; i++)
            {
                for (int j = 0; j<LastPhoto.Width; j++)
                {
                    if (!LastPhotoBitmap.GetPixel(j,i).Equals(CurrentPhotoBitmap.GetPixel(j,i))){
                        ok = false;
                    }
                }
            }
            return ok;
        }

        private System.Drawing.Bitmap BitmapSourceToBitmap(BitmapSource BitmapSource)
        {
            System.Drawing.Bitmap Bipmap;
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder Encoder = new BmpBitmapEncoder();
                Encoder.Frames.Add(BitmapFrame.Create(CurrentPhoto));
                Encoder.Save(outStream);
                Bipmap=new System.Drawing.Bitmap(outStream);
            }
            return Bipmap;

        }

        [DllImport("Kernel32")]
        public static extern void AllocConsole();
        public MainWindowViewModel()
        {
            FileName="def";
            OnPropertyChanged(FileName);
            Manager=new SettingsManager();
            Manager.ReadFile();

            FileName= (Manager.getSettingValue("File name") is null) ? "default" : Manager.getSettingValue("File name");
            path=(Manager.getSettingValue("Path") is null) ? "C:\\" : Manager.getSettingValue("Path");

            GlobalKeyInterceptor.Shortcut[] Shortcuts =
            [
                new GlobalKeyInterceptor.Shortcut(GlobalKeyInterceptor.Key.S,GlobalKeyInterceptor.KeyModifier.Win | GlobalKeyInterceptor.KeyModifier.Alt)
            ];

            KeyInterceptor = new GlobalKeyInterceptor.KeyInterceptor(Shortcuts);
            KeyInterceptor.ShortcutPressed+=OnShortcutPressed;

            
            if(App.Current is not null)
            {
                if (Manager.getSettingValue("DarkTheme").Equals("False"))
                {
                    AppTheme.ChangeTheme(new Uri("Dictionaries/LightThemeColors.xaml", UriKind.Relative));
                }
            }
        }

        private void OnShortcutPressed(object? sender, GlobalKeyInterceptor.ShortcutPressedEventArgs e)
        {
            SaveImage();
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
