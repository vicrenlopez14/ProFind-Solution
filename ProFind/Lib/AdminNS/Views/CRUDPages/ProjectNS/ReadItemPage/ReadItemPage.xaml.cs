using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using ProFind.Lib.Global.Helpers;
using ProFind.Lib.Global.Services;
using Client = ProFind.Lib.Global.Services.Client;
using Professional = ProFind.Lib.Global.Services.Professional;
using Project = ProFind.Lib.Global.Services.Project;
using ProFind.Lib.AdminNS.Controllers;
using Windows.UI.Xaml.Navigation;
using System.Net;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.ProjectNS.ReadItemPage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class ReadItemPage : Page
    {
        Project toManipulate = new Project();
        private string imageString;

        public ReadItemPage()
        {
            this.InitializeComponent();

        }
        private void AddEvents()
        {
            Title_tb.OnEnterNextField();
            Description_tb.OnEnterNextField();
            TotalPrice_tb.OnEnterNextField();

        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter == null || e.Parameter.GetType() != typeof(Project))
            {
                // Message dialog
                var dialog = new MessageDialog("Error while loading the project");
                await dialog.ShowAsync();
                // Navigate back
                new InAppNavigationController().GoBack();
            }

            toManipulate = e.Parameter as Project;

            Cargar();
            AddEvents();
        }

        private async void Cargar()
        {
            SelectedPicture_pp.ProfilePicture = await toManipulate.PicturePj.FromBase64String();
            Title_tb.Text = toManipulate.TitlePj;
            Description_tb.Text = toManipulate.DescriptionPj;
            TotalPrice_tb.Text = toManipulate.TotalPricePj.ToString();
            TimeRequired_cb.ItemsSource = await APIConnection.GetConnection.GetTimerequiredsAsync();


            Title_tb.IsEnabled = false;
            Description_tb.IsEnabled = false;
            TotalPrice_tb.IsEnabled = false;
            TimeRequired_cb.IsEnabled = false;
        }


        private void PictureSelection_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Title_tb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Description_tb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void InitialStatus_cb_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {

        }

        private void InitialStatus_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Professional_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Client_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Title_tb_KeyDown(object sender, KeyRoutedEventArgs e)
        {

        }

        private void Description_tb_KeyDown(object sender, KeyRoutedEventArgs e)
        {
        }

        private void TotalPrice_tb_KeyDown(object sender, KeyRoutedEventArgs e)
        {

        }





        private void Update_btn_Click_2(object sender, RoutedEventArgs e)
        {
            new InAppNavigationController().GoBack();
        }
    }
}
