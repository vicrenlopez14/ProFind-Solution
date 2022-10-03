using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.Global.Services;
using ProFind.Lib.ProfessionalNS.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.ProfessionalNS.Views.CRUDPage.NotificationNS.ReadPage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class ReadPage : Page
    {
        public ReadPage()
        {
            this.InitializeComponent();
            Cargar();
        }
        private async void Cargar()
        {
            var LeggedProfesionales = LoggedProfessionalStore.LoggedProfessional;
            var notifications = await APIConnection.GetConnection.GetNotificationsAsync();
            var RelatedProfesionales = notifications.Where(p => p.IdP1 == LeggedProfesionales.IdP).ToList();


            Activities_lw.ItemsSource = RelatedProfesionales;

        }

        private async void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Activities_lw.SelectedItem != null)
                {
                    var selectedNoti = Activities_lw.SelectedItem as Notification;
                    await APIConnection.GetConnection.DeleteNotificationAsync(selectedNoti.IdN);

                }
                else
                {

                    var dialog = new MessageDialog("You have to select a Notification.");
                    await dialog.ShowAsync();

                }
              

            }
            catch (ProFindServicesException ex)
            {
                if (ex.StatusCode >= 200 && ex.StatusCode <= 205)
                {
                    var dialog = new MessageDialog("Notification deleted successfully.");
                    await dialog.ShowAsync();
                }
                else
                {
                    var dialog = new MessageDialog("You have to select an Notification.");
                    await dialog.ShowAsync();
                }
            }
            finally
            {
                Cargar();
            }
        }
        }
    }

