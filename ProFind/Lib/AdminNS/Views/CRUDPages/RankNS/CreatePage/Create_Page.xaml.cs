using ProFind.Lib.Global.Services;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.RankNS.CreatePage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Create_Page : Page
    {
        public Create_Page()
        {
            this.InitializeComponent();
        }

        private async void Title_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private async void Create_btn_Click(object sender, RoutedEventArgs e)
        {
            var toCreateClien = new Rank(int.Parse(""), Name_tb.Text);


            var result = await APIConnection.GetConnection.PostRankAsync(toCreateClien);
        }
    }
}
