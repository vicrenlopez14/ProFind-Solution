using ProFind.Lib.Global.Services;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.ProfessionNS.UpdatePage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Update_Profesion : Page
    {
        Profession pro = new Profession();
        public Update_Profesion()
        {
            this.InitializeComponent();
        }

        private async void Update_btn_Click(object sender, RoutedEventArgs e)
        {
            int idNo = pro.Ids.GetValueOrDefault();
            var toUpdateProfession = new Profession(int.Parse(""), Name_tb.Text);
            await APIConnection.GetConnection.PutProfessionAsync(idNo, toUpdateProfession);
        }

        private async void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            int idNo = pro.IdPfs.GetValueOrDefault();

            await APIConnection.GetConnection.DeleteProfessionAsync(idNo);
            
        }
    }
}
