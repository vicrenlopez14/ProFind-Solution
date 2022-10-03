using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.Global.Helpers;
using ProFind.Lib.Global.Services;
using Project = ProFind.Lib.Global.Services.Project;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.ProfessionalNS.Views.CRUDPage.ProjectNS.UpdatePage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Update_Project : Page
    {
        Project toManipulate = new Project();
        private string imageString;

        public Update_Project()
        {
            this.InitializeComponent();
            AddEvents();
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
            }

            toManipulate = e.Parameter as Project;

            Cargar();

        }

        private async void Cargar()
        {
            
            Title_tb.Text = toManipulate.TitlePj;
            Description_tb.Text = toManipulate.DescriptionPj;
            TotalPrice_tb.Text = toManipulate.TotalPricePj.ToString();
           
            TagDuration_cb.SelectedIndex = toManipulate.TagDurationPj ?? 0;
        }

        private async void Update_btn_Click(object sender, RoutedEventArgs e)
        {
         
        }

        private async void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            await APIConnection.GetConnection.DeleteProjectAsync(toManipulate.IdPj);
        }
        private async void PictureSelection_btn_Checked(object sender, RoutedEventArgs e)
        {
            PictureSelection_btn.IsChecked = !PictureSelection_btn.IsChecked;
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

        private async void PictureSelection_btn_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {


                var file = await PickFileHelper.PickImage();

                if (file != null)
                {
                    SelectedPicture_tbk.Text = file.Name;
                    imageString = await file.ToBase64StringAsync();
                    toManipulate.PicturePj = imageString;
                    SelectedPicture_pp.ProfilePicture = await toManipulate.PicturePj.FromBase64String();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

                PictureSelection_btn.IsChecked = false;
            }
        }

        private void Delete_btn_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private async void Update_btn_Click_1(object sender, RoutedEventArgs e)
        {
            if (!FieldsChecker.CheckName(Title_tb.Text))
            {

                var dialog = new MessageDialog("The Title must be valid");
                await dialog.ShowAsync();
                return;
            }
            else if (!FieldsChecker.OnlyLetters(Description_tb.Text))
            {
                var dialog = new MessageDialog("The description must be valid");
                await dialog.ShowAsync();
                return;
            }

            try
            {
                var ToUpdateProject = new Project { IdPj = toManipulate.IdPj, PicturePj = toManipulate.PicturePj, TitlePj = Title_tb.Text, DescriptionPj = Description_tb.Text, TotalPricePj = int.Parse(TotalPrice_tb.Text), IdP1 = toManipulate.IdP1, IdC1 = toManipulate.IdC1};

                await APIConnection.GetConnection.PutProjectAsync(toManipulate.IdPj, ToUpdateProject);

                var dialog = new MessageDialog("Project updated successfully.");
                await dialog.ShowAsync();
            }
            catch (ProFindServicesException ex)
            {
                if (ex.StatusCode >= 200 && ex.StatusCode <= 205)
                {
                    var dialog = new MessageDialog("Project updated successfully.");
                    await dialog.ShowAsync();
                }
                else
                {
                    var dialog = new MessageDialog("There was an error, try again later.");
                    await dialog.ShowAsync();
                }
            }
            finally
            {
                new InAppNavigationController().NavigateTo(typeof(ReadPage.ReadPage));
            }
        }
    }
}
