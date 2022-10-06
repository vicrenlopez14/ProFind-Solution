using ProFind.Lib.Global.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.ClientNS.Views.CRUDPages.RatingNS.DetailsDialog
{
    public sealed partial class DetailsDialog : ContentDialog
    {

        private Rating ratingToShow;

        public DetailsDialog(Rating rating)
        {
            this.InitializeComponent();
            this.ratingToShow = rating;
        }

        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            if (ratingToShow == null)
            {
                Hide();
                return;
            }

            Experience_tb.Text = ratingToShow.CommentRt;
            Qualify_rc.Value = ratingToShow.RatingRt ?? 0;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
