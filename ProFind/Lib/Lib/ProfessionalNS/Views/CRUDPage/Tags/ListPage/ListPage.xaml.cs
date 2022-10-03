using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.Global.Services;
using ProFind.Lib.ProfessionalNS.Controllers;
using System;
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

namespace ProFind.Lib.ProfessionalNS.Views.CRUDPage.Tags.ListPage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class ListPage : Page
    {
        public ListPage()
        {
            this.InitializeComponent();
            InitializeData();
        }
        private async void InitializeData()
        {
            var loggendPro = LoggedProfessionalStore.LoggedProfessional;
            var project = await APIConnection.GetConnection.GetProjectsAsync();
            var Tag = await APIConnection.GetConnection.GetTagsAsync();

            var RelatedaTags = new List<Tag>();


            foreach (var Projects in project)
            {
                var RelatedATagForRelatedProject = Tag.Where(n => n.IdPj1Navigation.IdP1 == loggendPro.IdP).ToList();
                RelatedaTags.AddRange(RelatedATagForRelatedProject);
            }

            Ranks_lw.ItemsSource = RelatedaTags;

        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            new InAppNavigationController().NavigateTo(typeof(ProFind.Lib.ProfessionalNS.Views.CRUDPage.Tags.CreatePage.CreatePage));
        }

        private async void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (Ranks_lw.SelectedItem != null)
            {
                Tag lectedTag = Ranks_lw.SelectedItem as Tag;
                new InAppNavigationController().NavigateTo(typeof(ProFind.Lib.ProfessionalNS.Views.CRUDPage.Tags.UpdatePage.UpdatePage), lectedTag);
            }
            else
            {
                // Validation content dialog
                var dialog = new MessageDialog("You have to select a Tag.");
                await dialog.ShowAsync();

            }
        }

        private async void AppBarButton_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Ranks_lw.SelectedItem != null)
                {
                    var obj = Ranks_lw.SelectedItem as Tag;

                    await APIConnection.GetConnection.DeleteTagAsync(obj.IdT);
                }
                else
                {
                    var dialog = new MessageDialog("You have to select a Tag.");
                    await dialog.ShowAsync();

                }  
            }
            catch (ProFindServicesException ex)
            {
                if (ex.StatusCode >= 200 && ex.StatusCode <= 205)
                {
                    var dialog = new MessageDialog("The Tag has been deleted");
                    await dialog.ShowAsync();
                }
                else
                {
                    var dialog = new MessageDialog("There was a problem while creating the Tag, try again later");
                    await dialog.ShowAsync();
                }
            }
            finally
            {
                InitializeData();
            }
        }
    }
}
