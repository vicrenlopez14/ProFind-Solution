using ProFind.Lib.Global.Services;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.ProjectStatusNS.UpdatePage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Update_delete_Project_status : Page
    {
        Projectstatus pro = new Projectstatus();
        public Update_delete_Project_status()
        {
            this.InitializeComponent();
        }

        private void Title_tb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private async void Update_btn_Click(object sender, RoutedEventArgs e)
        {
            int idNo = pro.IdPs.GetValueOrDefault();
            var toUpdateProfession = new Projectstatus(int.Parse(""), Name_tb.Text);
            await APIConnection.GetConnection.PutProjectstatusAsync(idNo, toUpdateProfession);
           
        }

        private async void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            int idNo = pro.IdPs.GetValueOrDefault();
            
            await APIConnection.GetConnection.DeleteProjectstatusAsync(idNo);
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
