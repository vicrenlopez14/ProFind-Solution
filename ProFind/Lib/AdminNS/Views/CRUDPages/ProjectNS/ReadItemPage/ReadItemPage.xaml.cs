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
using ProFind.Lib.ProfessionalNS.Controllers;
using System.Threading;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.ProjectNS.ReadItemPage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class ReadItemPage : Page
    {
        private string imageString;
        Professional Id;
        Proposal Denegada;

                private Project IncomingProject;
        
        public ReadItemPage()
        {
            this.InitializeComponent();
            Cargar();
        }
        private void AddEvents()
        {
            Title_tb.OnEnterNextField();
            Description_tb.OnEnterNextField();
            TotalPrice_tb.OnEnterNextField();

        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null)
            {
                IncomingProject = (Project)e.Parameter;
            }
            imageString = IncomingProject.PicturePj;
            Title_tb.Text = IncomingProject.TitlePj;
            Description_tb.Text = IncomingProject.DescriptionPj;
            SelectedPicture_pp.ProfilePicture = await IncomingProject.PicturePj.FromBase64String();
            ExpectedBegin_dp.Date = IncomingProject.StartDate.Value;
            Theend.Date = IncomingProject.EndDate.Value;
            TotalPrice_tb.Value = IncomingProject.TotalPricePj.Value;
            TimeRequired_cb.SelectedItem = IncomingProject.TimeRequiredTr1Navigation;
            Title_tb.IsEnabled = false;
            Description_tb.IsEnabled = false;
            ExpectedBegin_dp.IsEnabled = false;
            Theend.IsEnabled = false;



        }

        private async void Cargar()
        {



        }

        private async void PictureSelection_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                var file = await PickFileHelper.PickImage();

                if (file != null)
                {
                    imageString = await file.ToBase64StringAsync();
                    SelectedPicture_pp.ProfilePicture = await imageString.FromBase64String();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

            }
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




        private void Tag_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void PictureSelection_btn_Checked(object sender, RoutedEventArgs e)
        {

        }
    }

}
