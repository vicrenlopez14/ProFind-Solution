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

namespace ProFind.Lib.ClientNS.Views.CRUDPages.SecurityAnswerClientNS.UpdatePage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class UpdatePage : Page
    {
        Securityanswerclient id = new Securityanswerclient();

        public UpdatePage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var toUpdapteAnswerClient = new Securityanswerclient(Answer1_tb.Text, Answer2_tb_tb.Text);

            await APIConnection.GetConnection.PostSecurityanswerclientAsync(id.IdSa, toUpdapteAnswerClient);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
