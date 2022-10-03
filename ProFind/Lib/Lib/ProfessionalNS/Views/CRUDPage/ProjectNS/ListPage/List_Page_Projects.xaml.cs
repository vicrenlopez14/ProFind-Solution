using Windows.UI.Xaml.Controls;
using ProFind.Lib.Global.Services;
using ProFind.Lib.ClientNS.Controllers;
using ProFind.Lib.ProfessionalNS.Controllers;
using System.Linq;
using ProFind.Lib.AdminNS.Controllers;
using Windows.UI.Popups;
using System;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.ProfessionalNS.Views.CRUDPage.ProjectNS.ListPage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class List_Page_Projects : Page
    {
        public List_Page_Projects()
        {

            this.InitializeComponent();
            LoadData();
        }

        private void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            LoadData();
        }

        private async void LoadData()
        {

            // Projects of the loggedProfessoinal
            var projects = await APIConnection.GetConnection.GetProjectsAsync();
            var ForThisProfessional = projects.Where(x => x.IdP1 == LoggedProfessionalStore.LoggedProfessional.IdP).ToList();

            ProjectsListView.ItemsSource = ForThisProfessional;

        }

     

        private async void AppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                var obj = ProjectsListView.SelectedItem as Project;
            }
            catch
            {
                var dialog = new MessageDialog("You have to select a Project.");
                await dialog.ShowAsync();
            }
        }

        private void AddProjects(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            new InAppNavigationController().NavigateTo(typeof(CreatePage.CreatePage));
        }

        private async void DeleteProject(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                
                var selectedProject = ProjectsListView.SelectedItem as Project;
                if (selectedProject != null)
                {
                    await APIConnection.GetConnection.DeleteProjectAsync(selectedProject.IdPj);

                    var dialog = new MessageDialog("Project deleted successfully.");
                    await dialog.ShowAsync();
                    return;
                }
                else
                {

                    var dialog = new MessageDialog("You have to select a Project.");
                    await dialog.ShowAsync();
                }



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
                LoadData();
            }
        }

        private async void UpdateProject(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
                if (ProjectsListView.SelectedItem != null)
                {
                    var obj = ProjectsListView.SelectedItem as Project;
                    new InAppNavigationController().NavigateTo(typeof(ProFind.Lib.ProfessionalNS.Views.CRUDPage.ProjectNS.UpdatePage.Update_Project), obj);
                }
                else
                {

                    var dialog = new MessageDialog("You have to select a Project.");
                    await dialog.ShowAsync();
                }

        }

        private async void TagCreate(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
                new InAppNavigationController().NavigateTo(typeof(ProFind.Lib.ProfessionalNS.Views.CRUDPage.Tags.CreatePage.CreatePage));
        }

        private async void NotificationCreate(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                var obj = ProjectsListView.SelectedItem as Project;
                new InAppNavigationController().NavigateTo(typeof(ProFind.Lib.ProfessionalNS.Views.CRUDPage.NotificationNS.CreatePage.CreatePage), obj);
            }
            catch
            {
                var dialog = new MessageDialog("You have to select a Project.");
                await dialog.ShowAsync();
            }
        }

        private async void AppBarButton_Click_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {

                var selectedProject = ProjectsListView.SelectedItem as Project;
                if (selectedProject != null)
                {
                    await APIConnection.GetConnection.DeleteProjectAsync(selectedProject.IdPj);

                    var dialog = new MessageDialog("Project completed successfully.");
                    await dialog.ShowAsync();
                    return;
                }
                else
                {

                    var dialog = new MessageDialog("You have to select a Project.");
                    await dialog.ShowAsync();
                }



            }
            catch (ProFindServicesException ex)
            {
                if (ex.StatusCode >= 200 && ex.StatusCode <= 205)
                {
                    var dialog = new MessageDialog("Project completed successfully.");
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
                LoadData();
            }
        }
    }
}
