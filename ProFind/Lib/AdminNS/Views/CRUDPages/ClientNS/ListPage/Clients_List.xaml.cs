﻿using Windows.UI.Xaml.Controls;
using ProFind.Lib.Global.Services;
using Client = ProFind.Lib.Global.Services.Client;
using ProFind.Lib.Global.Controllers;
using System.Collections.Generic;
using ProFind.Lib.AdminNS.Controllers;
using Windows.UI.Popups;
using System;
using System.Linq;
using ProFind.Lib.Global.Helpers;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.ClientNS.ListPage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Clients_List : Page
    {
        private List<Client> clientsListObj = new List<Client>();

        Client Id1;
        public Clients_List()
        {
            this.InitializeComponent();
            GetClientsList();
        }

        public async void GetClientsList()
        {
            try
            {
                clientsListObj = await APIConnection.GetConnection.GetClientsAsync() as List<Client>;

                Activities_lw.ItemsSource = clientsListObj;
            }
            catch
            {
                await new MessageDialog("Couldn't load clients list. Please try again later.").ShowAsync();
            }
        }

        private async void SearchBox_QueryChanged(SearchBox sender, SearchBoxQueryChangedEventArgs args)
        {

            var newList = clientsListObj.Where(x => x.NameC.ToLower().Contains(sender.QueryText.ToLower()));

            Activities_lw.ItemsSource = null;
            Activities_lw.ItemsSource = newList;
        }

        private async void Delete_Click_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                if (Activities_lw.SelectedItem != null)
                {
                    var obj = Activities_lw.SelectedItem as Client;
                    await APIConnection.GetConnection.DeleteClientAsync(obj.IdC);
                    var dialog = new MessageDialog("Client deleted successfully.");
                    await dialog.ShowAsync();
                }
                else
                {
                    // Validation content dialog
                    var dialog = new MessageDialog("You have to select a client.");
                    await dialog.ShowAsync();

                }
            }
            catch (ProFindServicesException ex)
            {
                if (ex.StatusCode == 204)
                {
                    var dialog = new MessageDialog("Client deleted successfully.");
                    await dialog.ShowAsync();
                }
                else
                {
                    var dialog = new MessageDialog("You have to select an client.");
                    await dialog.ShowAsync();
                }
            }
            finally
            {
                GetClientsList();
            }
        }

        private async void Update_Click_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // Selected client

            if (Activities_lw.SelectedItem != null)
            {
                Client selectedClient = Activities_lw.SelectedItem as Client;

                new InAppNavigationController().NavigateTo(typeof(Lib.AdminNS.Views.CRUDPages.ClientNS.UpdatePage.Client_Update_Delete), selectedClient);
            }
            else
            {
                // Validation content dialog
                var dialog = new MessageDialog("You have to select a client.");
                await dialog.ShowAsync();

            }
        }

        private void Add_Click_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            new InAppNavigationController().NavigateTo(typeof(ProFind.Lib.AdminNS.Views.CRUDPages.ClientNS.CreatePage.Create_page));
        }

        private void AppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            URLOpenerUtil.OpenURL(@"https://reporter.profind.work/Report/RegisteredClients");
        }

        private async void Activities_lw_DoubleTapped(object sender, Windows.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {
            if (Activities_lw.SelectedItem != null)
            {
                Client selectedClient = Activities_lw.SelectedItem as Client;

                new InAppNavigationController().NavigateTo(typeof(Lib.AdminNS.Views.CRUDPages.ClientNS.ReadPage.ReadPage), selectedClient);
            }
            else
            {
                // Validation content dialog
                var dialog = new MessageDialog("You have to select a client.");
                await dialog.ShowAsync();

            }
        }

        private void Refresh_Click_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            GetClientsList();
        }

        private void AppBarButton_Click_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            GetClientsList();
        }
    }
}
