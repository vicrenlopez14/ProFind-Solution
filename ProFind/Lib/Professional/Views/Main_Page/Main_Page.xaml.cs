﻿using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using ProFind.Lib.Global.Views.Preferences_Page;
using ProFind.Lib.Professional.Controllers;
using ProFind.Lib.Admin.Views.Estado_del_proyecto;

namespace ProFind.Lib.Professional.Views.Main_Page
{
    public sealed partial class Main_Page_Professional : Page
    {
        static Dictionary<string, Type> DefinedPagesDictionary = new Dictionary<string, Type>()
        {
            {"Calendar_Page", typeof(Calendar_Page.Calendar_Page) },
            {"Dashboard_Page", typeof(Dashboard_Page.Dashboard_Page) },
            {"Notifications_Page", typeof(Notifications_Page.Notifications_Page) },
            {"Processes_Page", typeof(Page_Activos) },
            {"", typeof(Dashboard_Page.Dashboard_Page) }
        };
        public Main_Page_Professional()
        {
            this.InitializeComponent();
            new AdminNavigationController().Init(Professionals_ContentFrame);

        }

        private void NavigationView_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                new AdminNavigationController().NavigateTo(typeof(Preferences_Page));
                return;
            }

            var item = args.InvokedItemContainer;
            string selectedItemTag = item.Tag.ToString();

            if (selectedItemTag.Contains("_"))
            {
                var split = selectedItemTag.Split('_');
                // Pending
            }
            sender.Header = item.Content.ToString();

            try
            {
                new AdminNavigationController().NavigateTo(DefinedPagesDictionary[selectedItemTag]);
            }
            catch
            {
                new AdminNavigationController().NavigateTo(DefinedPagesDictionary["Dashboard_Page"]);
            }
        }

        private void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
        }

        private void Page_Loaded_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            new AdminNavigationController().NavigateTo(DefinedPagesDictionary["Dashboard_Page"]);
        }
    }
}
