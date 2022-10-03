using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.Global.Helpers;
using ProFind.Lib.Global.Services;
using Project = ProFind.Lib.Global.Services.Project;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.ProfessionalNS.Views.CRUDPage.ProjectNS.CreatePage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class CreatePage : Page
    {
        Proposal SourceProposal = new Proposal();
        Project toManipulate = new Project();


        private string imageString;

        public CreatePage()
        {
            this.InitializeComponent();
            Cargar();
            AddEvents();
        }

        private void AddEvents()
        {
            Title_tb.OnEnterNextField();
            Description_tb.OnEnterNextField();
            TotalPrice_tb.OnEnterNextField();

        }


        private async void Cargar()
        {

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter != null)
            {
                if (e.Parameter.GetType() == typeof(Proposal))
                {
                    SourceProposal = e.Parameter as Proposal;
                }
            }
            else
            {
                new InAppNavigationController().NavigateTo(typeof(ReadPage.ReadPage));
            }
        }

        private async void Create_btn_Click(object sender, RoutedEventArgs e)
        {
            
        }


        private async void PictureSelection_btn_Checked(object sender, RoutedEventArgs e)
        {
            PictureSelection_btn.IsChecked = !PictureSelection_btn.IsChecked;
        }

        private async void PictureSelection_btn_Click(object sender, RoutedEventArgs e)
        {


        }

        private void Title_tb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Description_tb_TextChanged(object sender, TextChangedEventArgs e)
        {


        }



        private void TotalPrice_tb_ValueChanged(Microsoft.UI.Xaml.Controls.NumberBox sender, Microsoft.UI.Xaml.Controls.NumberBoxValueChangedEventArgs args)
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Title_tb_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {

        }

        private void Description_tb_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {

        }

        private void TotalPrice_tb_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            
        }

        private async void Create_btn_Click_1(object sender, RoutedEventArgs e)
        {
            if (!FieldsChecker.CheckName(Title_tb.Text))
            {
                var dialog = new MessageDialog("The Title must be valid");
                await dialog.ShowAsync();
                return;
            }
            if (!FieldsChecker.OnlyLetters(Description_tb.Text))
            {
                var dialog = new MessageDialog("The Description must be valid");
                await dialog.ShowAsync();
                return;
            }
            try
            {
                var toCreateProject = new Project { IdPj = "", TitlePj = Title_tb.Text, DescriptionPj = Description_tb.Text, PicturePj = imageString, TotalPricePj = int.Parse(TotalPrice_tb.Text), IdP1 = SourceProposal.IdP3, IdC1 = SourceProposal.IdC3, TagDurationPj = TagDuration_cbx.SelectedIndex };

                var result = await APIConnection.GetConnection.PostProjectAsync(toCreateProject);


                // Success dialog
                var dialog = new MessageDialog("Project created successfully");
                await dialog.ShowAsync();
            }
            catch (ProFindServicesException ex)
            {
                if (ex.StatusCode >= 200 && ex.StatusCode <= 300)
                {
                    var dialog = new MessageDialog("Project created successfully");
                    await dialog.ShowAsync();
                }
                else
                {
                    var dialog = new MessageDialog("Error creating project");
                    await dialog.ShowAsync();
                }
            }
            finally
            {
                new InAppNavigationController().NavigateTo(typeof(ReadPage.ReadPage));

            }


            if (string.IsNullOrEmpty(Title_tb.Text))
            {

                var dialog = new MessageDialog("The field is empty");
                await dialog.ShowAsync();
                return;
            }
            else if (string.IsNullOrEmpty(Description_tb.Text))
            {
                var dialog = new MessageDialog("The field is empty");
                await dialog.ShowAsync();
                return;
            }
        }

        private void PictureSelection_btn_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }

}
