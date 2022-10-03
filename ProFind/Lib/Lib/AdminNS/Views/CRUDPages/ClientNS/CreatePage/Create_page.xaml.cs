using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ProFind.Lib.Global.Helpers;
using ProFind.Lib.Global.Services;
using Client = ProFind.Lib.Global.Services.Client;
using ProFind.Lib.AdminNS.Controllers;
using Windows.UI.Popups;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.ClientNS.CreatePage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Create_page : Page
    {

        private string imageString;
        public Create_page()
        {
            this.InitializeComponent();
            AddEvents();
        }
        private void AddEvents()
        {
            Name_tb.OnEnterNextField();
            Email_tb.OnEnterNextField();
            PhoneNumber_tb.OnEnterNextField();



        }



        private async void Create_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PictureSelection_btn_Checked(object sender, RoutedEventArgs e)
        {
            PictureSelection_btn.IsChecked = !PictureSelection_btn.IsChecked;
        }

        private void Name_tb_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
        }

        private void Email_tb_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
        }

        private async void Create_btn_Click_1(object sender, RoutedEventArgs e)
        {
            if (!FieldsChecker.CheckName(Name_tb.Text))
            {
                var dialog = new MessageDialog("The name must be valid.");
                await dialog.ShowAsync();
                return;
            }
            if (!FieldsChecker.CheckEmail(Email_tb.Text))
            {
                var dialog = new MessageDialog("The email must be valid.");
                await dialog.ShowAsync();
                return;
            }
            if (!FieldsChecker.CheckPassword(Password_pb.Password))
            {
                var dialog = new MessageDialog("The password must be valid.");
                await dialog.ShowAsync();
                return;
            }
            try
            {
                var ToCreateAdmin = new Client
                {
                    IdC = "",
                    NameC = Name_tb.Text,
                    EmailC = Email_tb.Text,
                    PasswordC = Password_pb.Password,
                    PictureC = imageString
                };

                var result = await APIConnection.GetConnection.PostClientAsync(ToCreateAdmin);

                // Show a success content dialog
                var dialog = new ContentDialog()
                {
                    Title = "Success",
                    Content = "Client created successfully",
                    CloseButtonText = "Ok"
                };
                await dialog.ShowAsync();


                ToggleThemeTeachingTip2.IsOpen = true;
            }
            catch (ProFindServicesException ex)
            {
                if (ex.StatusCode == 400)
                {
                    var dialog = new ContentDialog()
                    {
                        Title = "Error",
                        Content = "The client already exists",
                        CloseButtonText = "Ok"
                    };
                    await dialog.ShowAsync();
                }
                else if (ex.StatusCode == 201 || ex.StatusCode == 200)
                {
                    // Show a success content dialog
                    var dialog = new ContentDialog()
                    {
                        Title = "Success",
                        Content = "Client created successfully",
                        CloseButtonText = "Ok"
                    };
                    await dialog.ShowAsync();

                }
                else
                {
                    var dialog = new ContentDialog()
                    {
                        Title = "Error",
                        Content = "The client already exists",
                        CloseButtonText = "Ok"
                    };
                    await dialog.ShowAsync();
                }
            }
            finally
            {
                // Go back to clients list
                new InAppNavigationController().NavigateTo(typeof(Lib.AdminNS.Views.CRUDPages.ClientNS.ListPage.Clients_List));
            }
        }

        private void PictureSelection_btn_Checked_1(object sender, RoutedEventArgs e)
        {

        }

        private async void PictureSelection_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedImage = await PickFileHelper.PickImage();
                if (selectedImage != null)
                {
                    SelectedPicture_tbk.Text = selectedImage.Name;

                    imageString = await selectedImage.ToBase64StringAsync();
                    SelectedPicture_pp.ProfilePicture = await imageString.FromBase64String();
                }
            }
            catch (Exception ex)
            {
                // friendly error dialog
                var dialog = new MessageDialog("Image has not been loaded");
                await dialog.ShowAsync();

                Console.WriteLine(ex.Message);
            }
            finally
            {
                PictureSelection_btn.IsChecked = false;
            }
        }
    }
}
