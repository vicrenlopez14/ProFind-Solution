using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.Global.Helpers;
using ProFind.Lib.Global.Services.Models;
using System;
using System.Collections.Generic;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.Project.CreatePage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class CreatePage : Page
    {
        private Project newObject = new Project();

        private string[] status = Enum.GetNames(typeof(ProjectStatus));
        private List<Professional> professionals = new List<Professional>();
        private List<string> professionalStrings = new List<string>();

        private List<Client> clients = new List<Client>();
        private List<string> clientStrings = new List<string>();

        public CreatePage()
        {
            this.InitializeComponent();
            loadUsefulThings();
        }

        public async void loadUsefulThings()
        {
            (professionals, professionalStrings) = await new PFProfessionalService().GetComboboxChoices();
            (clients, clientStrings) = await new PfClientService().GetComboboxChoices();

            InitialStatus_cb.ItemsSource = status;
            Professional_cb.ItemsSource = professionalStrings;
            Client_cb.ItemsSource = clientStrings;
        }


        private async void Create_btn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Title_tb.Text))
            {

                var dialog = new MessageDialog("The field is empty");
                await dialog.ShowAsync();
                return;
            }
            else if (string.IsNullOrEmpty(Description_tb.Text))
            {
                var dialog = new MessageDialog("The field is empty");
                await dialog.ShowAsync();
                return;
            }

            var result = await new PfProjectService().Create(newObject);

            if (result == System.Net.HttpStatusCode.OK)
            {
                new InAppNavigationController().GoBack();
            }

        }

        private async void PictureSelection_btn_Checked(object sender, RoutedEventArgs e)
        {
            PictureSelection_btn.IsChecked = !PictureSelection_btn.IsChecked;
        }

        private async void PictureSelection_btn_Click(object sender, RoutedEventArgs e)
        {
            newObject.PicturePJ = await (await PickFileHelper.PickImage()).ToByteArrayAsync();

        }

        private void Title_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            newObject.TitlePJ = Title_tb.Text;
        }

        private void Description_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            newObject.DescriptionPJ = Description_tb.Text;
        }



        private void TotalPrice_tb_ValueChanged(Microsoft.UI.Xaml.Controls.NumberBox sender, Microsoft.UI.Xaml.Controls.NumberBoxValueChangedEventArgs args)
        {
            newObject.TotalPricePJ = (float)sender.Value;
        }

        private void InitialStatus_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PFProjectStatus selectedStatus = PFProjectStatus.Inactive;

            if (InitialStatus_cb.SelectedIndex == 1)
                selectedStatus = PFProjectStatus.Active;

            newObject.Status = selectedStatus;
            newObject.IdPS1 = selectedStatus == PFProjectStatus.Inactive ? "0" : "1";
        }

        private void Professional_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            newObject.ResponsibleProfessional = professionals[Professional_cb.SelectedIndex];
        }

        private void Client_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            newObject.ResponsibleClient = clients[Client_cb.SelectedIndex];
        }
    }
    
}
