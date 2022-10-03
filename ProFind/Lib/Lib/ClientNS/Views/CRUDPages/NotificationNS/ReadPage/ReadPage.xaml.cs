using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.Global.Services;
using ProFind.Lib.ClientNS.Controllers;
using System.Linq;
using System.Collections.Generic;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.ClientNS.Views.CRUDPages.NotificationNS.ReadPage
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
            var loggendClient = LoggedClientStore.LoggedClient;
            var project = await APIConnection.GetConnection.GetProjectsAsync();
            var Notification = await APIConnection.GetConnection.GetNotificationsAsync();

            var RelatedProjects = project.Where(c => c.IdC1 == loggendClient.IdC).ToList();

            var RelatedNotification = new List<Notification>();


            foreach (var Projects in project)
            {
                var RelatedANotificationsForRelatedProject = Notification.Where(n => n.IdPj2Navigation.IdC1 == LoggedClientStore.LoggedClient.IdC).ToList();
                RelatedNotification.AddRange(RelatedANotificationsForRelatedProject);
            }

            NotificationListView.ItemsSource = RelatedNotification ;
           
        }


        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
        
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

   

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

        }
    }
}
