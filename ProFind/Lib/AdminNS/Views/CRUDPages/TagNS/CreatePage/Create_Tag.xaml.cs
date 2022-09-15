using ProFind.Lib.Global.Services;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.TagNS.CreatePage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Create_Tag : Page
    {
        public Create_Tag()
        {
            this.InitializeComponent();
        }

        private async void Create_btn_Click(object sender, RoutedEventArgs e)
        {
            var toCreateClien = new Tag(int.Parse(""), Name_tb.Text);

            await APIConnection.GetConnection.PostTagAsync(toCreateClien);
        }

        private void Title_tb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
