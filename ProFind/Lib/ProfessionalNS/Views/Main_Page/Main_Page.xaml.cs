using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using ProFind.Lib.Global.Views.Preferences_Page;
using ProFind.Lib.Global.Views.About_Page;
using ProFind.Lib.AdminNS.Controllers;

namespace ProFind.Lib.ProfessionalNS.Views.Main_Page
{
    public sealed partial class Main_Page_Professional : Page
    {
        static Dictionary<string, Type> DefinedPagesDictionary = new Dictionary<string, Type>()
        {
            {"ActiveProjects_Page", typeof(Page_Activos) },
            {"InactiveProjects_Page", typeof(Page_Inactivos) },
            {"Notifications_Page", typeof(Notifications_Page.Notifications_Page) },
            {"Activities_Page", typeof(ActivesPage) },
            {"Preferences_Page", typeof(Preferences_Page) },
            {"About_Page",typeof(About_Page) },
            {"", typeof(Page_Activos) }
        };
        public Main_Page_Professional()
        {
            this.InitializeComponent();
            new InAppNavigationController().Init(Professionals_ContentFrame);
            new InAppNavigationController().NavigateTo(typeof(Page_Activos));

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

            try
            {
                new InAppNavigationController().NavigateTo(DefinedPagesDictionary[selectedItemTag]);
            }
            catch
            {
                new InAppNavigationController().NavigateTo(DefinedPagesDictionary["Dashboard_Page"]);
            }
        }

        private void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
        }

        private void Page_Loaded_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            new InAppNavigationController().NavigateTo(DefinedPagesDictionary["Dashboard_Page"]);
        }
    }
}
