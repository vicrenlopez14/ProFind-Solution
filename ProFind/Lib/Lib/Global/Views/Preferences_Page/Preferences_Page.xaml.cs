using ProFind.Lib.Global.Helpers;
using System;
using System.Diagnostics;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.Global.Views.Preferences_Page
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Preferences_Page : Page
    {
        public Preferences_Page()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Load the settings
            LoadSettings();
        }

        private void LoadSettings()
        {


        }

        private void OnThemeRadioButtonKeyDown(object sender, KeyRoutedEventArgs e)
        {

        }

        private void OnThemeRadioButtonChecked(object sender, RoutedEventArgs e)
        {

        }

        private void DarkModeToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (DarkModeToggleSwitch.IsOn)
            {
                ApplicationData.Current.LocalSettings.Values["DarkMode"] = true;
            }
            else
            {
                ApplicationData.Current.LocalSettings.Values["DarkMode"] = false;
            }
        }

        private void SoundEffectsToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (SoundEffectsToggleSwitch.IsOn)
            {
                ElementSoundPlayer.State = ElementSoundPlayerState.On;
            }
            else
            {
                {
                    ElementSoundPlayer.State = ElementSoundPlayerState.Off;
                }
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            AppRestartFailureReason result =
            await CoreApplication.RequestRestartAsync("");
            if (result == AppRestartFailureReason.NotInForeground ||
            result == AppRestartFailureReason.RestartPending ||
            result == AppRestartFailureReason.Other)
            {
                Debug.WriteLine("RequestRestartAsync failed: {0}", result);
            }

        }
    }
}
