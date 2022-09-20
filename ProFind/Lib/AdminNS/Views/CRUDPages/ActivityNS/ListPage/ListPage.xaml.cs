using Windows.UI.Xaml.Controls;
using ProFind.Lib.Global.Services;
using ProFind.Lib.Global.Controllers;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.ActivityNS.ListPage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListPage : Page
    {
        public ListPage()
        {
            this.InitializeComponent();
            GetProjectsList();
        }

        public async void GetProjectsList()
        {
            Activities_lw.ItemsSource = await APIConnection.GetConnection.GetProjectsAsync();
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            new GlobalNavigationController().NavigateTo(typeof(ProFind.Lib.AdminNS.Views.CRUDPages.ActivityNS.CreatePage.CreatePage));
        }

        private void Button_Click_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            new GlobalNavigationController().NavigateTo(typeof(ProFind.Lib.AdminNS.Views.CRUDPages.ActivityNS.CreatePage.CreatePage));
        }

        private void Button_Click_2(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            new GlobalNavigationController().NavigateTo(typeof(ProFind.Lib.AdminNS.Views.CRUDPages.ActivityNS.SearchPage.SearchPageActivi));
        }

        private void Add_btn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            new GlobalNavigationController().NavigateTo(typeof(ProFind.Lib.AdminNS.Views.CRUDPages.ActivityNS.ReadPage.ReadPage));
        }
    }
}
