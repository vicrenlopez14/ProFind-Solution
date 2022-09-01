﻿using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.Global.Services.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.ProfessionalNS.ListPage
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
            var professionalService = new ProfessionalService();
            List<Project> activeProfessionalsList = new List<Project>();


            IDictionary<string, string> criteries = new Dictionary<string, string>()
            {
                ["ActiveP"] = "1"
            };

            activeProfessionalsList = await professionalService.Search(criteries) as List<Project>;

            DashboardProfessionalsActiveListView.ItemsSource = activeProfessionalsList;
        }

        private void ProjectsActiveListView_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void ProjectsActiveListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Professional project = e.ClickedItem as Professional;
            new InAppNavigationController().NavigateTo(typeof(Lib.AdminNS.Views.InitPage.InitPage), project);
        }
    }
}
