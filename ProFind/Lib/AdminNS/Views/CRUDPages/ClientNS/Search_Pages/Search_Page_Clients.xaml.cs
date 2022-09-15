using ProFind.Lib.Global.Services;
using Windows.UI.Xaml.Controls;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.ClientNS.Search_Pages
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Search_Page_Clients : Page
    {
        Client id = new Client();

        public Search_Page_Clients()
        {
            this.InitializeComponent();
        }

        private async void Control2_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            var Resul = await APIConnection.GetConnection.SearchAdminsAsync(id.IdC, Search_Client.text);

            await APIConnection.GetConnection.GetAdminAsync(Resul);
        }
    }
}
