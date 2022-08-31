﻿using Application.Models;
using Application.Services;
using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.AdminNS.Views.CRUD;
using ProFind.Lib.Global.Services;
using ProFind.Lib.Global.Services.Models;
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

namespace ProFind.Lib.AdminNS.Views.CRUDPages.AdminPages.ReadPage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class ReadPage : Page
    {
        public ReadPage()
        {
            this.InitializeComponent();

            InitializeData();
        }

        private async void InitializeData()
        {
            ProjectsListView.ItemsSource = await APIConnection.GetConnection.GetAdminsAsync();
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
