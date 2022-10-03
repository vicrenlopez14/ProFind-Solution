using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.Global.Helpers;
using ProFind.Lib.Global.Services;
using Client = ProFind.Lib.Global.Services.Client;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.ClientNS.UpdatePage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Client_Update_Delete : Page
    {


        Client toManipulateClient;

        public Client_Update_Delete()
        {
            this.InitializeComponent();
            AddEvents();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter == null)
            {
                new InAppNavigationController().NavigateTo(typeof(ProFind.Lib.AdminNS.Views.CRUDPages.ClientNS.ListPage.Clients_List));
            }
            else
            {
                {
                    toManipulateClient = e.Parameter as Client;
                    FillFields();
                }
            }
        }

        private async void FillFields()
        {
            Name1_tbx.Text = toManipulateClient.NameC;
            Email_tbx.Text = toManipulateClient.EmailC;
            Picture_img.ProfilePicture = await toManipulateClient.PictureC.FromBase64String();

        }

        private void AddEvents()
        {
            Name1_tbx.OnEnterNextField();
            Email_tbx.OnEnterNextField();


        }


        private async Task Delete_btn_ClickAsync(object sender, RoutedEventArgs e)
        {


        }

        private async void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            await APIConnection.GetConnection.DeleteClientAsync(toManipulateClient.IdC);

            if (!FieldsChecker.CheckName(Name1_tbx.Text))
            {
                var dialog = new MessageDialog("The name must be valid.");
                await dialog.ShowAsync();
                return;
            }
            if (!FieldsChecker.CheckEmail(Email_tbx.Text))
            {
                var dialog = new MessageDialog("The email must be valid.");
                await dialog.ShowAsync();
                return;
            }
            if (!FieldsChecker.CheckPassword(Password_tbx.Password))
            {
                var dialog = new MessageDialog("The password must be valid.");
                await dialog.ShowAsync();
                return;
            }
        }

        private void Name1_tbx_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
        }

        private void Email_tbx_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (FieldsChecker.CheckEmail(Email_tbx.Text))
            {
                e.Handled = true;
                toManipulateClient.EmailC = Email_tbx.Text;
            }
            else e.Handled = false;
        }

        private void Phone_tbx_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
        }

        private async void Update_btn_Click_1(object sender, RoutedEventArgs e)
        {
            bool ChangeThePassword = false;

            if (string.IsNullOrEmpty(Name1_tbx.Text))
            {

                var dialog = new MessageDialog("The Name is empty.");
                await dialog.ShowAsync();
                return;
            }
            else if (!FieldsChecker.CheckEmail(Email_tbx.Text))
            {
                var dialog = new MessageDialog("The email is invalid.");
                await dialog.ShowAsync();
                return;
            }
            else if (Password_tbx.Password.Length > 0 && !FieldsChecker.CheckPassword(Password_tbx.Password))
            {
                var dialog = new MessageDialog("The password is invalid.");
                await dialog.ShowAsync();
                return;
            }
            if (string.IsNullOrEmpty(toManipulateClient.PasswordC) && !string.IsNullOrEmpty(Password_tbx.Password))
            {
                if (!FieldsChecker.CheckPassword(Password_tbx.Password))
                {
                    var dialog = new MessageDialog("The password must be valid.");
                    await dialog.ShowAsync();
                    return;
                }

                ChangeThePassword = true;
            }
            try
            {
                toManipulateClient.EmailC = Email_tbx.Text;
                toManipulateClient.NameC = Name1_tbx.Text;
                if (Password_tbx.Password.Length > 0) toManipulateClient.PasswordC = Password_tbx.Password;
                await APIConnection.GetConnection.PutClientAsync(toManipulateClient.IdC, body: toManipulateClient);
                if (ChangeThePassword)
                    await APIConnection.GetConnection.ChangePasswordClientAsync(toManipulateClient.EmailC, Password_tbx.Password);
                // Message dialog indicating success
                var dialog = new MessageDialog("The client has been updated");
                await dialog.ShowAsync();

            }
            catch (ProFindServicesException ex)
            {
                if (ex.StatusCode == 200 || ex.StatusCode == 201 || ex.StatusCode == 204)
                {
                    // Message dialog indicating success
                    var dialog = new MessageDialog("The client has been updated");
                    await dialog.ShowAsync();

                }
                else
                {
                    // Message dialog indicating error
                    var dialog = new MessageDialog("The client has not been updated, an error ocurred, try again later.");
                    await dialog.ShowAsync();
                }
            }
            finally
            {
                // Navigate to clients list
                new InAppNavigationController().NavigateTo(typeof(Lib.AdminNS.Views.CRUDPages.ClientNS.ListPage.Clients_List));
            }
        }

        private async void Delete_btn_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                await APIConnection.GetConnection.DeleteClientAsync(toManipulateClient.IdC);

                // Show success content dialog
                var dialog = new MessageDialog("The client has been deleted");
                await dialog.ShowAsync();

            }
            catch (ProFindServicesException ex)
            {
                if (ex.StatusCode == 200 || ex.StatusCode == 201)
                {
                    // Show success content dialog
                    var dialog = new MessageDialog("The client has been deleted");
                    await dialog.ShowAsync();
                }
                else
                {
                    // Show error content dialog
                    var dialog = new MessageDialog("The client has not been deleted, an error ocurred, try again later.");
                    await dialog.ShowAsync();
                }
            }
            finally
            {
                new InAppNavigationController().NavigateTo(typeof(Lib.AdminNS.Views.CRUDPages.ClientNS.ListPage.Clients_List));
            }
        }

        private async void UploadPicture_btn_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                toManipulateClient.PictureC = await (await PickFileHelper.PickImage()).ToBase64StringAsync();
                Picture_img.ProfilePicture = await toManipulateClient.PictureC.FromBase64String();

                // Success message dialog
                var dialog = new MessageDialog("The picture has been uploaded");
                await dialog.ShowAsync();
            }
            catch
            {
                // Failure dialog
                var dialog = new MessageDialog("No picture was selected.");
                await dialog.ShowAsync();
            }
        }

        private void Back_btn_Click_1(object sender, RoutedEventArgs e)
        {

            new InAppNavigationController().NavigateTo(typeof(Lib.AdminNS.Views.CRUDPages.ClientNS.ListPage.Clients_List));


        }

        private void Name1_tbx_TextChanged(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
        }

        private void Email_tbx_TextChanged(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {

        }

        private void Name1_tbx_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Email_tbx_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}