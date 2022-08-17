using Application.Models;
using Application.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace ProFind.Lib.Client.Views.Notifications_Page
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class NotificationCreation_Page : Page
    {
        public NotificationCreation_Page()
        {
            this.InitializeComponent();
        }

        private async void btnSend_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void btnSend_Click_1(object sender, RoutedEventArgs e)
        {
            PFNotification ClientNotification = new PFNotification();
            ClientNotification.Project = new PFProject();
            ClientNotification.Project.ResponsibleClient = new PFClient();
            ClientNotification.Project.ResponsibleClient.NameC = ClientName_txb.Text;
            ClientNotification.TitleN = Title_txb.Text;
            ClientNotification.DescriptionN = Description_txb.Text;
            ClientNotification.Project.ResponsibleProfessional = new PFProfessional();
            ClientNotification.Project.ResponsibleProfessional.Profession = new PFProfession();
            ClientNotification.Project.ResponsibleProfessional.Profession.NamePFS = TypeProfession_txb.Text;

            var answer = new PFNotificationService();
            await answer.Create(ClientNotification);
        }

        private void btnSend_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClientName_txb.Text = string.Empty;
            Title_txb.Text = string.Empty;
            Description_txb.Text = string.Empty;
            TypeProfession_txb.Text = string.Empty;
        }
    }
}
