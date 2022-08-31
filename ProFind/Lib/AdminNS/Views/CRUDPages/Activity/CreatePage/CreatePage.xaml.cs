using Microsoft.UI.Xaml.Controls;
using ProFind.Lib.Global.Controllers;
using ProFind.Lib.Global.Helpers;
using ProFind.Lib.Global.Services.Models;
using ProFind.Lib.Global.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.Activity.CreatePage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class CreatePage : Page
    {

        private List<Rank> ranks = new List<Rank>();
        private List<string> rankStrings = new List<string>();
        private byte[] imageBytes;
        private bool isFirstAdmin = false;

        public CreatePage()
        {

            this.InitializeComponent();
            loadUsefulThings();

        }

        public async void loadUsefulThings()
        {
            ranks = (List<Rank>)await new RankService().ListObjectAsync();

            foreach (var rank in ranks)
            {
                rankStrings.Add(rank.NameR);
            }

            Rank_cb.ItemsSource = null;
            Rank_cb.ItemsSource = rankStrings;

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
            new GlobalNavigationController().NavigateTo(typeof(InitPage.InitPage));
        }

        private void Professionals_Login_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            new GlobalNavigationController().NavigateTo(typeof(Professional.View.InitPage.InitPage));
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

                    SelectedPicture_pp.ProfilePicture = imageBytes.ToBitmapImage();
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
            SelectedPicture_pp.DisplayName = Name_tb.Text;
        }

        private async void Create_btn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                Creation_pr.IsActive = true;

                var toCreateAdmin = new Admin(Name_tb.Text, Email_tb.Text, PhoneNumber_tb.Text, Password_pb.Password, "", imageBytes);
                toCreateAdmin.IdR1 = ranKS[Rank_cb.SelectedIndex].IdR;

                var result = await new PAdminService().Create(toCreateAdmin);

                if (result == System.Net.HttpStatusCode.OK)
                {
                    ToggleThemeTeachingTip2.IsOpen = true;
                }
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
            new GlobalNavigationController().NavigateTo(typeof(Professional_Add.ProfessionalInformationAddition), isFirstAdmin);
        }

        private void ToggleThemeTeachingTip2_CloseButtonClick(TeachingTip sender, object args)
        {

        }

        private void ToggleThemeTeachingTip2_Closed(TeachingTip sender, TeachingTipClosedEventArgs args)
        {
            CreateProfessionals_btn.Visibility = Visibility.Visible;
        }

        private void GoToProfessionals(object sender, RoutedEventArgs e)
        {
            new GlobalNavigationController().NavigateTo(typeof(Professional_Add.ProfessionalInformationAddition), isFirstAdmin);

        }
    }
}
