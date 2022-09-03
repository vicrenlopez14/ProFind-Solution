﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.AdminNS.Views.CRUD;
using ProFind.Lib.Global.Services.Models;
using ProFind.Lib.Global.Services;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.AdminNS.ReadPage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class ReadPage : Page
    {
        Admin Id1 = new Admin();
        public ReadPage()
        {
            this.InitializeComponent();

            InitializeData();
        }

        private async void InitializeData()
        {
            await APIConnection.GetConnection.GetAdminAsync(Id1.IdA);
        }

        private void AdminListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var admin = e.ClickedItem as Admin;

            new InAppNavigationController().NavigateTo(typeof(UpdatePageAdmin), admin);
        }

        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            new InAppNavigationController().NavigateTo(typeof(CreatePageAdmin));
        }
    }
}