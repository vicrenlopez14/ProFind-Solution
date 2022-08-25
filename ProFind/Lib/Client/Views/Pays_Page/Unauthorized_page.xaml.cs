﻿using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.Client.Views.Pays_Page
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Unauthorized_page : Page
    {
        public Unauthorized_page()
        {
            this.InitializeComponent();
            GetProjectUnauthorizedList();
        }

        public async void GetProjectUnauthorizedList()
        {
            var projectUnauthorizedService = new PfProjectService();
            List<PFProject> projectUnauthorizedList = new List<PFProject>();

            projectUnauthorizedList = await projectUnauthorizedService.ListObjectAsync() as List<PFProject>;

            clientsUnauthorizedListView.ItemsSource = projectUnauthorizedList;
        }
    }
}
