using Windows.UI.Xaml.Controls;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.ProfessionalNS.Views.CRUDPage.ProjectNS.UpdatePage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class UpdatePage : Page
    {
        Project toManipulate = new Project();
        private string[] status = Enum.GetNames(typeof(Projectstatus));
        private List<Professional> professionals = new List<Professional>();
        private List<string> professionalStrings = new List<string>();

        private List<Client> clients = new List<Client>();
        private List<string> clientStrings = new List<string>();

        public UpdatePage()
        {
            this.InitializeComponent();
        }
        private async void loadUsefulthings()
        {
            (professionals, professionalStrings) = await new ProfessionalService().GetComboboxChoices();
            (clients, clientStrings) = await new ClientService().GetComboboxChoices();

            InitialStatus_cb.ItemsSource = status;
            Professional_cb.ItemsSource = professionalStrings;
            Client_cb.ItemsSource = clientStrings;

            SelectedPicture_pp.Source = toManipulate.PicturePj.ToBitmapImage();
            Title_tb.Text = toManipulate.TitlePj ?? "";
            Description_tb.Text = toManipulate.DescriptionPj ?? "";
            TotalPrice_tb.Text = ((int)toManipulate.TotalPricePj).ToString();
            InitialStatus_cb.SelectedIndex = ((int)toManipulate.Status) - 1;
            Professional_cb.SelectedIndex = professionals.FindIndex(x => x.IdP == toManipulate.ResponsibleProfessional.IdP);
            Client_cb.SelectedIndex = clients.FindIndex(x => x.IdC == toManipulate.ResponsibleClient.IdC);

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            toManipulate = (Project)e.Parameter;
            loadUsefulthings();
        }

        private async void Reset_btn_Click(object sender, RoutedEventArgs e)
        {
            // Reset with the same ID
            toManipulate = new Project()
            {
                IdPj = toManipulate.IdP1,

            };
            loadUsefulthings();
        }


        private async void Update_btn_Click(object sender, RoutedEventArgs e)
        {
            //await new ProjectService().Update(toManipulate);
            await APIConnection.GetConnection.GetAdminAsync(toManipulate);
        }

        private async void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            await APIConnection.GetConnection.DeleteProjectAsync(toManipulate.IdPj);
        }

        private void Back_btn_Click(object sender, RoutedEventArgs e)
        {
            new InAppNavigationController().GoBack();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            toManipulate.PicturePj = await (await PickFileHelper.PickImage()).ToByteArrayAsync();
        }

        private void PictureSelection_btn_Checked(object sender, RoutedEventArgs e)
        {

        }

        private async void PictureSelection_btn_Click(object sender, RoutedEventArgs e)
        {
            var pickedFile = await PickFileHelper.PickImage();
            toManipulate.PicturePj = await (pickedFile).ToByteArrayAsync();
            SelectedPicture_tbk.Text = pickedFile.DisplayName;
        }

        private void Title_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            toManipulate.TitlePj = Title_tb.Text;
        }

        private void Description_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            toManipulate.DescriptionPj = Description_tb.Text;
        }

        private void TotalPrice_tb_ValueChanged(Microsoft.UI.Xaml.Controls.NumberBox sender, Microsoft.UI.Xaml.Controls.NumberBoxValueChangedEventArgs args)
        {
            toManipulate.TotalPricePj = (int)TotalPrice_tb.Value;
        }

        private void InitialStatus_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProjectStatus selectedStatus = ProjectStatus.Inactive;

            if (InitialStatus_cb.SelectedIndex == 1)
                selectedStatus = ProjectStatus.Active;

            toManipulate.Status = selectedStatus;
            toManipulate.IdPs1 = selectedStatus == ProjectStatus.Inactive ? "0" : "1";
        }

        private void Professional_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            toManipulate.ResponsibleProfessional = professionals[Professional_cb.SelectedIndex];
            toManipulate.IdP1 = professionals[Professional_cb.SelectedIndex].IdP;

        }

        private void Client_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            toManipulate.ResponsibleClient = clients[Client_cb.SelectedIndex];
            toManipulate.IdC1 = clients[Client_cb.SelectedIndex].IdC;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new InAppNavigationController().NavigateTo(typeof(ReadPageActivity), toManipulate);
        }
    }
}
