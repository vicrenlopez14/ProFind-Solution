using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.ClientNS.Views.CRUDPages.RatingNS.DetailsDialog;
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

namespace ProFind.Lib.ClientNS.Views.CRUDPages.RatingNS.ListPage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListPage : Page
    {
        private Professional professionalToShow;

        public ListPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (!(e.Parameter is Professional))
            {
                // Message dialog
                await new MessageDialog("No professional was provided").ShowAsync();
                new InAppNavigationController().GoBack();
                return;
            }

            professionalToShow = e.Parameter as Professional;
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            MoreDetails();
        }

        private void DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            MoreDetails();
        }

        private async void MoreDetails()
        {
            if (Ratings_lw.SelectedItem == null)
            {
                await new MessageDialog("Please select a rating first").ShowAsync();
                return;
            }

            var selectedRating = Ratings_lw.SelectedItem as Rating;

            new Lib.ClientNS.Views.CRUDPages.RatingNS.DetailsDialog.DetailsDialog(selectedRating);
        }
    }
}
