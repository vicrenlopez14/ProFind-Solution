using Windows.UI.Xaml.Controls;
using ProFind.Lib.Global.Services;
using Profession = ProFind.Lib.Global.Services.Profession;
using ProFind.Lib.Global.Controllers;
using System.Collections.Generic;
using ProFind.Lib.AdminNS.Controllers;
using Windows.UI.Popups;
using System;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.ProfessionNS.ListPage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class List_Page : Page
    {
        public List_Page()
        {
            this.InitializeComponent();
            InitializeData();
        }
        private async void InitializeData()
        {
            ProfessionalsListView.ItemsSource = await APIConnection.GetConnection.GetProfessionsAsync() as List<Profession>;
        }
    

       

        private async void Delete_Click_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                var obj = (ProfessionalsListView.SelectedItem as Profession);
                await APIConnection.GetConnection.DeleteProfessionAsync(obj.IdPfs.GetValueOrDefault());
                var dialog = new MessageDialog("The profession has been deleted.");
                await dialog.ShowAsync();
            }
            catch (ProFindServicesException ex)
            {
                if (ex.StatusCode >= 200 && ex.StatusCode <= 205)
                {
                    var dialog = new MessageDialog("The profession has been deleted.");
                    await dialog.ShowAsync();
                }
                else
                {
                    var dialog = new MessageDialog("You have to select a profession.");
                    await dialog.ShowAsync();
                }
            }
            finally
            {
                InitializeData();
            }
        }

        private void Add_Click_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            new InAppNavigationController().NavigateTo(typeof(ProFind.Lib.AdminNS.Views.CRUDPages.ProfessionNS.CreatePage.Profession_Create));

        }
    }
}
