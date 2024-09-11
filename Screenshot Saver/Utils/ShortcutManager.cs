using GlobalKeyInterceptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screenshot_Saver.Utils
{
    public class ShortcutManager
    {

        public void LoadShortcut()
        {
            GlobalKeyInterceptor.Shortcut[] Shortcuts =
            [
            new GlobalKeyInterceptor.Shortcut(GlobalKeyInterceptor.Key.S,GlobalKeyInterceptor.KeyModifier.Win | GlobalKeyInterceptor.KeyModifier.Alt)
            ];

            GlobalKeyInterceptor.KeyInterceptor KeyInterceptor= new GlobalKeyInterceptor.KeyInterceptor(Shortcuts);
            KeyInterceptor.ShortcutPressed+=OnShortcutPressed;
        }
       
        private void OnShortcutPressed(object? sender, ShortcutPressedEventArgs e)
        {

        }

    }
}
