﻿using ProFind.Lib.AdminNS.Controllers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.Professional_CRUD
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ReadPageProfessional : Page
    {
        public ReadPageProfessional()
        {
            this.InitializeComponent();

            InitializeData();
        }

        private async void InitializeData()
        {
            ProfessionalsListView.ItemsSource = await new PFProfessionalService().ListObjectAsync();
        }

        private void AdminListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var project = e.ClickedItem as PFProfessional;

            new InAppNavigationController().NavigateTo(typeof(CreatePageProfessional), project);
        }

        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            new InAppNavigationController().NavigateTo(typeof(CreatePageProfessional));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new InAppNavigationController().NavigateTo(typeof(CreatePageProfessional));
        }
    }
}