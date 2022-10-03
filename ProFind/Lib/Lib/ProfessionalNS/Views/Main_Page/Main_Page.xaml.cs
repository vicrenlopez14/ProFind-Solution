using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.Global.Views.Preferences_Page;
using ProFind.Lib.Global.Views.About_Page;
using ProFind.Lib.ProfessionalNS.Controllers;
using ProFind.Lib.Global.Helpers;
using System.Threading.Tasks;

namespace ProFind.Lib.ProfessionalNS.Views.Main_Page
{
    public sealed partial class Main_Page_Professional : Page
    {
        static Dictionary<string, Type> DefinedPagesDictionary = new Dictionary<string, Type>()
        {
            {"ProjectsOverview_Page_Professionals", typeof(CRUDPage.ProjectNS.ListPage.List_Page_Projects) },
            {"Projects_Page_Professionals", typeof(CRUDPage.ProjectNS.ListPage.List_Page_Projects) },
            {"Clients_Page_Professionals", typeof(CRUDPage.ClientNS.ListPage.ListPageClient) },
            {"GeneralNotifications_Page_Professionals", typeof(CRUDPage.NotificationNS.CreatePage.CreatePage)},
            {"ProposalNotifications_Page_Professionals", typeof(CRUDPage.ProposalNS.ReadPage.ReadPage) },
            {"Preferences_Page", typeof(Preferences_Page) },
            {"About_Page",typeof(About_Page) },
            {"", typeof(CRUDPage.ProjectNS.ReadPage.ReadPage) }
        };
        public Main_Page_Professional()
        {
            this.InitializeComponent();
            new InAppNavigationController().Init(Professionals_ContentFrame);
            new InAppNavigationController().NavigateTo(typeof(CRUDPage.ProjectNS.ReadPage.ReadPage));

            LoadLoggedProfessionalDataAsync();
        }

        private async Task LoadLoggedProfessionalDataAsync()
        {
            var loggedUserInfo = LoggedProfessionalStore.LoggedProfessional;

            LoggedUser_pp.ProfilePicture = await loggedUserInfo.PictureP.FromBase64String();
            LoggedUserName_tb.Text = loggedUserInfo.NameP;
            LoggedUserEmail_tb.Text = loggedUserInfo.EmailP;
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
                new InAppNavigationController().NavigateTo(DefinedPagesDictionary["Projects_Page_Professionals"]);
            }
        }

        private void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
        }

        private void Page_Loaded_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            new InAppNavigationController().NavigateTo(DefinedPagesDictionary["Projects_Page_Professionals"]);
        }
    }
}
