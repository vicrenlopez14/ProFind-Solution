using ProFind.Lib.Global.Helpers;
using ProFind.Lib.Global.Services;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.ClientNS.CreatePage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Create_page : Page
    {
        Client toManipulate = new Client();
        public Create_page()
        {
            this.InitializeComponent();
        }

      

        private async void Create_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                byte[] da = toManipulate.PictureC = await(await PickFileHelper.PickImage()).ToByteArrayAsync();
                var toCreateAdmin = new Client(Name_tb.Text, Email_tb.Text, Password_pb.Password, da );
               

                var result = await APIConnection.GetConnection.PostClientAsync(toCreateAdmin);

                ToggleThemeTeachingTip2.IsOpen = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                
            }
        }

        private void PictureSelection_btn_Checked(object sender, RoutedEventArgs e)
        {
            PictureSelection_btn.IsChecked = !PictureSelection_btn.IsChecked;
        }
    }
}
