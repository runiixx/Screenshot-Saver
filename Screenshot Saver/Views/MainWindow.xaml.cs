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
using Screenshot_Saver.ViewModels;
using System.Runtime.InteropServices;
using Screenshot_Saver.Utils;


namespace Screenshot_Saver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        private bool isNotifyIconVisible;
        private bool dublicate = true;
        public MainWindow()
        {

            
            InitializeComponent();
            DataContext= new MainWindowViewModel();

            SettingsManager settingsManager = new SettingsManager();
            settingsManager.ReadFile();

            if (settingsManager.getSettingValue("EnableSystemTrayIcon").Equals("True") && dublicate)
            {
                dublicate=false;
                NotifyIcon notifyIcon = new NotifyIcon();
                notifyIcon.Icon =new System.Drawing.Icon("Resources/icon.ico");
                notifyIcon.Visible = true;
                notifyIcon.Click += NotifyIcon_Click;
                isNotifyIconVisible = true;
            }
          
        }

        private protected void NotifyIcon_Click(object sender, EventArgs e)
        {
            isNotifyIconVisible = !isNotifyIconVisible;
            if (isNotifyIconVisible) { this.Show(); }
            else { this.Hide(); }
        }
       
    }
}