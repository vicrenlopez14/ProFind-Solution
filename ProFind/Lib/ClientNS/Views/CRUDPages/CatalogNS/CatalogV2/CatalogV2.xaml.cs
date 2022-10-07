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
            }
            catch
            {
                // Message dialog
                await new Windows.UI.Popups.MessageDialog("Error loading professions list").ShowAsync();
            }
        }

        private async void Professions_lw_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedProfession = Professions_lw.SelectedItem as Profession;

            professionalsListObj = await APIConnection.GetConnection.GetProfessionalsOfProfessionAsync(selectedProfession.IdPfs.Value) as List<Professional>;
            UpdateProfessionalsGrid();

        }
        private void UpdateProfessionalsGrid()
        {
            ProfessionalsGrid.ItemsSource = null;
            ProfessionalsGrid.ItemsSource = professionalsListObj;
        }
    }
}
