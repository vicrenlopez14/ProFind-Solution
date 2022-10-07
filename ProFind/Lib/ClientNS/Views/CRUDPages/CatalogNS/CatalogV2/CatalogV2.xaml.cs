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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.ClientNS.Views.CRUDPages.CatalogNS.CatalogV2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CatalogV2 : Page
    {
        private List<Profession> professionsListObj = new List<Profession>();
        private List<Professional> professionalsListObj = new List<Professional>();

        public CatalogV2()
        {
            this.InitializeComponent();
        }
        
        private async void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
            try
            {
                professionsListObj = await APIConnection.GetConnection.GetProfessionsAsync() as List<Profession>;
                Professions_lw.ItemsSource = professionsListObj;
                Professions_lw.SelectedIndex = 0;
                OnSelectedItem();
            }
            catch
            {
                // Message dialog
                await new Windows.UI.Popups.MessageDialog("Error loading professionals list").ShowAsync();
            }
        }

        private async void Professions_lw_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            OnSelectedItem();
        }

        private async void OnSelectedItem()
        {
            if (Professions_lw.SelectedItem == null || professionsListObj.Count < 1)
            {
                return;
            }

            var selectedProfession = Professions_lw.SelectedItem as Profession;

            professionalsListObj = await APIConnection.GetConnection.GetProfessionalsOfProfessionAsync(selectedProfession.IdPfs.Value) as List<Professional>;
            UpdateProfessionalsGrid();
        }

        private void UpdateProfessionalsGrid()
        {
            ProfessionalsListView.ItemsSource = null;
            ProfessionalsListView.ItemsSource = professionalsListObj;
        }

        private async void Add_Click_1(object sender, RoutedEventArgs e)
        {
            if (ProfessionalsListView.SelectedItem != null)
            {
                var selectedProfessional = ProfessionalsListView.SelectedItem as Professional;
                new InAppNavigationController().NavigateTo(typeof(Lib.ClientNS.Views.CRUDPages.ProposalsNS.CreatePage.CreatePage), selectedProfessional);
            }
            else
            {
                // Validation content dialog
                var dialog = new MessageDialog("You have to select a Professional.");
                await dialog.ShowAsync();

            }
        }

        private async void SearchBox_QueryChanged(SearchBox sender, SearchBoxQueryChangedEventArgs args)
        {
            var newList = professionalsListObj.Where(x => x.NameP.ToLower().Contains(sender.QueryText));

            ProfessionalsListView.ItemsSource = null;
            ProfessionalsListView.ItemsSource = newList;
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            OnSelectedItem();
        }

        private async void ProfessionalsListView_DoubleTapped_1(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (ProfessionalsListView.SelectedItem != null)
            {
                var selectedProject = ProfessionalsListView.SelectedItem as Professional;
                new InAppNavigationController().NavigateTo(typeof(Lib.AdminNS.Views.CRUDPages.ProfessionalNS.ReadPage.ReadPage), selectedProject);
            }
            else
            {
                // Validation content dialog
                var dialog = new MessageDialog("You have to select a Professional.");
                await dialog.ShowAsync();

            }
        }
    }
}
