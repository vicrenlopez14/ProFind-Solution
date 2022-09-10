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

namespace ProFind.Lib.AdminNS.Views.CRUDPages.SecurityAnswerAdmins.AnswersPage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class RestorePasswordSQ : Page
    {
        Admin admin = new Admin();
        public RestorePasswordSQ()
        {
            this.InitializeComponent();
        }

        private async void Chage_Click(object sender, RoutedEventArgs e)
        {
            if(PasswordNew == PasswordTryAgain)
            {
                var toUpdate = new Admin(admin.IdA, admin.NameA, admin.EmailA, admin.TelA, PasswordNew.password, admin.PictureA);
                await APIConnection.GetConnection.PutAdminAsync(admin.IdA, toUpdate);
            }
            else
            {
                var dialog = new MessageDialog("The password is not the same, try again");
                await dialog.ShowAsync();
                PasswordNew.Clear();
                PasswordTryAgain.Clear();


                return;
            }
            
        }
    }
}
