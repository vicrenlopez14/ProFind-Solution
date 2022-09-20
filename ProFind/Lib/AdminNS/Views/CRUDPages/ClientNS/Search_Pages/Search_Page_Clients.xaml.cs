using Windows.UI.Xaml.Controls;
using ProFind.Lib.Global.Services;
using Client = ProFind.Lib.Global.Services.Client;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.ClientNS.Search_Pages
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Search_Page_Clients : Page
    {
        Client id;

        public Search_Page_Clients()
        {
            this.InitializeComponent();
        }

        private async void Control2_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            ClientListView.ItemsSource = await APIConnection.GetConnection.SearchClientAsync(id.IdC, Search_Client.Text);

        }
    }
}
