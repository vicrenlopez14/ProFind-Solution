using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ProFind.Lib.Global.Controllers;
using ProFind.Lib.Global.Helpers;
using ProFind.Lib.ClientNS.Views.Main_Page;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.Global.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Clients_Register : Page
    {
        public Clients_Register()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mail_tb.Text == "" || pass_tb.Password == "" || name_tb.Text == "")
            {
                warning_tb.Text = "Please do not leave empty fields";
                return;
            }

            if (!FieldsChecker.CheckEmail(mail_tb.Text))
            {
                warning_tb.Text = "Please enter a valid email";
                return;
            }
            if (!FieldsChecker.CheckPassword(pass_tb.Password, confirm_tb.Password))

            {
                warning_tb.Text = "The password must be 5 characters long at least";
                return;
            }


            if (registerIt(Nanoid.Nanoid.Generate(), name_tb.Text, mail_tb.Text, pass_tb.Password))
            {
                new GlobalNavigationController().NavigateTo(typeof(Main_Page_Client));
            }
            else
            {
                warning_tb.Text = "Something went wrong.";
            }
        }

        public bool registerIt(string id, string name, string email, string password)
        {
            Connection connectionObj = new Connection();

            var query = $"INSERT INTO Client  (Id_C, Name_C, Email_C, Password_C) VALUES('{id}', '{name}', '{email}', '{password}');";

            try
            {
                var results = Connection.ExecuteNonQueryInDatabase(query) > 0;

                return results;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

    }


}
