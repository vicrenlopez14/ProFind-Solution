using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.Global.Services;
using Admin = ProFind.Lib.Global.Services.Admin;
using ProFind.Lib.Global.Helpers;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.AdminNS.UpdatePage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class UpdatePage : Page
    {

        Admin toManipulate = new Admin();


        public UpdatePage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null)
            {
                toManipulate = e.Parameter as Admin;
                loadUsefulthings();
                AddEvents();
            }
        }

        private void AddEvents()
        {
            FirstName1_tbx.OnEnterNextField();
            Email_tbx.OnEnterNextField();
            Phone_tbx.OnEnterNextField();



        }

        private async void loadUsefulthings()
        {
            FirstName1_tbx.Text = toManipulate.NameA;
            Email_tbx.Text = toManipulate.EmailA;
            Phone_tbx.Text = toManipulate.TelA;
            Picture_img.ProfilePicture = await toManipulate.PictureA.FromBase64String();

        }

        private async void Reset_btn_Click(object sender, RoutedEventArgs e)
        {
        }

        private async void Update_btn_Click(object sender, RoutedEventArgs e)
        {


        }

        private async void Delete_btn_Click(object sender, RoutedEventArgs e)
        {



        }




        private void Back_btn_Click(object sender, RoutedEventArgs e)
        {
            new InAppNavigationController().GoBack();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FirstName1_tbx_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {

        }

        private void Email_tbx_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {


        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                var file = await PickFileHelper.PickImage();

                if (file != null)
                {
                    toManipulate.PictureA = await file.ToBase64StringAsync();
                    Picture_img.ProfilePicture = await toManipulate.PictureA.FromBase64String();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void Update_btn_Click_1(object sender, RoutedEventArgs e)
        {
            bool ChangeThePassword = false;

            if (!FieldsChecker.CheckEmail(Email_tbx.Text))
            {
                var dialog = new MessageDialog("The email must be valid.");
                await dialog.ShowAsync();
                return;
            }
            if (string.IsNullOrEmpty(toManipulate.PasswordA) && !string.IsNullOrEmpty(Password_tbx.Password))
            {
                if (!FieldsChecker.CheckPassword(Password_tbx.Password))
                {
                    var dialog = new MessageDialog("The password must be valid.");
                    await dialog.ShowAsync();
                    return;
                }

                ChangeThePassword = true;
            }
            if (!FieldsChecker.CheckName(FirstName1_tbx.Text))
            {
                var dialog = new MessageDialog("The name must be valid.");
                await dialog.ShowAsync();
                return;
            }

            try
            {
                toManipulate.EmailA = Email_tbx.Text;
                toManipulate.NameA = FirstName1_tbx.Text;
                if (Password_tbx.Password.Length > 0) toManipulate.PasswordA = Password_tbx.Password;
                toManipulate.TelA = Phone_tbx.Text;
                await APIConnection.GetConnection.PutAdminAsync(toManipulate.IdA, toManipulate);
                if (ChangeThePassword)
                    await APIConnection.GetConnection.ChangePasswordAdminsAsync(toManipulate.EmailA, Password_tbx.Password);
                var dialog = new MessageDialog("Admin updated successfully.");
                await dialog.ShowAsync();
            }
            catch (ProFindServicesException ex)
            {
                if (ex.StatusCode == 204)
                {
                    var dialog = new MessageDialog("Admin updated successfully.");
                    await dialog.ShowAsync();
                }
                else
                {
                    var dialog = new MessageDialog("There was an error try again later.");
                    await dialog.ShowAsync();
                }
            }
            finally
            {
                new InAppNavigationController().NavigateTo(typeof(ListPage.ListPageAdmin));
            }
        }

        private async void Delete_btn_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                await APIConnection.GetConnection.DeleteAdminAsync(toManipulate.IdA);

                var dialog = new MessageDialog("Admin deleted successfully.");
                await dialog.ShowAsync();
            }
            catch (ProFindServicesException ex)
            {
                if (ex.StatusCode == 204)
                {
                    var dialog = new MessageDialog("Admin deleted successfully.");
                    await dialog.ShowAsync();
                }
                else
                {
                    var dialog = new MessageDialog("You have to select an admin.");
                    await dialog.ShowAsync();
                }
            }
            finally
            {
                new InAppNavigationController().NavigateTo(typeof(ListPage.ListPageAdmin));
            }

        }

        private void Reset_btn_Click_1(object sender, RoutedEventArgs e)
        {

            FirstName1_tbx.Text = "";
            Email_tbx.Text = "";
            Phone_tbx.Text = "";
            Password_tbx.Password = "";
        }

        private void Back_btn_Click_1(object sender, RoutedEventArgs e)
        {
            new InAppNavigationController().NavigateTo(typeof(ListPage.ListPageAdmin));
        }
    }
}



