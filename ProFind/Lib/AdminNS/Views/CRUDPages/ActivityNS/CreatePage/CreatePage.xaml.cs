using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Controls;
using ProFind.Lib.AdminNS.Views.CRUDPages.ProfessionalNS.CreatePage;
using ProFind.Lib.Global.Controllers;
using ProFind.Lib.Global.Helpers;
using ProFind.Lib.Global.Services;
using ProFind.Lib.Global.Views;
using Rank = ProFind.Lib.Global.Services.Rank;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.ActivityNS.CreatePage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreatePage : Page
    {
       
        private byte[] imageBytes;
        Professional id2; 
        private bool isFirstAdmin = false;
        Project id = new Project();

        public CreatePage()
        {

            this.InitializeComponent();
            

        }

       

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            isFirstAdmin = (bool)e.Parameter;
        }

        private void Hyperlink_Click(Windows.UI.Xaml.Documents.Hyperlink sender,
            Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            new GlobalNavigationController().NavigateTo(typeof(Clients_Login));
        }

        private void Button_Click_4(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            new GlobalNavigationController().NavigateTo(typeof(Lib.ProfessionalNS.Views.InitPage.InitPage));
        }

        private void Professionals_Login_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            new GlobalNavigationController().NavigateTo(typeof(Lib.ProfessionalNS.Views.InitPage.InitPage));
        }

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

        private void Name_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private async void Create_btn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                Creation_pr.IsActive = true;

                var toCreateAdmin = new Activity { IdA = "", TitleA = Title_tb.Text, DescriptionA = Description_tb.Text, ExpectedBeginA = ExpectedBegin_dp.Date, ExpectedEndA = ExpectedEnd_dp.Date, PictureA = imageBytes, IdPj1 = id.IdPj,   };
               

                var result = await APIConnection.GetConnection.PostActivityAsync(toCreateAdmin);
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
        private void ToggleThemeTeachingTip2_ActionButtonClick(TeachingTip sender, object args)
        {
            new GlobalNavigationController().NavigateTo(typeof(ProfessionalInformationAddition), isFirstAdmin);
        }

        private void ToggleThemeTeachingTip2_CloseButtonClick(TeachingTip sender, object args)
        {

        }

        private void ToggleThemeTeachingTip2_Closed(TeachingTip sender, TeachingTipClosedEventArgs args)
        {
            
        }

        private void GoToProfessionals(object sender, RoutedEventArgs e)
        {
            new GlobalNavigationController().NavigateTo(typeof(ProfessionalInformationAddition), isFirstAdmin);

        }

        private void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {

        }

        private void Reset_btn_Click(object sender, RoutedEventArgs e)
        {
            Title_tb.Text = "";
            Description_tb.Text = ""; 

        }
    }
}
