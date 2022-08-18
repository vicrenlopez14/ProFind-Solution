﻿using Application.Models;
using Application.Services;
using ProFind.Lib.Admin.Controllers;
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

namespace ProFind.Lib.Admin.Views.Professionals_Page
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ActivesPage : Page
    {
        public ActivesPage()
        {
            this.InitializeComponent();
            GetProjectsList();
        }

        public async void GetProjectsList()

        {
            var professionalService = new PFProfessionalService();

            List<PFProject> activeProfessionalsList = new List<PFProject>();

            IDictionary<string, string> criteries = new Dictionary<string, string>()
            {
                ["ActiveP"] = "1"
            };

            activeProfessionalsList = await professionalService.Search(criteries) as List<PFProject>;

            DashboardProfessionalsActiveListView.ItemsSource = activeProfessionalsList;
        }

        private void ProjectsActiveListView_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void ProjectsActiveListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            PFProfessional project = e.ClickedItem as PFProfessional;
            new AdminNavigationController().NavigateTo(typeof(Lib.Admin.Views.InitPage.InitPage), project);
        }
    }
}
