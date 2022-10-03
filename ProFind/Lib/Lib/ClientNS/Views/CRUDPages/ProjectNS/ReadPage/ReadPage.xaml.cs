using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.Global.Services;
using Project = ProFind.Lib.Global.Services.Project;
using ProFind.Lib.Global.Controllers;
using ProFind.Lib.ClientNS.Controllers;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Popups;
using System;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.ClientNS.Views.CRUDPages.ProjectNS.ReadPage
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
            var loggendProject = LoggedClientStore.LoggedClient;
            var Projects = await APIConnection.GetConnection.GetProjectsAsync();

            var RelatedProject = Projects.Where(c => c.IdC1 == loggendProject.IdC).ToList();


            AdminsListView.ItemsSource = RelatedProject;
        }

        private void AdminListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var project = e.ClickedItem as Project;

        }

        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           
        }

        private async void Add_Click_1(object sender, RoutedEventArgs e)
        {

            if (AdminsListView.SelectedItem != null)
            {
                var dialog = new MessageDialog("You have accepted the project.");
                await dialog.ShowAsync();
            }
            else
            {

                var dialog = new MessageDialog("You have to select a Project.");
                await dialog.ShowAsync();
            }
        }

        private async void Decline_Click_1(object sender, RoutedEventArgs e)
        {

            try
            {
                var selectedProject = AdminsListView.SelectedItem as Project;
                await APIConnection.GetConnection.DeleteProjectAsync(selectedProject.IdPj);

                var dialog = new MessageDialog("Project deleted successfully.");
                await dialog.ShowAsync();
            }
            catch (ProFindServicesException ex)
            {
                if (ex.StatusCode >= 200 && ex.StatusCode <= 205)
                {
                    var dialog = new MessageDialog("Project deleted successfully.");
                    await dialog.ShowAsync();
                }
                else
                {
                    var dialog = new MessageDialog("You have to select an Project.");
                    await dialog.ShowAsync();
                }
            }
            finally
            {
                InitializeData();
            }
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
