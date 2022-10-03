using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.Global.Controllers;
using ProFind.Lib.Global.Helpers;
using ProFind.Lib.Global.Services;
using System;
using System.Collections;
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

namespace ProFind.Lib.AdminNS.Views.CRUDPages.AdminNS.ListPage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class ListPageAdmin : Page
    {
        public ListPageAdmin()
        {
            this.InitializeComponent();
            GetAdminsList();
        }
        public async void GetAdminsList()
        {

            AdminsListView.ItemsSource = await APIConnection.GetConnection.GetAdminsAsync();
        }



        private void Generate_Report_btn_Click(object sender, RoutedEventArgs e)
        {
            new InAppNavigationController().NavigateTo(typeof(SearchPage.search_admin));
        }




        private async void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {


        }

        private async void SearchBox_QueryChanged(SearchBox sender, SearchBoxQueryChangedEventArgs args)
        {
            var allList = await APIConnection.GetConnection.GetAdminsAsync();

            if (string.IsNullOrEmpty(sender.QueryText))
            {
                AdminsListView.ItemsSource = null;
                AdminsListView.ItemsSource = allList;
                return;
            }

            var newList = allList.Where(x => x.NameA.Contains(sender.QueryText));

            AdminsListView.ItemsSource = null;
            AdminsListView.ItemsSource = newList;
        }


        private async void Delete_Click_1(object sender, RoutedEventArgs e)
        {
            if ((AdminsListView.ItemsSource as List<Admin>).Count > 1)
            {
                try
                {
                    var obj = AdminsListView.SelectedItem as Admin;
                    await APIConnection.GetConnection.DeleteAdminAsync(obj.IdA);
                    var dialog = new MessageDialog("Admin deleted successfully.");
                    await dialog.ShowAsync();
                }
                catch (ProFindServicesException ex)
                {
                    if (ex.StatusCode == 204)
                    {
                        var dialog = new MessageDialog("Admin deleted successfully.");
                        await dialog.ShowAsync();
                    }
                    else
                    {
                        var dialog = new MessageDialog("You have to select an admin.");
                        await dialog.ShowAsync();
                    }
                }
                finally
                {
                    GetAdminsList();
                }
            }
            else
            {
                {
                    var dialog = new MessageDialog("You can't delete the last admin.");
                    await dialog.ShowAsync();
                }
            }
        }

        private async void Update_Click_1(object sender, RoutedEventArgs e)
        {
            if (AdminsListView.SelectedItem != null)
            {
                var obj = AdminsListView.SelectedItem as Admin;
                new InAppNavigationController().NavigateTo(typeof(UpdatePage.UpdatePage), obj);
            }
            else
            {
                // Validation content dialog
                var dialog = new MessageDialog("You have to select a Admin.");
                await dialog.ShowAsync();

            }

        }

        private void Add_Click_1(object sender, RoutedEventArgs e)
        {
            new InAppNavigationController().NavigateTo(typeof(CreatePage.CreatePage));
        }

        private async void GenerateReport_btn_Click(object sender, RoutedEventArgs e)
        {
            URLOpenerUtil.OpenURL(@"https://localhost:7119/Report/RegisteredAdmins");
        }

    }
}

