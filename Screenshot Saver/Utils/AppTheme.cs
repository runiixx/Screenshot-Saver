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

        public static void DarkTheme()
        {
            ResourceDictionary Theme = new ResourceDictionary()
            {
                Source = new Uri("Dictionaries/DarkThemeColors.xaml", UriKind.Relative)
            };

            ResourceDictionary Animations = new ResourceDictionary()
            {
                Source = new Uri("Dictionaries/DarkThemeStoryBoards.xaml", UriKind.RelativeOrAbsolute)
            };

            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(Theme);
            //Application.Current.Resources.MergedDictionaries.Add(Animations);
        }
        public static void LightTheme()
        {
            ResourceDictionary Theme = new ResourceDictionary()
            {
                Source = new Uri("Dictionaries/LightThemeColors.xaml", UriKind.Relative)
            };

            ResourceDictionary Animations = new ResourceDictionary()
            {
                Source = new Uri("Dictionaries/LightThemeStoryBoards.xaml", UriKind.RelativeOrAbsolute)
            };

            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(Theme);
            //Application.Current.Resources.MergedDictionaries.Add(Animations);
        }
    }
}
