﻿using System;
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
using Admin = ProFind.Lib.Global.Services.Admin;
using Rank = ProFind.Lib.Global.Services.Rank;


// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.AdminNS.CreatePage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class CreatePage : Page
    {
        private List<Rank> ranks = new List<Rank>();
        private byte[] imageBytes;
        private bool isFirstAdmin = false;

        public CreatePage()
        {

            this.InitializeComponent();
            loadUsefulThings();

        }

        public async void loadUsefulThings()
        {
            ranks = await APIConnection.GetConnection.GetRanksAsync() as List<Rank>;

            Rank_cb.ItemsSource = ranks;
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

                var toCreateAdmin = new Admin{IdA = "", NameA = Name_tb.Text, EmailA = Email_tb.Text, PasswordA = Password_pb.Password, IdR1 = int.Parse(Rank_cb.Text)  , TelA = PhoneNumber_tb.Text, PictureA = imageBytes  };
                toCreateAdmin.IdR1 = int.Parse((Rank_cb.SelectedItem as Rank).IdR.ToString());

                var result = await APIConnection.GetConnection.PostAdminAsync(toCreateAdmin);

                ToggleThemeTeachingTip2.IsOpen = true;
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
            CreateProfessionals_btn.Visibility = Visibility.Visible;
        }

        private void GoToProfessionals(object sender, RoutedEventArgs e)
        {
            new GlobalNavigationController().NavigateTo(typeof(ProfessionalInformationAddition), isFirstAdmin);

        }
    }
}
