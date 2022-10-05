using Windows.UI.Xaml.Controls;
using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.Global.Services;
using Professional = ProFind.Lib.Global.Services.Professional;
using ProFind.Lib.Global.Controllers;
using ProFind.Lib.ProfessionalNS.Controllers;
using ProFind.Lib.ClientNS.Controllers;
using System.Linq;
using System.Collections.Generic;
using Windows.UI.Popups;
using System;

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

            // Major lists
            var projects = await APIConnection.GetConnection.GetProjectsAsync();
            var clients = await APIConnection.GetConnection.GetProfessionalsAsync();

            // Projects where loggedProfessional is related
            var relatedProjects = projects.Where(p => p.IdC1 == loggedClient.IdC).ToList();

            // Notifications where loggedProfessional is related through a project
            var relateProfessional = new List<Professional>();
            foreach (var project in projects)
            {
                var relatedProfessionalForThisProject = clients.Where(n => n.IdP == project.IdP1).ToList();
                relateProfessional.AddRange(relatedProfessionalForThisProject);
            }

            ProfessionalsListView.ItemsSource = relateProfessional.Distinct().ToList();
        }

        private void Control2_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
           
            var loggedClient = LoggedClientStore.LoggedClient;
            var relatedProf = (from Project in loggedClient.Projects
                               where Project.IdC1 == loggedClient.IdC
                               select Project.IdP1Navigation).Where(p => p.NameP.Contains(sender.Text)).ToList();

            ProfessionalsListView.ItemsSource = relatedProf;
        }

        private void Button_Click_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            new InAppNavigationController().NavigateTo(typeof(ProFind.Lib.ClientNS.Views.CRUDPages.ProfessionalNS.SearchPage.SearchPageP));
        }

        private async void SearchBox_QueryChanged(SearchBox sender, SearchBoxQueryChangedEventArgs args)
        {

            var loggedClient = LoggedClientStore.LoggedClient;

            // Major lists
            var projects = await APIConnection.GetConnection.GetProjectsAsync();
            var clients = await APIConnection.GetConnection.GetProfessionalsAsync();

            // Projects where loggedProfessional is related
            var relatedProjects = projects.Where(p => p.IdC1 == loggedClient.IdC).ToList();

            // Notifications where loggedProfessional is related through a project
            var relateProfessional = new List<Professional>();
            foreach (var project in projects)
            {
                var relatedProfessionalForThisProject = clients.Where(n => n.IdP == project.IdP1).ToList();
                relateProfessional.AddRange(relatedProfessionalForThisProject);
            }

            var filteredList = relateProfessional.Distinct().ToList();


            if (string.IsNullOrEmpty(sender.QueryText))
            {
                ProfessionalsListView.ItemsSource = null;
                ProfessionalsListView.ItemsSource = filteredList;
                return;
            }

            var newList = filteredList.Where(x => x.NameP.Contains(sender.QueryText));

            ProfessionalsListView.ItemsSource = null;
            ProfessionalsListView.ItemsSource = newList;
        }

        private async void ProfessionalsListView_DoubleTapped(object sender, Windows.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {
            if (ProfessionalsListView.SelectedItem != null)
            {
                var selectedProject = ProfessionalsListView.SelectedItem as Professional;
                new InAppNavigationController().NavigateTo(typeof(Lib.AdminNS.Views.CRUDPages.ProfessionalNS.ReadPage.ReadPage), selectedProject);
            }
            else
            {
                // Validation content dialog
                var dialog = new MessageDialog("You have to select a Project.");
                await dialog.ShowAsync();

            }
        }
    }
}
