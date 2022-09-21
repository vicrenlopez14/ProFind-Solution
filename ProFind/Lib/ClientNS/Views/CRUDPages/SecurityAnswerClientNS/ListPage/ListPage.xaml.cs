﻿using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.ClientNS.Controllers;
using ProFind.Lib.Global.Controllers;
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

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.ClientNS.Views.CRUDPages.SecurityAnswerClientNS.ListPage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class ListPage : Page
    {
        Securityanswerclient toManipulate = new Securityanswerclient();

        public ListPage()
        {
            this.InitializeComponent();
            InitializeData();
        }

        private async void InitializeData()
        {
            var loggendClient = LoggedClientStore.LoggedClient;
            var SecurityASw = await APIConnection.GetConnection.GetSecurityanswerclientsAsync();
            var RelatePropals = SecurityASw.Where(c => c.IdC1 == loggendClient.IdC).ToList();

            SecurityAnswerClientListView.ItemsSource = RelatePropals;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            new GlobalNavigationController().NavigateTo(typeof(Lib.ClientNS.Views.CRUDPages.SecurityAnswerClientNS.UpdatePage.UpdatePage));
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            await APIConnection.GetConnection.DeleteSecurityanswerclientAsync(toManipulate.IdSa);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            new InAppNavigationController().GoBack();
        }
    }
}
