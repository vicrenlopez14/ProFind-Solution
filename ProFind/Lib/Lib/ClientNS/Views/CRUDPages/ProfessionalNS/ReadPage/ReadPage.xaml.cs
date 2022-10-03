using Windows.UI.Xaml.Controls;
using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.Global.Services;
using Professional = ProFind.Lib.Global.Services.Professional;
using ProFind.Lib.Global.Controllers;
using ProFind.Lib.ProfessionalNS.Controllers;
using ProFind.Lib.ClientNS.Controllers;
using System.Linq;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.ClientNS.Views.CRUDPages.ProfessionalNS.ReadPage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class ReadPage : Page
    {

        public ReadPage()
        {
            this.InitializeComponent();
            GetProjectsList();
        }


        public async void GetProjectsList()

        {
            
            var loggedClient = LoggedClientStore.LoggedClient;
            var relatedProf = (from Project in loggedClient.Projects
                               where Project.IdC1 == loggedClient.IdC
                               select Project.IdP1Navigation).ToList();

            DashboardProfessionalsActiveListView.ItemsSource = relatedProf;
        }

        private void Control2_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            // Only the professionals related to this client
            var loggedClient = LoggedClientStore.LoggedClient;
            var relatedProf = (from Project in loggedClient.Projects
                               where Project.IdC1 == loggedClient.IdC
                               select Project.IdP1Navigation).Where(p => p.NameP.Contains(sender.Text)).ToList();

            DashboardProfessionalsActiveListView.ItemsSource = relatedProf;
        }

        private void Button_Click_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            new InAppNavigationController().NavigateTo(typeof(ProFind.Lib.ClientNS.Views.CRUDPages.ProfessionalNS.SearchPage.SearchPageP));
        }

        private void DashboardProfessionalsActiveListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Professional selectedProfessional = e.ClickedItem as Professional;

            new InAppNavigationController().NavigateTo(typeof(ProFind.Lib.ClientNS.Views.CRUDPages.ProfessionalNS.ReadPage.ReadPage), selectedProfessional);
        }

       
    }
}
