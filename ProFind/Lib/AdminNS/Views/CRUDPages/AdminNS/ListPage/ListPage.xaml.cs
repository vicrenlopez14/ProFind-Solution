using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.Global.Services;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.AdminNS.ListPage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListPage : Page
    {
        List<Admin> DEBUG_LIST = new List<Admin> {
            new Admin() {
                IdA = "1",
                NameA = "Admin 1",
                EmailA = "example@mail.com",
                PasswordA = "123456",
            },
            new Admin() {
                IdA = "2",
                NameA = "Admin 2",
                EmailA = "example@mail.com",
                PasswordA = "123456",
                },
            new Admin() {
                IdA = "3",
                NameA = "Admin 3",
                EmailA = "example@mail.com",
            },

        };

        public ListPage()
        {
            this.InitializeComponent();

            InitializeData();
        }

        private async void InitializeData()
        {
            AdminsListView.ItemsSource = await APIConnection.GetConnection.GetAdminsAsync();
        }

        private void AdminListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var admin = e.ClickedItem as Admin;

            new InAppNavigationController().NavigateTo(typeof(UpdatePage.UpdatePage), admin);
        }

        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            new InAppNavigationController().NavigateTo(typeof(CreatePage.CreatePage));
        }

        private void ProfessionalsListView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}
