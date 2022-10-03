using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.Global.Services;
using Project = ProFind.Lib.Global.Services.Project;
using System.Linq;
using ProFind.Lib.ProfessionalNS.Controllers;
using Windows.UI.Popups;
using System;
using ProFind.Lib.Global.Helpers;

namespace ProFind.Lib.AdminNS.Views.CRUDPages.ProjectNS.ReadPage
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
            // Projects in which professional is related
            var projects = await APIConnection.GetConnection.GetProjectsAsync();

            ProjectsListView.ItemsSource = projects;
        }

        private void AdminListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var project = e.ClickedItem as Project;

            new InAppNavigationController().NavigateTo(typeof(UpdatePage.Update_Project), project);
        }

        private async void SearchBox_QueryChanged(SearchBox sender, SearchBoxQueryChangedEventArgs args)
        {
            // Projects in which professional is related
            var projects = await APIConnection.GetConnection.GetProjectsAsync();
            projects = projects.Where(x => x.TitlePj.ToLower().Contains(args.QueryText.ToLower())).ToList();

            ProjectsListView.ItemsSource = projects;
        }

       

        private async void Update_Click_1(object sender, RoutedEventArgs e)
        {
            if (ProjectsListView.SelectedItem != null)
            {
                var selectedProject = ProjectsListView.SelectedItem as Project;
                new InAppNavigationController().NavigateTo(typeof(Lib.AdminNS.Views.CRUDPages.ProjectNS.UpdatePage.Update_Project), selectedProject);
            }
            else
            {
                // Validation content dialog
                var dialog = new MessageDialog("You have to select a Project.");
                await dialog.ShowAsync();

            }
        }

        private void ProjectsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ProjectsListView_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void Delete_Click_2(object sender, RoutedEventArgs e)
        {
            if (ProjectsListView.SelectedItem != null)
            {
                try
                {
                    var selectedProject = ProjectsListView.SelectedItem as Project;
                    await APIConnection.GetConnection.DeleteProjectAsync(selectedProject.IdPj);

                    var dialog = new MessageDialog("Project deleted successfully.");
                    await dialog.ShowAsync();
                }
                catch (ProFindServicesException ex)
                {
                    if (ex.StatusCode == 204)
                    {
                        var dialog = new MessageDialog("Project deleted successfully.");
                        await dialog.ShowAsync();
                    }
                    else
                    {
                        var dialog = new MessageDialog("You have to select a project.");
                        await dialog.ShowAsync();
                    }
                }
                finally
                {
                    InitializeData();
                }
            }
            else
            {
                // Validation content dialog
                var dialog = new MessageDialog("You have to select a Project.");
                await dialog.ShowAsync();

            }
            
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            URLOpenerUtil.OpenURL(@"https://localhost:7119/Report/CreatedProjects");

        }
    }
}
