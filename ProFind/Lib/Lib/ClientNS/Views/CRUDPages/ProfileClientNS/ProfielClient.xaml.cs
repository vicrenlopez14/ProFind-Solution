using ProFind.Lib.ClientNS.Controllers;
using ProFind.Lib.Global.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.ClientNS.Views.CRUDPages.ProfileClientNS
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class ProfielClient : Page
    {
        public ProfielClient()
        {
            this.InitializeComponent();
            InitializeData();
        }

        private async void InitializeData()
        {
            var loggendClient = LoggedClientStore.LoggedClient;
            var client = await APIConnection.GetConnection.GetClientsAsync();
            var RelatedClient = new List<Client>();

            var RelatedClient2 = client.Where(c => c.IdC == loggendClient.IdC).ToList();

            RelatedClient.AddRange(RelatedClient2);

            ListViewProfileClient.ItemsSource = RelatedClient;
        }
    }
}
