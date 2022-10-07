using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.ClientNS.Controllers;
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

namespace ProFind.Lib.ClientNS.Views.CRUDPages.ProposalsNS.ListPage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class ListPAge : Page
    {
        private List<Proposal> proposalsListObj = new List<Proposal>();
        Proposal Id;
        public ListPAge()
        {
            this.InitializeComponent();
            InitializeData();
        }
        private async void InitializeData()
        {
            try
            {
                proposalsListObj = await APIConnection.GetConnection.GetProposalsFromClientAsync(LoggedClientStore.LoggedClient.IdC) as List<Proposal>;

                Proposals_lw.ItemsSource = proposalsListObj;
            }
            catch
            {
                await new MessageDialog("Couldn't load proposals list. Please try again later.").ShowAsync();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {


        }

        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new InAppNavigationController().NavigateTo(typeof(ProFind.Lib.ClientNS.Views.CRUDPages.ProposalsNS.CreatePage.CreatePage));
        }


        private async void Delete_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Proposals_lw.SelectedItem != null)
                {
                    var obj = Proposals_lw.SelectedItem as Proposal;
                    await APIConnection.GetConnection.DeleteProposalAsync(obj.IdPp);
                    var dialog = new MessageDialog("Proposal deleted successfully.");
                    await dialog.ShowAsync();

                }
                else
                {
                    // Validation content dialog
                    var dialog = new MessageDialog("You have to select a Proposal.");
                    await dialog.ShowAsync();

                }

            }
            catch (ProFindServicesException ex)
            {
                if (ex.StatusCode == 204 || ex.StatusCode == 200 || ex.StatusCode == 201)
                {
                    var dialog = new MessageDialog("Proposal deleted successfully.");
                    await dialog.ShowAsync();
                }
                else
                {
                    var dialog = new MessageDialog("You have to select an Proposal.");
                    await dialog.ShowAsync();
                }
            }
            finally
            {
                InitializeData();
            }
        }

        private async void Update_Click_1(object sender, RoutedEventArgs e)
        {

            if (Proposals_lw.SelectedItem != null)
            {
                Proposal selectedProject = Proposals_lw.SelectedItem as Proposal;

                new InAppNavigationController().NavigateTo(typeof(ProFind.Lib.ClientNS.Views.CRUDPages.ProposalsNS.UpdatePage.UpdatePage), selectedProject);
            }
            else
            {
                // Validation content dialog
                var dialog = new MessageDialog("You have to select a Proposal.");
                await dialog.ShowAsync();

            }
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            InitializeData();
        }

        private void SearchBox_QueryChanged(SearchBox sender, SearchBoxQueryChangedEventArgs args)
        {
            var newList = proposalsListObj.Where(x => x.TitlePp.ToLower().Contains(sender.QueryText.ToLower()));

            Proposals_lw.ItemsSource = null;
            Proposals_lw.ItemsSource = newList;
        }
    }
}
