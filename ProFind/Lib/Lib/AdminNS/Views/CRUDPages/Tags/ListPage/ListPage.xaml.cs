using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.Global.Services;
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

namespace ProFind.Lib.AdminNS.Views.CRUDPages.Tags.ListPage
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
            Ranks_lw.ItemsSource = await APIConnection.GetConnection.GetTagsAsync();
            Ranks_lw.ItemsSource = await APIConnection.GetConnection.GetTagsAsync() as List<Tag>;

        }

       
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            new InAppNavigationController().NavigateTo(typeof(ProFind.Lib.AdminNS.Views.CRUDPages.Tags.CreatePage.CreatePage));
        }

        private async void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (Ranks_lw.SelectedItem != null)
            {
                Tag lectedTag = Ranks_lw.SelectedItem as Tag;
                new InAppNavigationController().NavigateTo(typeof(ProFind.Lib.AdminNS.Views.CRUDPages.Tags.UpdatePage.UpdatePage), lectedTag);
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
                var obj = Ranks_lw.SelectedItem as Tag;
                await APIConnection.GetConnection.DeleteTagAsync(obj.IdT);
                var dialog = new MessageDialog("The Tag has been deleted");
                await dialog.ShowAsync();
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
