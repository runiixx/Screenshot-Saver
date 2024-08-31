using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Data.Common;
using System.Runtime.CompilerServices;


namespace Screenshot_Saver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Boolean isSave;
        String filename;
        String path;
        BitmapSource lastPhoto;
        BitmapSource currentPhoto;
        Thread saveThread;
        SettingsManager Manager;
        GlobalKeyInterceptor.KeyInterceptor _interceptor;

        
        public MainWindow()
        {
            InitializeComponent();
            isSave = true;
            YesLabel.Content = Directory.GetCurrentDirectory();
            Manager = new SettingsManager();
            Manager.ReadFile();
            FileNameTextBox.Text = Manager.getSettingValue("File name");
            path =Manager.getSettingValue("Path");
            GlobalKeyInterceptor.Shortcut[] shortcut = new GlobalKeyInterceptor.Shortcut[]{
                new GlobalKeyInterceptor.Shortcut(GlobalKeyInterceptor.Key.S, GlobalKeyInterceptor.KeyModifier.Win | GlobalKeyInterceptor.KeyModifier.Alt)
        };
            _interceptor = new GlobalKeyInterceptor.KeyInterceptor(shortcut);
            _interceptor.ShortcutPressed+=OnShortcutPressed;
        


    }
        private void OnShortcutPressed(object? sender ,GlobalKeyInterceptor.ShortcutPressedEventArgs e)
        {
            YesLabel.Content = "Pressed"; 
        }

    private void AutomaticSaving()
        {
            
            while (true)
            {
                Thread saveThread = new Thread(() => SaveImage());
                saveThread.SetApartmentState(ApartmentState.STA);
                saveThread.Start();
                saveThread.Join();
            }
            
            
          
        }
     
        private bool SimilarTwoImages()
        {
            if (lastPhoto!=null)
            {
                return lastPhoto.Width == currentPhoto.Width && lastPhoto.Height == currentPhoto.Height;

            }
            return false;
        }
        private void SaveImage()
        {
            
            
            if (System.Windows.Clipboard.ContainsImage())
            {
                currentPhoto = System.Windows.Clipboard.GetImage();
                if (!SimilarTwoImages())
                {
                    using (var fileStream = new FileStream(path +"\\"+ filename, FileMode.Create))
                    {
                        BitmapEncoder encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(currentPhoto as BitmapSource));
                        encoder.Save(fileStream);
                    }
                }
                else
                {
                    YesLabel.Content="LA FEL";
                }
            }
            lastPhoto = currentPhoto;
            Thread.Sleep(1000);
        }
        


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            filename = FileNameTextBox.Text;

            SaveImage();
            


        }

        private void SelectDirectoryButton_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new Microsoft.Win32.OpenFolderDialog();

            if (fileDialog.ShowDialog()==true)
            {
                path = fileDialog.FolderName;
                YesLabel.Content =path;
            }
            int size = 0;
            
            for(int i = 0; i<path.Length; i++)
            {
                size++;
                if (path[i] == '\\')
                {
                    size++;
                }
            }
            char[] filePathChar = new char[size];
            int j = 0;
            for(int i = 0; i<path.Length; i++)
            {
                filePathChar[j++] = path[i];
                if (path[i]=='\\')
                {
                    filePathChar[j++] = '\\';
                }
            }

            string correctFilePath = new string(filePathChar);
            YesLabel.Content=correctFilePath;
            path = correctFilePath;
        }

        private void SaveSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.AddSetting("Path", path);
            Manager.AddSetting("File name", FileNameTextBox.Text);
            Manager.WriteFile();
        }

        private void AutomaticButton_Click(object sender, RoutedEventArgs e)
        {
            AutomaticSaving();
        }
    }
}