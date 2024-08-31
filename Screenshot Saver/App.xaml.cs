using System.ComponentModel.Design.Serialization;
using System.Configuration;
using System.Data;
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
        MainWindow mainWindow = new MainWindow();
        NotifyIcon notifyIcon;
        Boolean isShown = true;
        private  GlobalKeyInterceptor.KeyInterceptor _interceptor;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            mainWindow.Show();

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
                mainWindow.Show();
            }
            else
            {
                mainWindow.Hide();
            }
        }
    }

}
