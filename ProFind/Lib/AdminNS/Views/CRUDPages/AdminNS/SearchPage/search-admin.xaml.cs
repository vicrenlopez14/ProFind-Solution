﻿using ProFind.Lib.Global.Services;
using Windows.UI.Xaml.Controls;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.ProfessionalNS.Views.CRUDPages.ProfessionalNS.SearchPage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class search_admin : Page
    {
        Admin id = new Admin();
        
        public search_admin()
        {
            this.InitializeComponent();
        }

        private async void Control2_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
           var Resul =   await APIConnection.GetConnection.SearchAdminsAsync(id.IdA, Search_admin1.text);

            await APIConnection.GetConnection.GetAdminAsync(Resul);

        }
    }
}
