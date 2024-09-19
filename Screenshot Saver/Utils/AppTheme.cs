using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Application = System.Windows.Application;

namespace Screenshot_Saver.Utils
{
    public class AppTheme
    {
        public static void ChangeTheme(Uri ThemeUri)
        {
            ResourceDictionary Theme = new ResourceDictionary() { Source=ThemeUri};

            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(Theme);
        }
    }
}
