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
using System.Windows.Forms;


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
        SettingsManager Manager;
        GlobalKeyInterceptor.KeyInterceptor _interceptor;

        NotifyIcon notifyIcon;
        Boolean isShown = true;
        public MainWindow()
        {

            
            InitializeComponent();
            isSave = true;
            
            Manager = new SettingsManager();
            Manager.ReadFile();
            FileNameTextBox.Text = (Manager.getSettingValue("File name") is null) ? "default" : Manager.getSettingValue("File name");
            path = (Manager.getSettingValue("Path") is null) ? "C:\\" : Manager.getSettingValue("Path");
         
            GlobalKeyInterceptor.Shortcut[] shortcut = new GlobalKeyInterceptor.Shortcut[]{
                new GlobalKeyInterceptor.Shortcut(GlobalKeyInterceptor.Key.S, GlobalKeyInterceptor.KeyModifier.Win | GlobalKeyInterceptor.KeyModifier.Alt)
        };
            _interceptor = new GlobalKeyInterceptor.KeyInterceptor(shortcut);
            _interceptor.ShortcutPressed+=OnShortcutPressed;



            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = new System.Drawing.Icon("Resources/icon.ico");
            notifyIcon.Visible = true;
            notifyIcon.Click += NotifyIcon_Click;
        }
        private void NotifyIcon_Click(object? sender, EventArgs e)
        {
            isShown = !isShown;
            if (isShown)
            {
                this.Show();
                
            }
            else
            {
                this.Hide();
            }
        }
        private void OnShortcutPressed(object? sender ,GlobalKeyInterceptor.ShortcutPressedEventArgs e)
        {
            
            filename = FileNameTextBox.Text;
            SaveImage();
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

            int copy = FilesystemManipulation.GetLastCopy(path, filename);
            
            if (System.Windows.Clipboard.ContainsImage())
            {
                currentPhoto = System.Windows.Clipboard.GetImage();
               
                if (!SimilarTwoImages())
                {
                    using (var fileStream = new FileStream($"{path}\\{filename}({copy}).png", FileMode.Create))
                    {
                        BitmapEncoder encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(currentPhoto as BitmapSource));
                        encoder.Save(fileStream);
                    }
                }
                else
                {
                   
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
                
            }
            int size = 0;
            path = FilesystemManipulation.CorrectPath(path);

            
        }

        private void SaveSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.AddSetting("Path", path);
            Manager.AddSetting("File name", FileNameTextBox.Text);
            Manager.WriteFile();
        }
    }
}