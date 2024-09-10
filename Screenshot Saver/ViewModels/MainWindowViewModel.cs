using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Screenshot_Saver.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string path;
        private SettingsManager Manager;
        private string FileName;
        private BitmapSource CurrentPhoto;
        private BitmapSource LastPhoto;

        private GlobalKeyInterceptor.KeyInterceptor KeyInterceptor;

        public ICommand ISelectDirectory => new RelayCommand(SelectDirectory);
        public ICommand ISaveSettings => new RelayCommand(SaveSettings);

        public ICommand ISaveImage => new RelayCommand(SaveImage);
        private void SelectDirectory()
        {
            Microsoft.Win32.OpenFolderDialog FolderDialog= new Microsoft.Win32.OpenFolderDialog(); ;
            if (FolderDialog.ShowDialog()==true)
            {
                path=FolderDialog.FolderName;
            }

            //path=FilesystemManipulation.CorrectPath(path);
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
            string FullPath = (Copy!=0) ? $"{path}\\{FileName}({Copy}).png" : $"{path}\\{FileName}.png";


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
            return (LastPhoto is not null) ? LastPhoto.Width==CurrentPhoto.Width ||
                                         LastPhoto.Height==CurrentPhoto.Height
                                       : false;
        }
        public MainWindowViewModel()
        {
            Manager=new SettingsManager();
            Manager.ReadFile();

            FileName= (Manager.getSettingValue("File name") is null) ? "default" : Manager.getSettingValue("File name");
            path=(Manager.getSettingValue("Path") is null) ? "C:\\" : Manager.getSettingValue("Path");

            GlobalKeyInterceptor.Shortcut[] Shortcuts = new GlobalKeyInterceptor.Shortcut[]
            {
                new GlobalKeyInterceptor.Shortcut(GlobalKeyInterceptor.Key.S,GlobalKeyInterceptor.KeyModifier.Win | GlobalKeyInterceptor.KeyModifier.Alt)
            };

            KeyInterceptor = new GlobalKeyInterceptor.KeyInterceptor(Shortcuts);
            KeyInterceptor.ShortcutPressed+=OnShortcutPressed;
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
