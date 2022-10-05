using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using ProFind.Lib.Global.Helpers;
using ProFind.Lib.Global.Services;
using Department = ProFind.Lib.Global.Services.Department;
using Profession = ProFind.Lib.Global.Services.Profession;
using Professional = ProFind.Lib.Global.Services.Professional;
using ProFind.Lib.AdminNS.Controllers;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.ProfessionalNS.ReadPage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class ReadPage : Page
    {


        Profession selectedProfession;
        Department selectedDepartment;

        Professional ToManipulateProfessional = new Professional();

        private string imageString;

        private byte[] curriculumBytes;

        private bool _isFirstAdmin;

        public ReadPage()
        {
            this.InitializeComponent();
            loadUsefulThings();
            AddEvents();
        }

        private void AddEvents()
        {
            FirstName1_tbx.OnEnterNextField();
            Afp.OnEnterNextField();
            Dui.OnEnterNextField();
            SeguroSocial.OnEnterNextField();
            CodigoPostal.OnEnterNextField();
            Email.OnEnterNextField();
            Salario.OnEnterNextField();
        }

        public async void loadUsefulThings()
        {

            profession_cbx.ItemsSource = await APIConnection.GetConnection.GetProfessionsAsync();
            profession_cbx.SelectedIndex = 0;

            departamento.ItemsSource = await APIConnection.GetConnection.GetDepartmentsAsync();
            departamento.SelectedIndex = 0;

            ProfilePicture_pp.ProfilePicture = await ToManipulateProfessional.PictureP.FromBase64String();

        }
        private void position_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {

        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null)
            {
                ToManipulateProfessional = (Professional)e.Parameter;
                AutoComplete();
            }
            else
            {
                // Go back to professionals list
                // Error message dialog
                var dialog = new MessageDialog("Professional not found or  not valid.");
                await dialog.ShowAsync();

                // Back to profesionals list
                new InAppNavigationController().NavigateTo(typeof(Lib.AdminNS.Views.CRUDPages.ProfessionalNS.ListPage.ReadPage));
            }
        }

        private async void AutoComplete()
        {
            FirstName1_tbx.Text = ToManipulateProfessional.NameP;
            if (ToManipulateProfessional.IdPfs1 == 1) profession_cbx.SelectedIndex = 0;

            if (ToManipulateProfessional.IdPfs1 == 2) profession_cbx.SelectedIndex = 2;

            if (ToManipulateProfessional.IdPfs1 == 3) profession_cbx.SelectedIndex = 3;

            Afp.Text = ToManipulateProfessional.Afpp;
            SeguroSocial.Text = ToManipulateProfessional.Isssp;
            Dui.Text = ToManipulateProfessional.Duip;
            FechadeIngreso.Date = ToManipulateProfessional.HiringDateP;
            Email.Text = ToManipulateProfessional.EmailP;
            CodigoPostal.Text = ToManipulateProfessional.ZipCodeP;
            Salario.Text = ToManipulateProfessional.SalaryP.ToString();
            Nacimiento.Date = (DateTimeOffset)ToManipulateProfessional.DateBirthP;
            FirstName1_tbx.Text = ToManipulateProfessional.NameP;
            ProfilePicture_pp.ProfilePicture = await ToManipulateProfessional.PictureP.FromBase64String();
            imageString = ToManipulateProfessional.PictureP;
            FirstName1_tbx.IsEnabled = false;
            profession_cbx.IsEnabled = false;
            Afp.IsEnabled = false;
            SeguroSocial.IsEnabled = false;
            Dui.IsEnabled = false;
            FechadeIngreso.IsEnabled = false;
            Email.IsEnabled = false;
            CodigoPostal.IsEnabled = false;
            Salario.IsEnabled = false;
            Nacimiento.IsEnabled = false;
            profession_cbx.IsEnabled = false;
           

        }

        private async void btnExaminar_Click_1(object sender, RoutedEventArgs e)
        {
            
        }

        private void Selection_Sexo(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TxtNumEmpleado(object sender, TextChangedEventArgs e)
        {

        }

        private void TxtNum_SeguroSocial(object sender, TextChangedEventArgs e)
        {

        }

        private void TxtPuestoDeTrabajo(object sender, TextChangedEventArgs e)
        {

        }

        private void Selection_Departamento(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TxtDui(object sender, TextChangedEventArgs e)
        {

        }

        private void TxtCodigo_Postal(object sender, TextChangedEventArgs e)
        {

        }

        private void TxtEmail(object sender, TextChangedEventArgs e)
        {

        }

        private void txtMetodoDePago(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TxtSalario(object sender, TextChangedEventArgs e)
        {

        }

        private void Selection_Tipo_Jornada(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TxtAFP(object sender, TextChangedEventArgs e)
        {

        }

       

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private async void btnExaminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TxtFIstName(TextBlock sender, TextChangedEventArgs e)
        {

        }

        private void TxtLastName(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionProfesional(object sender, SelectionChangedEventArgs e)
        {

        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void TxtFIstName(object sender, TextChangedEventArgs e)
        {

        }

        private async void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            await APIConnection.GetConnection.DeleteProfessionalAsync(ToManipulateProfessional.IdP);
        }

        private void FirstName1_tbx_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {

        }

        private void Afp_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {

        }

        private void SeguroSocial_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {

        }

        private void TextBox_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
        }

        private void CodigoPostal_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
        }

        private void Email_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {

        }

        private void Salario_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {

        }

        


        private void Nacimiento_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {

        }

        private void SelectCurriculum_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}