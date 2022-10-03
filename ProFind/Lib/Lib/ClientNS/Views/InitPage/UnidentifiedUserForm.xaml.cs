using Windows.UI.Xaml.Controls;
using ProFind.Lib.AdminNS.Views.Main_Page_Admin;
using ProFind.Lib.ClientNS.Views.Main_Page;
using ProFind.Lib.Global.Controllers;
using ProFind.Lib.ProfessionalNS.Views.Main_Page;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace ProFind.Lib.ClientNS.Views.InitPage
{
    public sealed partial class UnidentifiedUserForm : UserControl
    {
        public UnidentifiedUserForm()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ButtonClickHomeMethod();
        }



        private void mail_tb_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                e.Handled = true;
                ButtonClickHomeMethod();
            }
        }

        void ButtonClickHomeMethod()
        {



        

        }
    }


}