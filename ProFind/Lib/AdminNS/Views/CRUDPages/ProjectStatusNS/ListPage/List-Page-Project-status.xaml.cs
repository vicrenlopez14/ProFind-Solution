﻿using ProFind.Lib.Global.Services;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.ProfessionalNS.Views.CRUDPages.ProjectStatusNS.ListPage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class List_Page_Project_status : Page
    {
        Projectstatus Id1 = new Projectstatus();
        public List_Page_Project_status()
        {
            this.InitializeComponent();
            InitializeData();
        }
        private async void InitializeData()
        {
            int idNo = Id1.IdPs.GetValueOrDefault();
            await APIConnection.GetConnection.GetProjectstatusAsync(idNo);
        }
    }
}
