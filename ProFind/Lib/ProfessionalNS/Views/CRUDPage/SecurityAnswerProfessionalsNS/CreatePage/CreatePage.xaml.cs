using ProFind.Lib.Global.Services;
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

namespace ProFind.Lib.ProfessionalNS.Views.CRUDPage.SecurityAnswerProfessionalsNS.CreatePage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class CreatePage : Page
    {
        public CreatePage()
        {
            this.InitializeComponent();
        }

        private async void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Answer1_tb.Text))
            {

                var dialog = new MessageDialog("The field is empty");
                await dialog.ShowAsync();
                return;
            }
            else if (string.IsNullOrEmpty(Answer2_tb.Text))
            {
                var dialog = new MessageDialog("The field is empty");
                await dialog.ShowAsync();
                return;
            }

            try
            {

                var toCreateAnswerProfessional = new Securityanswerprofessional(Answer1_tb.Text, Answer2_tb.Text);


                var result = await APIConnection.GetConnection.PostSecurityanswerprofessionalAsync(toCreateAnswerProfessional);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
