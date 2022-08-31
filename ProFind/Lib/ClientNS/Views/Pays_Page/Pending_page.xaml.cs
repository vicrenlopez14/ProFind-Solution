﻿using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.ClientNS.Views.Pays_Page
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Pending_page : Page
    {
        public Pending_page()
        {
            this.InitializeComponent();
            GetprojectsPendingList();

        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        public async void GetprojectsPendingList()
        {
            var projectService = new PfProjectService();
            List<PFProject> clientPendingList = new List<PFProject>();

            clientPendingList = await projectService.ListObjectAsync() as List<PFProject>;

            clientsPendingListView.ItemsSource = clientPendingList;
        }
    }
}