using ProFind.Lib.Global.Services;
using Windows.UI.Xaml.Controls;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.ClientNS.ListPage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Clients_List : Page
    {
        Client Id1 = new Client();
        public Clients_List()
        {
            this.InitializeComponent();
            GetClientList();
        }

        public async void GetClientList()
        {
            await APIConnection.GetConnection.GetClientAsync(Id1.IdC);

        }

        private void ClientListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Client clickedItem = e.ClickedItem as Client;

           
        }
    }
}
