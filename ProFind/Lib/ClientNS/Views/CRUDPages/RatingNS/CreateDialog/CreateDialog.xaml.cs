using ProFind.Lib.ClientNS.Controllers;
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

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.ClientNS.Views.CRUDPages.RatingNS.CreateDialog
{
    public sealed partial class CreateDialog : ContentDialog
    {

        private Professional ToRateProfessional;
        public CreateDialog(Professional toRateProfessional)
        {
            this.InitializeComponent();

            this.ToRateProfessional = toRateProfessional;
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            try
            {
                if (string.IsNullOrEmpty(Experience_tb.Text))
                {
                    Hide();
                    return;
                }

                var newRating = new Rating
                {
                    IdC1 = LoggedClientStore.LoggedClient.IdC,
                    IdP1 = ToRateProfessional.IdP,
                    CommentRt = Experience_tb.Text,
                    RatingRt = ((float)Qualify_rc.Value)
                };

                await APIConnection.GetConnection.PostRatingAsync(newRating);
            }
            catch (ProFindServicesException ex)
            {
                if (ex.StatusCode >= 200 && ex.StatusCode < 300)
                {
                    var dialog = new MessageDialog("Rating created successfully");
                    await dialog.ShowAsync();
                }
                else
                {
                    var dialog = new MessageDialog("Error creating rating");
                    await dialog.ShowAsync();
                }

                Hide();
            }
            {
                var dialog = new MessageDialog("Error creating rating");
                await dialog.ShowAsync();
            }


        }

        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }


}

