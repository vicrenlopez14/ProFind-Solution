using ProFind.Lib.Global.Services;
using Windows.UI.Xaml.Controls;
using ProFind.Lib.AdminNS.Controllers;
using Professional = ProFind.Lib.Global.Services.Professional;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.ProfessionalNS.Views.CRUDPage.ProfessionNS.ReadPage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class ReadPage : Page
    {
        public ReadPage()
        {
            this.InitializeComponent();
            InitializeData();
        }
        private async void InitializeData()
        {

           
            await APIConnection.GetConnection.GetProfessionsAsync();

        }

        private async void ProjectsActiveListView_ItemClick(object sender, ItemClickEventArgs e)
        {
           
            
        }

        private void DashboardProfessionalsActiveListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
