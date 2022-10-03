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
using Admin = ProFind.Lib.Global.Services.Admin;
using Rank = ProFind.Lib.Global.Services.Rank;
using ProFind.Lib.AdminNS.Controllers;
using Windows.UI.Popups;
using System.Linq;
using System.Threading.Tasks;


// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.AdminNS.CreatePage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class CreatePage : Page
    {
        private Admin ToCreateAdmin = new Admin();
        private List<Rank> ranks = new List<Rank>();
        private string imageString;
        private bool isFirstAdmin = false;

        public CreatePage()
        {

            this.InitializeComponent();
            loadUsefulThings();
            AddEvents();

        }
        private void AddEvents()
        {
            Name_tb.OnEnterNextField();
            Email_tb.OnEnterNextField();
            PhoneNumber_tb.OnEnterNextField();
            Password_pb.OnEnterNextField();

        }

        public async void loadUsefulThings()
        {
            ranks = await APIConnection.GetConnection.GetRanksAsync() as List<Rank>;

            if (isFirstAdmin)
            {
                {
                    ranks = ranks.Where(x => x.NameR == "Principal").ToList();
                    Rank_cb.IsEnabled = false;
                    Rank_cb.SelectedIndex = 0;
                }
            }

            Rank_cb.ItemsSource = ranks;
            ToCreateAdmin = new Admin();
        }



        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter != null)
                isFirstAdmin = (bool)e.Parameter;
        }


        private void ToggleThemeTeachingTip2_Closed(TeachingTip sender, TeachingTipClosedEventArgs args)
        {
            CreateProfessionals_btn.Visibility = Visibility.Visible;
        }

        private void GoToProfessionals(object sender, RoutedEventArgs e)
        {
            new GlobalNavigationController().NavigateTo(typeof(ProfessionalInformationAddition), isFirstAdmin);

        }

        private void Name_tb_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            ToCreateAdmin.NameA = Name_tb.Text;
        }

        private void Email_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ToCreateAdmin.EmailA = Email_tb.Text;
        }

        private void PhoneNumber_tb_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
        {
            ToCreateAdmin.TelA = PhoneNumber_tb.Text;
        }

        private void Password_pb_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ToCreateAdmin.PasswordA = Password_pb.Password;
        }

        private void PictureSelection_btn_Checked(object sender, RoutedEventArgs e)
        {

        }


        private void Rank_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedRank = Rank_cb.SelectedItem as Rank;
            ToCreateAdmin.IdR1 = selectedRank.IdR;
        }

        private async void Create_btn_Click_1(object sender, RoutedEventArgs e)
        {
            if (!FieldsChecker.CheckEmail(Email_tb.Text))
            {
                var dialog = new MessageDialog("The email must be valid.");
                await dialog.ShowAsync();
                return;
            }
            if (!FieldsChecker.CheckPassword(Password_pb.Password))
            {
                var dialog = new MessageDialog("The password must be valid.");
                await dialog.ShowAsync();
                return;
            }
            if (!FieldsChecker.CheckName(Name_tb.Text))
            {
                var dialog = new MessageDialog("The name must be valid.");
                await dialog.ShowAsync();
                return;
            }
            try
            {
                Creation_pr.IsActive = true;

                ToCreateAdmin.IdA = "";
                ToCreateAdmin.TelA = PhoneNumber_tb.Text;
                ToCreateAdmin.NameA = Name_tb.Text;
                ToCreateAdmin.EmailA = Email_tb.Text;
                ToCreateAdmin.PasswordA = Password_pb.Password;
                ToCreateAdmin.IdR1 = (Rank_cb.SelectedItem as Rank).IdR;

                var result = await APIConnection.GetConnection.PostAdminAsync(ToCreateAdmin);

                if (isFirstAdmin)
                    ToggleThemeTeachingTip2.IsOpen = true;
                else
                    new InAppNavigationController().GoBack();

            }
            catch (ProFindServicesException ex)
            {
                if (ex.StatusCode == 201)
                {
                    if (isFirstAdmin)
                        ToggleThemeTeachingTip2.IsOpen = true;
                    else
                        new InAppNavigationController().GoBack();
                }
                else if (ex.StatusCode == 409)
                {
                    // Error message dialog
                    var dialog = new MessageDialog("This email is already registed, please try with another one.");
                    await dialog.ShowAsync();
                }
                else
                {
                    {
                        // Error message dialog
                        var dialog = new MessageDialog("An error has ocurred, please try again later.");
                        await dialog.ShowAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                // Error message dialog
                var dialog = new MessageDialog("An unexpected error has ocurred, please try again later.");
                await dialog.ShowAsync();

            }
            finally
            {
                Creation_pr.IsActive = false;
            }
        }

        private void ToggleThemeTeachingTip2_ActionButtonClick_1(TeachingTip sender, object args)
        {
            new GlobalNavigationController().NavigateTo(typeof(Int_Page.Int_Page));

        }


        private void CreateProfessionals_btn_Click(object sender, RoutedEventArgs e)
        {
            new GlobalNavigationController().NavigateTo(typeof(Lib.ProfessionalNS.Views.InitPage.InitPage));

        }

        private void Name_tb_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {

        }

        private void Email_tb_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {

        }

        private void PhoneNumber_tb_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {

        }

        private void PictureSelection_btn_Checked_1(object sender, RoutedEventArgs e)
        {

        }

        private void Name_tb_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {

        }

        public async void PictureSelection_btn_Checked_2(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedImage = await PickFileHelper.PickImage();
                if (selectedImage != null)
                {
                    SelectedPicture_tbk.Text = selectedImage.Name;

                    imageString = await selectedImage.ToBase64StringAsync();
                    ToCreateAdmin.PictureA = imageString;
                    SelectedPicture_pp.ProfilePicture = await imageString.FromBase64String();
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
                Creation_pr.IsActive = false;
                PictureSelection_btn.IsChecked = false;
            }
        }
    }
}
