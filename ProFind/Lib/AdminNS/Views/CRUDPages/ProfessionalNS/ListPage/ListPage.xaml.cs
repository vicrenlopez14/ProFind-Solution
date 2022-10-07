using Windows.UI.Xaml.Controls;
using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.ClientNS.Views.InitPage;
using ProFind.Lib.Global.Services;
using Professional = ProFind.Lib.Global.Services.Professional;
using System.Collections.Generic;
using Windows.UI.Popups;
using System;
using System.Linq;
using ProFind.Lib.Global.Helpers;


// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.ProfessionalNS.ListPage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class ReadPage : Page
    {
        //Professional Id1 = new Professional();
        private List<Professional> professionalsListObj = new List<Professional>();

        public ReadPage()
        {
            this.InitializeComponent();
            GetProfessionalsList();
        }
        public async void GetProfessionalsList()
        {
            professionalsListObj = await APIConnection.GetConnection.GetProfessionalsAsync() as List<Professional>;

            ProfessionalsListView.ItemsSource = professionalsListObj;
        }

        private async void ProjectsActiveListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Professional professional = e.ClickedItem as Professional;

            new InAppNavigationController().NavigateTo(typeof(Lib.AdminNS.Views.CRUDPages.ProfessionalNS.ListPage.ReadPage), professional);
        }

        private async void SearchBox_QueryChanged(SearchBox sender, SearchBoxQueryChangedEventArgs args)
        {
            var newList = professionalsListObj.Where(x => x.NameP.ToLower().Contains(sender.QueryText.ToLower()));

            ProfessionalsListView.ItemsSource = null;
            ProfessionalsListView.ItemsSource = newList;
        }

        private async void Delete_Click_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                if (ProfessionalsListView.SelectedItem != null)
                {
                    var obj = ProfessionalsListView.SelectedItem as Professional;
                    await APIConnection.GetConnection.DeleteProfessionalAsync(obj.IdP);
                    var dialog = new MessageDialog("Professional has been deleted");
                    await dialog.ShowAsync();
                }
                else
                {
                    // Validation content dialog
                    var dialog = new MessageDialog("You have to select a Professional.");
                    await dialog.ShowAsync();

                }

            }
            catch (ProFindServicesException ex)
            {
                if (ex.StatusCode == 204 || ex.StatusCode == 202 || ex.StatusCode == 200 || ex.StatusCode == 405)
                {
                    var dialog = new MessageDialog("Professional has been deleted");
                    await dialog.ShowAsync();
                }
                else
                {
                    var dialog = new MessageDialog(ex.Message);
                    await dialog.ShowAsync();
                }
            }
            finally
            {
                GetProfessionalsList();
            }
        }

        private async void Update_Click_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (ProfessionalsListView.SelectedItem != null)
            {
                var selectedProfessional = ProfessionalsListView.SelectedItem as Professional;
                new InAppNavigationController().NavigateTo(typeof(Lib.AdminNS.Views.CRUDPages.ProfessionalNS.UpdatePage.Editar_User), selectedProfessional);
            }
            else
            {
                // Validation content dialog
                var dialog = new MessageDialog("You have to select a Professional.");
                await dialog.ShowAsync();

            }
        }

        private void Add_Click_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            new InAppNavigationController().NavigateTo(typeof(Lib.AdminNS.Views.CRUDPages.ProfessionalNS.CreatePage.ProfessionalInformationAddition));
        }

        private void AppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            URLOpenerUtil.OpenURL(@"https://reporter.profind.work/Report/RegisteredProfessionals");
        }

        private async void ProfessionalsListView_DoubleTapped(object sender, Windows.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {
            if (ProfessionalsListView.SelectedItem != null)
            {
                var selectedProfessional = ProfessionalsListView.SelectedItem as Professional;
                new InAppNavigationController().NavigateTo(typeof(Lib.AdminNS.Views.CRUDPages.ProfessionalNS.ReadPage.ReadPage), selectedProfessional);
                // Validation content dialog
                //var dialog = new MessageDialog("temporary disabled");
                //await dialog.ShowAsync();
            }
            else
            {
                // Validation content dialog
                var dialog = new MessageDialog("You have to select a Professional.");
                await dialog.ShowAsync();

            }
        }

        private void AppBarButton_Click_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            GetProfessionalsList();
        }
    }
}
