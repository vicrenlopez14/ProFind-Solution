﻿using Professional = ProFind.Lib.Global.Services.Professional;
using ProFind.Lib.AdminNS.Controllers;
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
using ProFind.Lib.Global.Helpers;
using ProFind.Lib.Global.Services;
using Windows.UI.Popups;
using ProFind.Lib.Global.Controllers;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.ProfessionalNS.Views.Operations.PasswordChangePage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PasswordChangePage : Page
    {
        public PasswordChangePage()
        {
            this.InitializeComponent();
        }
        private string email;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null)
            {
                email = e.Parameter.ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new InAppNavigationController().NavigateTo(typeof(InitPage.InitPage));
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Password_pb.Password != Confirmation_pb.Password || (!FieldsChecker.CheckPassword(Password_pb.Password)))
            {

            }

            try
            {
                await APIConnection.GetConnection.ChangePasswordAsync(email, Password_pb.Password);
            }
            catch (ProFindServicesException ex)
            {
                if (ex.StatusCode >= 200 && ex.StatusCode <= 300)
                {
                    // Success message dialog
                    var dialog = new MessageDialog("The password has been changed successfully");
                    await dialog.ShowAsync();

                    new GlobalNavigationController().NavigateTo(typeof(Lib.ProfessionalNS.Views.InitPage.InitPage));
                }
                else
                {
                    // Failure message dialog
                    var dialog = new MessageDialog("There was a problem changing the password");
                    await dialog.ShowAsync();
                }
            }
            finally
            {
                new GlobalNavigationController().NavigateTo(typeof(Lib.ProfessionalNS.Views.InitPage.InitPage));

            }
        }
    }
}
