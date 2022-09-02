﻿using ProFind.Lib.AdminNS.Controllers;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using ProFind.Lib.Global.Services.Models;
using ProFind.Lib.Global.Services;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.ProfessionalNS.ListPage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class ReadPage : Page
    {
        Professional Id1 = new Professional();
        public ReadPage()
        {
            this.InitializeComponent();
            GetProjectsList();
        }
        public async void GetProjectsList()

        {
            await APIConnection.GetConnection.GetProfessionalAsync(Id1.IdP);
           
        }

        private async void ProjectsActiveListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Professional project = e.ClickedItem as Professional;
            new InAppNavigationController().NavigateTo(typeof(Lib.AdminNS.Views.InitPage.InitPage), project);
        }

    }
}
