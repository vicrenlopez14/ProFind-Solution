using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.Global.Controllers;
using ProFind.Lib.Global.Helpers;
using ProFind.Lib.Global.Services;
using ProFind.Lib.ProfessionalNS.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.ProfessionalNS.Views.CRUDPage.ProposalNS.Accept_or_Deny
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class AcceptOrDeny : Page
    {
        private string imageString;
        Professional Id;
        Proposal Denegada;

        private Proposal InComingProposal;


        public AcceptOrDeny()
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null)
            {
                InComingProposal = (Proposal)e.Parameter;
            }
            Title_tb.Text = InComingProposal.TitlePp;
            Description_tb.Text = InComingProposal.DescriptionPp;
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
                    SelectedPicture_tbk.Text = file.Name;
                    SelectedPicture_pp.ProfilePicture = await imageString.FromBase64String();
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

        private void Title_tb_KeyDown(object sender, KeyRoutedEventArgs e)
        {
        }

        private void Description_tb_KeyDown(object sender, KeyRoutedEventArgs e)
        {
        }

        private void TotalPrice_tb_KeyDown(object sender, KeyRoutedEventArgs e)
        {

        }

        private async void Create_btn_Click(object sender, RoutedEventArgs e)
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
                var LoggendPro = LoggedProfessionalStore.LoggedProfessional;
                var toCreateClien = new Project { IdPj = "", TitlePj = Title_tb.Text, DescriptionPj = Description_tb.Text, PicturePj = imageString, TotalPricePj = int.Parse(TotalPrice_tb.Text), IsPaidPj = false, IdP1 = LoggendPro.IdP, IdC1 = InComingProposal.IdC3 };
                toCreateClien.TagDurationPj = Tag_cb.SelectedIndex + 1;
                var result = await APIConnection.GetConnection.PostProjectAsync(toCreateClien);
                var dialog = new MessageDialog("The proposal was accepted and project created.");
                await dialog.ShowAsync();
                DeleteProposal();
            }
            catch (ProFindServicesException ex)
            {
                if (ex.StatusCode >= 200 && ex.StatusCode <= 205)
                {
                    var dialog = new MessageDialog("The proposal was accepted.");
                    await dialog.ShowAsync();
                    DeleteProposal();
                }
                else
                {
                    var dialog = new MessageDialog("There was a problem while accepting the proposal, try again later.");
                    await dialog.ShowAsync();
                }
            }
            finally
            {
                new InAppNavigationController().NavigateTo(typeof(ProFind.Lib.AdminNS.Views.CRUDPages.ProjectNS.ReadPage.ReadPage));
            }
        }


        private async void DeleteProposal()
        {
            try
            {
                await APIConnection.GetConnection.DeleteProposalAsync(id: InComingProposal.IdPp);

            }
            catch (ProFindServicesException ex)
            {
                if (ex.StatusCode >= 200 && ex.StatusCode <= 205)
                {
                }
                else
                {
                    var dialog = new MessageDialog("There was a problem while deleting the proposal, even so, the project was crated, try again later.");
                    await dialog.ShowAsync();
                }
            }
            finally
            {
                new InAppNavigationController().NavigateTo(typeof(ReadPage.ReadPage));
            }
        }

        private async void Decline_Click(object sender, RoutedEventArgs e)
        {
            DeleteProposal();
        }

        private void Tag_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void PictureSelection_btn_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
