using ProFind.Lib.Global.Services;
using Windows.UI.Xaml.Controls;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.ProfessionalNS.Views.CRUDPage.ClientNS.ListPage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class List_myClients : Page
    {
        Client Id1 = new Client();

        public List_myClients()
        {
            this.InitializeComponent();
            GetClientList();
        }
        public async void GetClientList()
        {

            await APIConnection.GetConnection.GetClientAsync(Id1.IdC);

        }
    }
}
