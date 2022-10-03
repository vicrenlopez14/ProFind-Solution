using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.ClientNS.Controllers;
using ProFind.Lib.Global.Controllers;
using ProFind.Lib.Global.Helpers;
using ProFind.Lib.Global.Services;
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

namespace ProFind.Lib.ClientNS.Views.CRUDPages.ProposalsNS.CreatePage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class CreatePage : Page
    {
        private string imageString;
        
        Professional IdP;
        public CreatePage()
        {
            this.InitializeComponent();
            AddEvents();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if(e.Parameter != null)
            {
                IdP = (Professional)e.Parameter;
            }
        }
        private async void PictureSelection_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
              

                var file = await PickFileHelper.PickImage();

                if (file != null)
                {
                    SelectedPicture_tbk.Text = file.Name;
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
                
                PictureSelection_btn.IsChecked = false;
            }
        }

        private void Title_tb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void Description_tb_TextChanged(object sender, TextChangedEventArgs e)
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
                var dialog = new MessageDialog("The description must be valid");
                await dialog.ShowAsync();
                return;
            }
            try
            {

                var loggendClient = LoggedClientStore.LoggedClient;
                var toCreatProposals = new Proposal { IdPp = "", TitlePp = Title_tb.Text, DescriptionPp = Description_tb.Text, SuggestedStart = (DateTimeOffset)ExpectedBegin_dp.Date, SuggestedEnd = (DateTimeOffset)Theend.Date, PicturePp = imageString, Seen = false, IdC3 = loggendClient.IdC, IdP3 = IdP.IdP };




                var result = await APIConnection.GetConnection.PostProposalAsync(toCreatProposals);

                var dialog = new MessageDialog("Proposal already sent.");
                await dialog.ShowAsync();

            }
            catch (ProFindServicesException ex)
            {
                if(ex.StatusCode>=200 && ex.StatusCode <= 205)
                {
                    var dialog = new MessageDialog("Proposal already sent.");
                    await dialog.ShowAsync();
                }
                else
                {
                    var dialog = new MessageDialog("There was a problem while sending the proposal, try again later.");
                    await dialog.ShowAsync();
                }
            }
            finally
            {
              new InAppNavigationController().NavigateTo(typeof(ProFind.Lib.ClientNS.Views.CRUDPages.CatalogNS.CatalogList.CatalogList));
            }
        }

        private void Reset_btn_Click(object sender, RoutedEventArgs e)
        {
            Title_tb.Text = "";
            Description_tb.Text = "";

        }

        private void Title_tb_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
        }

        private void Description_tb_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
        }

        private void AddEvents()
        {
            Title_tb.OnEnterNextField();
            Description_tb.OnEnterNextField();
        }

        private void PictureSelection_btn_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
