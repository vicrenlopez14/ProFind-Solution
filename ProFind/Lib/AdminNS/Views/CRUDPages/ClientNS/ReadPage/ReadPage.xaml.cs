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

namespace ProFind.Lib.AdminNS.Views.CRUDPages.ClientNS.ReadPage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class ReadPage : Page
    {


        Client toManipulateClient;

        public ReadPage()
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
            Name1_tbx.IsEnabled = false;
            Email_tbx.IsEnabled = false;
        }

        private void AddEvents()
        {
            Name1_tbx.OnEnterNextField();
            Email_tbx.OnEnterNextField();


        }


        private async Task Delete_btn_ClickAsync(object sender, RoutedEventArgs e)
        {


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





        private void Back_btn_Click_1(object sender, RoutedEventArgs e)
        {

            new InAppNavigationController().GoBack();


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