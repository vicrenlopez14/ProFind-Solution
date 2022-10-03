using ProFind.Lib.AdminNS.Controllers;
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

namespace ProFind.Lib.ClientNS.Views.CRUDPages.CatalogNS.CatalogList
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CatalogList : Page
    {
        public CatalogList()
        {
            this.InitializeComponent();

            GetProfessionalsList();
        }

        public async void GetProfessionalsList()
        {
            ProfessionalsListView.ItemsSource = await APIConnection.GetConnection.GetProfessionalsAsync() as List<Professional>;
        }

        private async void ProjectsActiveListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Professional professional = e.ClickedItem as Professional;

            new InAppNavigationController().NavigateTo(typeof(Lib.AdminNS.Views.CRUDPages.ProfessionalNS.ListPage.ReadPage), professional);
        }

        private void Button_Click_2(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            new InAppNavigationController().NavigateTo(typeof(Lib.AdminNS.Views.CRUDPages.ProfessionalNS.CreatePage.ProfessionalInformationAddition));
        }

        private void CreateProposalBtn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (ProfessionalsListView.SelectedItem != null)
            {
                var selectedProfessional = ProfessionalsListView.SelectedItem as Professional;
                new InAppNavigationController().NavigateTo(typeof(Lib.ClientNS.Views.CRUDPages.ProposalsNS.CreatePage.CreatePage), selectedProfessional);
            }


        }

        private void UpdateProfessionalBtn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (ProfessionalsListView.SelectedItem != null)
            {
                var selectedProfessional = ProfessionalsListView.SelectedItem as Professional;
                new InAppNavigationController().NavigateTo(typeof(Lib.AdminNS.Views.CRUDPages.ProfessionalNS.UpdatePage.Editar_User), selectedProfessional);
            }
        }

        private async void DeleteProfessionalBtn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                var obj = ProfessionalsListView.SelectedItem as Professional;
                await APIConnection.GetConnection.DeleteProfessionalAsync(obj.IdP);
                var dialog = new MessageDialog("Professional has been deleted");
                await dialog.ShowAsync();

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

        private void Add_Click_1(object sender, RoutedEventArgs e)
        {
            if (ProfessionalsListView.SelectedItem != null)
            {
                var selectedProfessional = ProfessionalsListView.SelectedItem as Professional;
                new InAppNavigationController().NavigateTo(typeof(Lib.ClientNS.Views.CRUDPages.ProposalsNS.CreatePage.CreatePage), selectedProfessional);
            }
        }
    }
}
