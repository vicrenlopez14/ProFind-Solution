using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.Global.Helpers;
using ProFind.Lib.Global.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.AdminNS.ListPage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class ListPageAdmin : Page
    {
        private List<Admin> adminsListObj = new List<Admin>();

        public ListPageAdmin()
        {
            this.InitializeComponent();
            GetAdminsList();
        }
        public async void GetAdminsList()
        {
            try
            {
                adminsListObj = await APIConnection.GetConnection.GetAdminsAsync() as List<Admin>;
                AdminsListView.ItemsSource = adminsListObj;
            }
            catch (Exception ex)
            {
                await new MessageDialog("Couldn't load admins list. Please try again later.").ShowAsync();
            }
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

            var newList = adminsListObj.Where(x => x.NameA.ToLower().Contains(sender.QueryText.ToLower()));

            AdminsListView.ItemsSource = null;
            AdminsListView.ItemsSource = newList;
        }


        private async void Delete_Click_1(object sender, RoutedEventArgs e)
        {

            try
            {
                if (AdminsListView.SelectedItem != null)
                {
                    if (adminsListObj.Count == 1)
                    {
                        await new MessageDialog("You can't delete the last admin.").ShowAsync();
                        return;
                    }

                    var obj = AdminsListView.SelectedItem as Admin;
                    await APIConnection.GetConnection.DeleteAdminAsync(obj.IdA);
                    var dialog = new MessageDialog("Admin deleted successfully.");
                    await dialog.ShowAsync();
                }
                else
                {
                    var dialog = new MessageDialog("You have to select an admin.");
                    await dialog.ShowAsync();
                }
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
            URLOpenerUtil.OpenURL(@"https://reporter.profind.work/Report/RegisteredAdmins");
        }


        private async void AdminsListView_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (AdminsListView.SelectedItem != null)
            {
                var obj = AdminsListView.SelectedItem as Admin;
                new InAppNavigationController().NavigateTo(typeof(ReadPage.ReadPage), obj);
            }
            else
            {
                // Validation content dialog
                var dialog = new MessageDialog("You have to select an Admin.");
                await dialog.ShowAsync();

            }
        }

        private void Reflesh_Click_1(object sender, RoutedEventArgs e)
        {
            GetAdminsList();
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            GetAdminsList();
        }
    }
}

