﻿using Application.Models;
using Application.Services;
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

namespace UWP_Administradores
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Page_Activos : Page
    {
        public Page_Activos()
        {
            this.InitializeComponent();
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
        public async void GetProjectsList()

        {
            var projectService = new PfProjectService();

            List<PFProject> ActiveProjectsList = new List<PFProject>();

            ActiveProjectsList = await projectService.ListObjectAsync() as List<PFProject>;

            ActiveProjectsListView.ItemsSource = ActiveProjectsList;
        }
    }
}
