﻿using ProFind.Lib.Global.Services;
using Windows.UI.Xaml.Controls;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.NotificationNS.ListPage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class List_Page : Page
    {
        Notification Id1 = new Notification();
        public List_Page()
        {
            this.InitializeComponent();
            InitializeData();
        }
        private async void InitializeData()
        {
        

        }
        
    }
}
