﻿using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using ProFind.Lib.AdminNS.Views.Estado_del_proyecto;
using ProFind.Lib.Global.Services.Models;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.ClientNS.Views.Pays_Page
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
            var projectUnauthorizedService = new ProjectService();
            List<Project> projectUnauthorizedList = new List<Project>();

            projectUnauthorizedList = await projectUnauthorizedService.ListObjectAsync() as List<Project>;

            clientsUnauthorizedListView.ItemsSource = projectUnauthorizedList;
        }
    }
}
