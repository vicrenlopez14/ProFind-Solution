﻿using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.Global.Views.About_Page;
using ProFind.Lib.Global.Views.Preferences_Page;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.ClientNS.Views.Main_Page
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 


    public sealed partial class Main_Page_Client : Page
    {

        static Dictionary<string, Type> DefinedPagesDictionary = new Dictionary<string, Type>()
        {
            {"Proyects_Page", typeof(CRUDPages.ProjectNS.ReadPage.ReadPage) },
            {"Notifications_Page", typeof(CRUDPages.NotificationNS.ReadPage.ReadPage) },
            {"Catalog_Page", typeof(CRUDPages.NotificationNS.ReadPage.ReadPage) }, 
            {"Activities_Page", typeof(CRUDPages.NotificationNS.ReadPage.ReadPage) }, 
            {"Preferences_Page", typeof(Preferences_Page) },
            {"About_Page",typeof(About_Page) },
            {"", typeof(CRUDPages.ProjectNS.ReadPage.ReadPage) }
        };

        public Main_Page_Client()
        {
            this.InitializeComponent();
            new InAppNavigationController().Init(ClientsContentFrame);
            new InAppNavigationController().NavigateTo(typeof(CRUDPages.ProjectNS.ReadPage.ReadPage));
        }

        private void NavigationView_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                new InAppNavigationController().NavigateTo(typeof(Preferences_Page));
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




            new InAppNavigationController().NavigateTo(DefinedPagesDictionary[selectedItemTag]);
        }
    }
}
