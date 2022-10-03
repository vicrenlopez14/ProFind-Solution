using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.Global.Helpers;
using ProFind.Lib.Global.Services;
using ProFind.Lib.ProfessionalNS.Controllers;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.ProfessionalNS.Views.CRUDPage.NotificationNS.CreatePage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class CreatePage : Page
    {
        private string imageBytes;
        Project id1;
        Client id2;
        private string imageString;
        private Client ranks2;
        private Notification ToCreateAdmin = new Notification();
        Notification toManipulate = new Notification();
        Professional id; 

        public CreatePage()
        {
            this.InitializeComponent();
            
            AddEvents();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null)
            {
                id1 = e.Parameter as Project;
               
            }
        }

        private async void Create_btn_Click(object sender, RoutedEventArgs e)
        {

            if (!FieldsChecker.CheckName(Title_tb1.Text))
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

                var loggedPro = LoggedProfessionalStore.LoggedProfessional;

                var toCreateClien = new Notification { IdN = "", TitleN = Title_tb1.Text, DescriptionN = Description_tb.Text, PictureN = imageString, DateTimeIssuedN = (DateTimeOffset)DateTimeOffset.Now, IdP1 = loggedPro.IdP, IdPj2 = id1.IdPj };


                var result = await APIConnection.GetConnection.PostNotificationAsync(toCreateClien);



            }
            catch (ProFindServicesException ex)
            {
                if (ex.StatusCode > 200 && ex.StatusCode < 205)
                {
                    var dialog = new MessageDialog("Notification created.");
                    await dialog.ShowAsync();
                }
                else
                {
                    var dialog = new MessageDialog("Try again later please.");
                    await dialog.ShowAsync();
                }
            }
            finally
            {
               
            }

        }


        private async void PictureSelection_btn_Checked(object sender, RoutedEventArgs e)
        {
            
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

       

        private void Title_tb1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Description_tb_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private async void PictureSelection_btn_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedImage = await PickFileHelper.PickImage();
                SelectedPicture_tbk.Text = selectedImage.Name;


                var file = await PickFileHelper.PickImage();

                if (file != null)
                {
                    SelectedPicture_tbk.Text = file.Name;
                    imageBytes = await file.ToBase64StringAsync();

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

        private void Title_tb1_KeyUp(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
        }

        private void Description_tb_KeyUp(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
        }

        private void AddEvents()
        {
            Title_tb1.OnEnterNextField();
            Description_tb.OnEnterNextField();
        }

        private async void Create_btn_Click_2(object sender, RoutedEventArgs e)
        {

            if (!FieldsChecker.CheckName(Title_tb1.Text))
            {

                var dialog = new MessageDialog("The field is empty");
                await dialog.ShowAsync();
                return;
            }
            else if (!FieldsChecker.OnlyLetters(Description_tb.Text))
            {
                var dialog = new MessageDialog("The field is empty");
                await dialog.ShowAsync();
                return;
            }




            try
            {


                var loggedprofesionales = LoggedProfessionalStore.LoggedProfessional;
                var toCreateClien = new Notification { IdN = "", TitleN = Title_tb1.Text, DescriptionN = Description_tb.Text, PictureN = imageString, IdPj2 = id1.IdPj, DateTimeIssuedN = (DateTimeOffset)DateTimeOffset.Now, IdP1 = loggedprofesionales.IdP };

                var result = await APIConnection.GetConnection.PostNotificationAsync(toCreateClien);



            }
            catch (ProFindServicesException ex)
            {
                if (ex.StatusCode >= 200 && ex.StatusCode <= 205)
                {
                    var dialog = new MessageDialog("Notifications was created .");
                    await dialog.ShowAsync();

                }
                else
                {
                    var dialog = new MessageDialog("There was an error creating notifications, please try again later.");
                    await dialog.ShowAsync();
                }
            }
            finally
            {
                new InAppNavigationController().NavigateTo(typeof(ProFind.Lib.ProfessionalNS.Views.CRUDPage.NotificationNS.ReadPage.ReadPage));
            }

        }
    }
}
