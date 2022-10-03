using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.Global.Views.About_Page;
using ProFind.Lib.Global.Views.Preferences_Page;
using ProFind.Lib.ProfessionalNS.Controllers;
using ProFind.Lib.Global.Helpers;
using ProFind.Lib.ClientNS.Controllers;
using System.Threading.Tasks;

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
            {"Projects_Page_Clients", typeof(Lib.ClientNS.Views.CRUDPages.ProjectNS.ReadPage.ReadPage) },
            {"Notifications_Page", typeof(Lib.ClientNS.Views.CRUDPages.NotificationNS.ReadPage.ReadPage) },
            {"Catalog_Page", typeof(Lib.ClientNS.Views.CRUDPages.CatalogNS.CatalogList.CatalogList) },
            {"Professionals_Page_Clients", typeof(Lib.ClientNS.Views.CRUDPages.ProfessionalNS.ReadPage.ReadPage) },
            {"GeneralNotifications_Page_Clients", typeof(Lib.ClientNS.Views.CRUDPages.NotificationNS.ReadPage.ReadPage) },
            {"ProposalNotifications_Page_Clients", typeof(Lib.ClientNS.Views.CRUDPages.NotificationNS.ReadPage.ReadPage) },
            {"ProjectsOverview_Page", typeof(Lib.ClientNS.Views.CRUDPages.ProjectNS.ReadPage.ReadPage) },
            {"Projecttype_Page", typeof(Lib.ClientNS.Views.CRUDPages.NotificationNS.ReadPage.ReadPage) },
            {"Preferences_Page", typeof(Preferences_Page) },
            {"About_Page",typeof(About_Page) },
            {"", typeof(CRUDPages.ProjectNS.ReadPage.ReadPage) }
        };

        public Main_Page_Client()
        {
            this.InitializeComponent();
            new InAppNavigationController().Init(ClientsContentFrame);
            new InAppNavigationController().NavigateTo(typeof(CRUDPages.ProjectNS.ReadPage.ReadPage));

            LoadLoggedProfessionalDataAsync();
        }

        private async Task LoadLoggedProfessionalDataAsync()
        {
            var loggedUserInfo = LoggedClientStore.LoggedClient;

            LoggedUser_pp.ProfilePicture = await loggedUserInfo.PictureC.FromBase64String();
            LoggedUserName_tb.Text = loggedUserInfo.NameC;
            LoggedUserEmail_tb.Text = loggedUserInfo.EmailC;
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

            sender.Header = item.Content.ToString();

            if (DefinedPagesDictionary[selectedItemTag] == null)
            {
                new InAppNavigationController().NavigateTo(DefinedPagesDictionary["Projects_Page_Clients"]);
            }

            new InAppNavigationController().NavigateTo(DefinedPagesDictionary[selectedItemTag]);
        }
    }
}
