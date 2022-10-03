using ProFind.Lib.Global.Services;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.AdminNS.Views.CRUDPages.AdminNS.CreatePage;
using Proposal = ProFind.Lib.Global.Services.Proposal;
using Windows.UI.Popups;
using System;
using System.Linq;
using ProFind.Lib.ProfessionalNS.Controllers;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.ProfessionalNS.Views.CRUDPage.ProposalNS.ReadPage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class ReadPage : Page
    {
       
        public ReadPage()
        {
            this.InitializeComponent();

            InitializeData();
        }

        private async void InitializeData()
        {
            var result = await APIConnection.GetConnection.GetProposalsAsync();
            var projectsOfThisProfessional = (from pp in result.ToList()
                                                   where pp.IdP3 == LoggedProfessionalStore.LoggedProfessional.IdP
                                                   select pp).ToList();
            Activities_lw.ItemsSource = projectsOfThisProfessional;
        }

        private void ProposalListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var proposal = e.ClickedItem as Proposal;

        }

       

    

        private async void Add_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Activities_lw.SelectedItem != null)
                {
                    Proposal obj = Activities_lw.SelectedItem as Proposal;
                    await APIConnection.GetConnection.DeleteProposalAsync(obj.IdPp);
                }

                else
                {

                    var dialog = new MessageDialog("You have to select a proposal.");
                    await dialog.ShowAsync();
                }
               
               
            }
            catch (ProFindServicesException ex)
            {
                if (ex.StatusCode >= 200 && ex.StatusCode <= 205)
                {
                    var dialog = new MessageDialog("The proposal has been deleted");
                    await dialog.ShowAsync();
                }
                else
                {
                    var dialog = new MessageDialog("There was a problem while eliminating the proposal, try again later.");
                    await dialog.ShowAsync();
                }
            }
            finally
            {
                InitializeData();
            }
        }

        private async void Delete_Click_1(object sender, RoutedEventArgs e)
        {
            if (Activities_lw.SelectedItem != null)
            {
                Proposal lectedProposal = Activities_lw.SelectedItem as Proposal;
                new InAppNavigationController().NavigateTo(typeof(ProFind.Lib.ProfessionalNS.Views.CRUDPage.ProposalNS.Accept_or_Deny.AcceptOrDeny), lectedProposal) ;
            }
            else
            {
                // Validation content dialog
                var dialog = new MessageDialog("You have to select a Activity.");
                await dialog.ShowAsync();

            }
           
        }
    }
}
