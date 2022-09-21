﻿using ProFind.Lib.Global.Services;
using Windows.UI.Xaml.Controls;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.ProjectNS.ListPage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class List_Page_Projects : Page
    {
        public List_Page_Projects()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            LoadData();
        }

        private async void LoadData()
        {
            {
                var projects = await APIConnection.GetConnection.GetProjectsAsync();
                ProjectsListView.ItemsSource = projects;
            }
        }
    }
}
