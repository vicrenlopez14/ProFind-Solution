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

namespace ProFind.Lib.ClientNS.Views.CRUDPages.ProposalsNS.UpdatePage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class UpdatePage : Page
    {
        private string imageString;

        Proposal ToManipulate;
        public UpdatePage()
        {
            this.InitializeComponent();
            AddEvents();
        }
        private void AddEvents()
        {
            Title_tb.OnEnterNextField();
            Description_tb.OnEnterNextField();

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null)
            {
                ToManipulate = (Proposal)e.Parameter;
            }
        }

        private async void LoadData()
        {
            Title_tb.Text = ToManipulate.TitlePp;
            Description_tb.Text = ToManipulate.DescriptionPp;
            Theend.Date = (DateTimeOffset)ToManipulate.SuggestedEnd;
            SelectedPicture_pp.ProfilePicture = await ToManipulate.PicturePp.FromBase64String();
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
                    SelectedPicture_pp.ProfilePicture = await ToManipulate.PicturePp.FromBase64String();

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

        private void Title_tb_KeyDown(object sender, KeyRoutedEventArgs e)
        {

        }

        private void Description_tb_KeyDown(object sender, KeyRoutedEventArgs e)
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

                var loggedClient = LoggedClientStore.LoggedClient;
                var toCreateAdmin = new Proposal { TitlePp = Title_tb.Text, DescriptionPp = Description_tb.Text, SuggestedEnd = (DateTimeOffset)Theend.Date, PicturePp = imageString, IdPp = ToManipulate.IdPp, IdC3 = loggedClient.IdC };




                await APIConnection.GetConnection.PutProposalAsync(ToManipulate.IdPp, toCreateAdmin);
                new GlobalNavigationController().NavigateTo(typeof(ProFind.Lib.ClientNS.Views.CRUDPages.ProposalsNS.ListPage.ListPAge));


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

            }
        }

        private async void Reset_btn_Click(object sender, RoutedEventArgs e)
        {
            await APIConnection.GetConnection.DeleteProposalAsync(ToManipulate.IdPp);
            new GlobalNavigationController().NavigateTo(typeof(ProFind.Lib.ClientNS.Views.CRUDPages.ProposalsNS.ListPage.ListPAge));
        }

        private void Create_btn_Click_12(object sender, RoutedEventArgs e)
        {
            new InAppNavigationController().GoBack();
        }

        private async void PictureSelection_btn_Checked(object sender, RoutedEventArgs e)
        {

            try
            {


                var file = await PickFileHelper.PickImage();

                if (file != null)
                {
                    SelectedPicture_tbk.Text = file.Name;
                    imageString = await file.ToBase64StringAsync();
                    SelectedPicture_pp.ProfilePicture = await ToManipulate.PicturePp.FromBase64String();

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
    }
}
