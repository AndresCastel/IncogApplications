using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IncogStuffControl
{
    internal class KeyboardUtilities
    {
        internal static bool IsKeyModifyingPopupState(KeyEventArgs e)
        {
            return ((((Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt) && ((e.SystemKey == Key.Down) || (e.SystemKey == Key.Up)))
                  || (e.Key == Key.F4));
        }
    }
}
