using MySqlX.XDevAPI.Common;
using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.ClientNS.Controllers;
using ProFind.Lib.Global.Controllers;
using ProFind.Lib.Global.Services;
using ProFind.Lib.ProfessionalNS.Views.Main_Page;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.Int_Page
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Int_Page : Page
    {
        public Int_Page()
        {
            this.InitializeComponent();
        }

        // validations
        private static void ValidationsSetUp()
        {

        }


        private void RichTextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            new InAppNavigationController().NavigateTo(typeof(Operations.PasswordChangePage.SendEmailPage));
        }

        private void Professionals_Login_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Admins_Login_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var adminLogin = new AdminLogin
            {
                Email = Email_tb.Text,
                Password = Password_tb.Password,
            };


            try
            {
                await APIConnection.GetConnection.LoginAdminAsync(body: adminLogin);

                LoggedAdminStore.LoggedAdmin = await APIConnection.GetConnection.GetAdminFromEmailAsync(Email_tb.Text);

                new GlobalNavigationController().NavigateTo(typeof(Main_Page_Admin.Main_Page_Admin));

            }
            catch (ProFindServicesException ex)
            {
                if (ex.StatusCode == 201)
                {
                    new GlobalNavigationController().NavigateTo(typeof(Main_Page_Admin.Main_Page_Admin));
                }
                else if (ex.StatusCode == 400)
                {
                    var dialog = new MessageDialog("Incorrect, check your credentials.");
                    await dialog.ShowAsync();
                }
                else
                {
                    {
                        var dialog = new MessageDialog("Something went wrong, try again later.");
                        await dialog.ShowAsync();
                    }
                }

                Console.WriteLine(ex.Message);
            }
        }

        private void Professionals_Login_Click_1(object sender, RoutedEventArgs e)
        {
            new GlobalNavigationController().NavigateTo(typeof(Lib.ProfessionalNS.Views.InitPage.InitPage));

        }

        private void Clients_Login_Click(object sender, RoutedEventArgs e)
        {
            new GlobalNavigationController().NavigateTo(typeof(Lib.ClientNS.Views.InitPage.InitPage));
        }

        private void Hyperlink_Click_1(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            new GlobalNavigationController().NavigateTo(typeof(Operations.PasswordChangePage.SendEmailPage));
        }

        private void RichTextBlock_SelectionChanged_1(object sender, RoutedEventArgs e)
        {

        }

        private void Hyperlink_Click_2(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            new GlobalNavigationController().NavigateTo(typeof(Operations.PasswordChangePage.SendEmailPage));
        }
    }
}
