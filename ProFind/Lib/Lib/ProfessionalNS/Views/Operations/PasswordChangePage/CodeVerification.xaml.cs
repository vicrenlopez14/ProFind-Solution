using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.ClientNS.Controllers;
using ProFind.Lib.Global.Controllers;
using ProFind.Lib.Global.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.ProfessionalNS.Views.Operations.PasswordChangePage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CodeVerification : Page
    {
        public CodeVerification()
        {
            this.InitializeComponent();
        }
        private string email;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if(e.Parameter != null)
            {
                email = e.Parameter.ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new GlobalNavigationController().NavigateTo(typeof(SendEmailPage));
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                await APIConnection.GetConnection.VerifyRecoveryCodeProfessionalsAsync(Code_tb.Text);
                new GlobalNavigationController().NavigateTo(typeof(PasswordChangePage), email);
                


            }
            catch (ProFindServicesException ex)
            {
                if (ex.StatusCode >= 200 && ex.StatusCode < 300)
                {
                    new GlobalNavigationController().NavigateTo(typeof(PasswordChangePage), email);
                }
                else if (ex != null)
                {
                    ToggleThemeTeachingTip1.IsOpen = true;
                }
            }
            
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
