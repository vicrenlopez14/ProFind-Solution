using Windows.UI.Xaml.Controls;
using ProFind.Lib.Global.Services;
using Rank = ProFind.Lib.Global.Services.Rank;
using ProFind.Lib.Global.Controllers;
using System.Collections.Generic;
using ProFind.Lib.AdminNS.Controllers;
using Windows.UI.Popups;
using System;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.RankNS.ListPage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class List_Ranks : Page
    {

        public List_Ranks()
        {
            this.InitializeComponent();
            InitializeData();
        }
        private async void InitializeData()
        {


            Ranks_lw.ItemsSource =  await APIConnection.GetConnection.GetRanksAsync();

            Ranks_lw.ItemsSource = await APIConnection.GetConnection.GetRanksAsync() as List<Rank>;

        }

    

   


        private async void Delete_Click_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

            try
            {
                var obj = Ranks_lw.SelectedItem as Rank;
                await APIConnection.GetConnection.DeleteRankAsync(obj.IdR.GetValueOrDefault());
                var dialog = new MessageDialog("The rank has been deleted");
                await dialog.ShowAsync();
            }
            catch (ProFindServicesException ex)
            {
                if (ex.StatusCode >= 200 && ex.StatusCode <= 205)
                {
                    var dialog = new MessageDialog("The rank has been deleted");
                    await dialog.ShowAsync();
                }
                else
                {
                    var dialog = new MessageDialog("There was a problem while creating the rank, try again later");
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
            new InAppNavigationController().NavigateTo(typeof(ProFind.Lib.AdminNS.Views.CRUDPages.RankNS.CreatePage.Create_Page));

        }
    }
}
