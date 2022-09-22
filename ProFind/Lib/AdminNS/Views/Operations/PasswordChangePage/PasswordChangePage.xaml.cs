﻿using Admin = ProFind.Lib.Global.Services.Admin;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.Operations.PasswordChangePage
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
            new InAppNavigationController().NavigateTo(typeof(Int_Page.Int_Page));
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(Password_pb.Password != Confirmation_pb.Password || (!FieldsChecker.CheckPassword(Password_pb.Password)))
            {

            }
            var toChangePassword = new Admin();
            toChangePassword.PasswordA = Password_pb.Password;
            var id = await APIConnection.GetConnection.GetAdminFromEmailAsync(email);
            await APIConnection.GetConnection.PutAdminAsync(id.IdA,toChangePassword);
            new InAppNavigationController().NavigateTo(typeof(Int_Page.Int_Page));
        }
    }
}
