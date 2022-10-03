using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.Global.Controllers;
using ProFind.Lib.Global.Helpers;
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
    public sealed partial class SendEmailPage : Page
    {
        public SendEmailPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string email = Email_tb.Text;
            if (!FieldsChecker.CheckEmail(email))
            {
                ToggleThemeTeachingTip1.IsOpen = true;
                return;
            }
            try
            {
                await APIConnection.GetConnection.SendRecoveryEmailProfessionalsAsync(email);
                new GlobalNavigationController().NavigateTo(typeof(CodeVerification), email);
            }
            catch (ProFindServicesException ex)
            {
                if (ex.StatusCode >= 200 && ex.StatusCode < 300)
                {
                    new GlobalNavigationController().NavigateTo(typeof(CodeVerification), email);
                }
                else
                {
                    ToggleThemeTeachingTip1.IsOpen = true;
                }
            } finally
            {
                new GlobalNavigationController().NavigateTo(typeof(CodeVerification), email);
                
            }
        }
    }
}
