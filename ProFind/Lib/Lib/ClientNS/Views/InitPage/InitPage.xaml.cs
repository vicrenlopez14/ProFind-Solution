using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using ProFind.Lib.ClientNS.Views.Main_Page;
using ProFind.Lib.Global.Controllers;
using ProFind.Lib.Global.Helpers;
using ProFind.Lib.Global.Services;
using MySqlX.XDevAPI.Common;
using ProFind.Lib.ClientNS.Controllers;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.ClientNS.Views.InitPage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InitPage : Page
    {
        private string imageString;
        public InitPage()
        {

            this.InitializeComponent();
        }

        private void UnidentifiedUserForm_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
        }

        private void Trigger_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
        }

        private void Button_Click_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
        }

        private void Button_Click_2(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
        }

        private void Button_Click_3(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
        }

        private void Hyperlink_Click(Windows.UI.Xaml.Documents.Hyperlink sender,
            Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {


        }

        private void Name_tb_TextChanged(TextBlock sender, TextChangedEventArgs e)
        {
            ProfilePicture_pp.DisplayName = sender.Text;
        }


        private void Name_tb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void Administrators_Login_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            new GlobalNavigationController().NavigateTo(typeof(Lib.AdminNS.Views.Int_Page.Int_Page));
        }

        private void Professionals_Login_Click_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            new GlobalNavigationController().NavigateTo(typeof(Lib.ProfessionalNS.Views.InitPage.InitPage));
        }

        private async void btnExaminar_Click_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {


                var file = await PickFileHelper.PickImage();

                if (file != null)
                {
                    imageString = await (file).ToBase64StringAsync();
                    ProfilePicture_pp.ProfilePicture = await imageString.FromBase64String();
                }
            }
            catch
            {
                {
                    await new MessageDialog("Image not loaded").ShowAsync();
                }
            }
        }

        private async void Button_Click_4(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(Name_tb.Text))
            {
                var dialog = new MessageDialog("The field is empty");
                await dialog.ShowAsync();
                return;
            }
            else if (string.IsNullOrEmpty(Email_tb.Text))
            {
                var dialog = new MessageDialog("The field is empty");
                await dialog.ShowAsync();
                return;
            }
            else if (string.IsNullOrEmpty(Password_tb.Password))
            {
                var dialog = new MessageDialog("The field is empty");
                await dialog.ShowAsync();
                return;
            }

            var registerClient = new Client
            {
                IdC = "",
                NameC = Name_tb.Text,
                EmailC = Email_tb.Text,
                PasswordC = Password_tb.Password,
                PictureC = imageString,
            };

            try
            {
                await APIConnection.GetConnection.RegisterClientAsync(registerClient);

                LoggedClientStore.LoggedClient = await APIConnection.GetConnection.GetClientByEmailAsync(Email_tb.Text);

                new GlobalNavigationController().NavigateTo(typeof(Main_Page_Client));
            }
            catch (ProFindServicesException ex)
            {
                if (ex.StatusCode == 201)
                {
                    new GlobalNavigationController().NavigateTo(typeof(Main_Page_Client));
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

        private void Hyperlink_Click_1(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            new GlobalNavigationController().NavigateTo(typeof(InitPage_Login));
        }

        private void Hyperlink_Click_2(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            new GlobalNavigationController().NavigateTo(typeof(Operations.PasswordChangePage.SendEmailPage));
        }
    }
}