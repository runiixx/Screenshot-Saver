using System.ComponentModel.Design.Serialization;
using System.Configuration;
using System.Data;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Windows;
using Forms = System.Windows.Forms;

namespace Screenshot_Saver
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    
    public partial class App : System.Windows.Application
    {
        [DllImport("Kernel32")]
        public static extern void AllocConsole();

        [DllImport("Kernel32", SetLastError = true)]
        public static extern void FreeConsole();


        MainWindow mainWindow = new MainWindow();
        NotifyIcon notifyIcon;
        Boolean isShown = true;
        private  GlobalKeyInterceptor.KeyInterceptor _interceptor;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //AllocConsole();
            mainWindow.Show();

            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = new System.Drawing.Icon("Resources/icon.ico");
            notifyIcon.Visible = true;
            notifyIcon.Click += NotifyIcon_Click;

            SettingsManager Manager = new SettingsManager();
            Manager.ReadFile();
        }


        private void ApplicationExit(object sender, ExitEventArgs e)
        {
            if (notifyIcon is not null)
            {
                notifyIcon.Icon.Dispose();
                notifyIcon.Dispose();
            }
        }      
        private void NotifyIcon_Click(object? sender, EventArgs e)
        {
            isShown = !isShown;
            if (isShown)
            {
                mainWindow.Show();
            }
            else
            {
                mainWindow.Hide();
            }
        }
    }

}
