using Windows.System;
using Windows.UI.Input.Preview.Injection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Microsoft.UI.Xaml.Controls;
using System;
using Windows.UI.Xaml;

namespace ProFind.Lib.Global.Helpers
{
    public static class TextBoxExtension
    {
        public static void OnEnterNextField(this TextBox textBox)
        {
            textBox.KeyDown += delegate (object sender, KeyRoutedEventArgs e)
            {
                if (e.Key == VirtualKey.Enter)
                {
                    e.Handled = true;
                    PressTabKey();
                }
            };

            
        }

        public static void OnEnterNextField(this NumberBox numberBox)
        {
            numberBox.KeyDown += delegate (object sender, KeyRoutedEventArgs e)
            {
                if (e.Key == VirtualKey.Enter)
                {
                    e.Handled = true;
                    PressTabKey();
                }
            };
        }

        public static void OnEnterNextField(this PasswordBox passwordBox)
        {
            passwordBox.KeyDown += delegate (object sender, KeyRoutedEventArgs e)
            {
                if (e.Key == VirtualKey.Enter)
                {
                    e.Handled = true;
                    PressTabKey();
                }
            };
        }


        private static void PressTabKey()
        {
            var can= FocusManager.TryMoveFocus(FocusNavigationDirection.Next);
        }
    }
}