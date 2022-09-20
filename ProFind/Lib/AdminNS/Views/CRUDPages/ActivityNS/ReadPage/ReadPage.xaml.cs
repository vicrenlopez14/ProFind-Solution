using ProFind.Lib.Global.Controllers;
using ProFind.Lib.Global.Helpers;
using ProFind.Lib.Global.Services;
using System;
using Windows.UI.Xaml.Controls;
using Activity = ProFind.Lib.Global.Services.Activity;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.ActivityNS.ReadPage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ReadPage : Page
    {
        private byte[] imageBytes;
        Professional id2;
        Activity id1; 
        private bool isFirstAdmin = false;
        Project id = new Project();
        public ReadPage()
        {
            this.InitializeComponent();
        }

        public Activity ModelActivity { get; set; }

        private async void PictureSelection_btn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                Creation_pr.IsActive = true;

                var file = await PickFileHelper.PickImage();

                if (file != null)
                {
                    SelectedPicture_tbk.Text = file.Name;
                    imageBytes = await file.ToByteArrayAsync();

                    //SelectedPicture_pp.ProfilePicture = imageBytes.ToBitmapImage();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Creation_pr.IsActive = false;
                PictureSelection_btn.IsChecked = false;
            }
        }

        private void Description_tb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Title_tb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private async void Update_btn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                Creation_pr.IsActive = true;

                var toCreateAdmin = new Activity {  TitleA = Title_tb.Text, DescriptionA = Description_tb.Text, ExpectedBeginA = ExpectedBegin_dp.Date, ExpectedEndA = ExpectedEnd_dp.Date, PictureA = imageBytes };



                await APIConnection.GetConnection.PutActivityAsync(id1.IdA,toCreateAdmin);
                new GlobalNavigationController().NavigateTo(typeof(ProFind.Lib.AdminNS.Views.CRUDPages.ActivityNS.ListPage.ListPage));


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Creation_pr.IsActive = false;
            }
        }

        private void Reset_btn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Title_tb.Text = "";
            Description_tb.Text = "";
        }

        private async void Delete_btn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                Creation_pr.IsActive = true;

              



                await APIConnection.GetConnection.DeleteActivityAsync(id1.IdA);
                new GlobalNavigationController().NavigateTo(typeof(ProFind.Lib.AdminNS.Views.CRUDPages.ActivityNS.ListPage.ListPage));


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Creation_pr.IsActive = false;
            }
        }
    }
}
