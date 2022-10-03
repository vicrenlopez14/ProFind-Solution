using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.Global.Views.About_Page;
using ProFind.Lib.Global.Views.Preferences_Page;
using ProFind.Lib.Global.Services;
using ProFind.Lib.Global.Helpers;
using ProFind.Lib.ProfessionalNS.Controllers;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.Main_Page_Admin
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Main_Page_Admin : Page
    {
        public static Dictionary<string, Type> DefinedPagesDictionary = new Dictionary<string, Type>()
        {
            #region AdminPages
            {"ProjectsOverview_Page", null},
            //Projects overview
            {"Projects_Page", typeof(Lib.AdminNS.Views.CRUDPages.ProjectNS.ReadPage.ReadPage)},
            //People
            {"People_Page", typeof(Lib.AdminNS.Views.CRUDPages.AdminNS.ListPage.ListPageAdmin) },
            {"Administrators_Page", typeof(Lib.AdminNS.Views.CRUDPages.AdminNS.ListPage.ListPageAdmin)},
            {"Clients_Page", typeof(Lib.AdminNS.Views.CRUDPages.ClientNS.ListPage.Clients_List)},
            {"Professionals_Page", typeof(Lib.AdminNS.Views.CRUDPages.ProfessionalNS.ListPage.ReadPage)},
            // Platform
            {"Professions_Page", typeof(Lib.AdminNS.Views.CRUDPages.ProfessionNS.ListPage.List_Page) },
            {"Ranks_Page", typeof(Lib.AdminNS.Views.CRUDPages.RankNS.ListPage.List_Ranks) },
         
            {"ProposalNotifications_Page", null },
            #endregion

            //Footer
            {"Preferences_Page", typeof(Preferences_Page)},
            {"Profile_Page", typeof(About_Page) },
        };

        public Main_Page_Admin()
        {
            this.InitializeComponent();
            new InAppNavigationController().Init(ContentFrame);
            LoadLoggedAdminDataAsync();
        }

        private async Task LoadLoggedAdminDataAsync()
        {
            var loggedUserInfo = LoggedAdminStore.LoggedAdmin;

            LoggedUser_pp.ProfilePicture = await loggedUserInfo.PictureA.FromBase64String();
            LoggedUserName_tb.Text = loggedUserInfo.NameA;
            LoggedUserEmail_tb.Text = loggedUserInfo.EmailA;
        }

        private void NavigationView_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                new Controllers.InAppNavigationController().NavigateTo(typeof(Preferences_Page));
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

            var page = DefinedPagesDictionary[selectedItemTag];

            if (page != null)
            {
                try
                {
                    navView.Header = item.Content;
                    new Controllers.InAppNavigationController().NavigateTo(page);
                }
                catch
                {
                    new Controllers.InAppNavigationController().NavigateTo(DefinedPagesDictionary["Preferences_Page"]);
                }
            }

        }

        private void navView_BackRequested(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewBackRequestedEventArgs args)
        {
            new Controllers.InAppNavigationController().GoBack();
        }
    }
}
