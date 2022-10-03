using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.Global.Controllers;
using ProFind.Lib.Global.Services;
using ProFind.Lib.ProfessionalNS.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.ProfessionalNS.Views.CRUDPage.ClientNS.ListPage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class ListPageClient : Page
    {
        Client id;
        public ListPageClient()
        {
            this.InitializeComponent();
            GetProjectsList();
        }
        public async void GetProjectsList()
        {
            var loggedProfessional = LoggedProfessionalStore.LoggedProfessional;

            // Major lists
            var projects = await APIConnection.GetConnection.GetProjectsAsync();
            var clients = await APIConnection.GetConnection.GetClientsAsync();

            // Projects where loggedProfessional is related
            var relatedProjects = projects.Where(p => p.IdP1 == loggedProfessional.IdP).ToList();

            // Notifications where loggedProfessional is related through a project
            var relatedClients = new List<Client>();
            foreach (var project in projects)
            {
                var relatedClientsForThisProject = clients.Where(n => n.IdC == project.IdC1).ToList();
                relatedClients.AddRange(relatedClientsForThisProject);
            }

            Clients_lw.ItemsSource = relatedClients.Distinct().ToList();
        }

        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            new InAppNavigationController().NavigateTo(typeof(ProFind.Lib.ProfessionalNS.Views.CRUDPage.ClientNS.ChatPage.ChatPageCLient));

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new InAppNavigationController().NavigateTo(typeof(ProFind.Lib.ProfessionalNS.Views.CRUDPage.ClientNS.SearchPage.SearchPageClient));

        }
        private void Activities_lw_SelectionChanged(object sender, ItemClickEventArgs e)
        {
            var client = e.ClickedItem as Client;

            //new InAppNavigationController().NavigateTo(typeof(Lib.ProfessionalNS.Views.CRUDPage.ClientNS.ReadPage), client);
        }

        private void Search_btn_Click(object sender, RoutedEventArgs e)
        {
            new InAppNavigationController().NavigateTo(typeof(ProFind.Lib.ProfessionalNS.Views.CRUDPage.ClientNS.SearchPage.SearchPageClient));
        }

        private async void SearchBox_QueryChanged(SearchBox sender, SearchBoxQueryChangedEventArgs args)
        {
            var loggedProfessional = LoggedProfessionalStore.LoggedProfessional;

            // Major lists
            var projects = await APIConnection.GetConnection.GetProjectsAsync();
            var clients = await APIConnection.GetConnection.GetClientsAsync();

            // Projects where loggedProfessional is related
            var relatedProjects = projects.Where(p => p.IdP1 == loggedProfessional.IdP).ToList();

            // Notifications where loggedProfessional is related through a project
            var relatedClients = new List<Client>();
            foreach (var project in projects)
            {
                var relatedClientsForThisProject = clients.Where(n => n.IdC == project.IdC1).ToList();
                relatedClients.AddRange(relatedClientsForThisProject);
            }

            var filteredList = relatedClients.Distinct().ToList();


            if (string.IsNullOrEmpty(sender.QueryText))
            {
                Clients_lw.ItemsSource = null;
                Clients_lw.ItemsSource = filteredList;
                return;
            }

            var newList = filteredList.Where(x => x.NameC.Contains(sender.QueryText));

            Clients_lw.ItemsSource = null;
            Clients_lw.ItemsSource = newList;
        }
    }
}
