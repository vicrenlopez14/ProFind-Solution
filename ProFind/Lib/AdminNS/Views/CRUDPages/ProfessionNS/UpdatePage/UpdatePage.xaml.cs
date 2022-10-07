using ProFind.Lib.AdminNS.Controllers;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.ProfessionNS.UpdatePage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UpdatePage : Page
    {
        private string picturestring;
        private string bannerstring;
        private Profession tomanipulateprofession;
        public UpdatePage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null)
            {
                tomanipulateprofession = e.Parameter as Profession;
                loadUsefulthings();


            }
            else
            {
                var dialog = new MessageDialog("The profession no valid");
                await dialog.ShowAsync();
            }
        }
        private async void loadUsefulthings()
        {
            Name_tb.Text = tomanipulateprofession.NamePfs;
            Description_tb.Text = tomanipulateprofession.DescriptionPfs ?? "";
            ProfessionBanner_img.Source = await tomanipulateprofession.BannerPfs.FromBase64String();
            ProfessionPicture_img.Source = await tomanipulateprofession.PicturePfs.FromBase64String();
            bannerstring = tomanipulateprofession.BannerPfs;
            picturestring = tomanipulateprofession.PicturePfs;


        }
        private async void Create_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Title_tb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Name_tb_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {


        }

        private async void Create_btn_Click_1(object sender, RoutedEventArgs e)
        {
            if (!FieldsChecker.CheckName(Name_tb.Text))
            {
                var dialog = new MessageDialog("The name must be valid");
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
                var toupdateprofession = new Profession { IdPfs = tomanipulateprofession.IdPfs, NamePfs = Name_tb.Text, DescriptionPfs = Description_tb.Text, BannerPfs = bannerstring, PicturePfs = picturestring };
                await APIConnection.GetConnection.PutProfessionAsync(toupdateprofession.IdPfs.Value, toupdateprofession);
                var dialog = new MessageDialog("The profession has been updated");
                await dialog.ShowAsync();
            }
            catch (ProFindServicesException ex)
            {
                if (ex.StatusCode >= 200 && ex.StatusCode <= 205)
                {
                    var dialog = new MessageDialog("The profession has been updated");
                    await dialog.ShowAsync();
                }
                else
                {
                    var dialog = new MessageDialog("There was a problem while updating the profession, try again later.");
                    await dialog.ShowAsync();
                }
            }
            finally
            {
                new InAppNavigationController().NavigateTo(typeof(ListPage.List_Page));
            }
        }

        private async void LoadPicture_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var selectedImage = await PickFileHelper.PickImage();
                if (selectedImage != null)
                {
                    picturestring = await selectedImage.ToBase64StringAsync();

                    ProfessionPicture_img.Source = await picturestring.FromBase64String();
                }


            }
            catch (Exception ex)
            {
                // friendly error dialog
                var dialog = new MessageDialog("Image has not been loaded");
                await dialog.ShowAsync();

                Console.WriteLine(ex.Message);
            }
            finally
            {
            }
        }

        private async void LoadBanner_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var selectedbanner = await PickFileHelper.PickImage();
                if (selectedbanner != null)
                {
                    bannerstring = await selectedbanner.ToBase64StringAsync();

                    ProfessionBanner_img.Source = await bannerstring.FromBase64String();
                }
            }
            catch (Exception ex)
            {
                // friendly error dialog
                var dialog = new MessageDialog("Banner has not been loaded");
                await dialog.ShowAsync();

                Console.WriteLine(ex.Message);
            }
            finally
            {
            }
        }
    }
}
