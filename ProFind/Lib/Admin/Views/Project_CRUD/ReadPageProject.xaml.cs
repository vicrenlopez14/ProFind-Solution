﻿using Application.Models;
using Application.Services;
using ProFind.Lib.Admin.Controllers;
using ProFind.Lib.Admin.Views.CRUD;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.Admin.Views.Project_CRUD
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ReadPageProject : Page
    {
        public ReadPageProject()
        {
            this.InitializeComponent();

            InitializeData();
        }

        private async void InitializeData()
        {
            AdminsListView.ItemsSource = await new PfProjectService().ListObjectAsync();
        }

        private void AdminListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var project = e.ClickedItem as PFProject;

            new AdminNavigationController().NavigateTo(typeof(UpdatePageProject), project);
        }

        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            new AdminNavigationController().NavigateTo(typeof(CreatePageProject));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new AdminNavigationController().NavigateTo(typeof(CreatePageProject));
        }
    }
}
