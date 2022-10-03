using System;
using System.Collections.Generic;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Controls;
using ProFind.Lib.Global.Helpers;
using ProFind.Lib.Global.Services;
using Department = ProFind.Lib.Global.Services.Department;
using Profession = ProFind.Lib.Global.Services.Profession;
using Professional = ProFind.Lib.Global.Services.Professional;
using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.ClientNS.Views.InitPage;
using ProFind.Lib.Global.Controllers;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.ProfessionalNS.CreatePage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class ProfessionalInformationAddition : Page
    {
        bool isFirstProfessional;

        Profession profession = new Profession();
        Department department;

        Professional toManipulate = new Professional();

        private string imageString;

        private byte[] curriculumBytes;

        public ProfessionalInformationAddition()
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
            position.OnEnterNextField();
            CodigoPostal.OnEnterNextField();
            Email.OnEnterNextField();
            Salario.OnEnterNextField();
        }

        public async void loadUsefulThings()
        {
            // Professions
            profession_cbx.ItemsSource = await APIConnection.GetConnection.GetProfessionsAsync();
            profession_cbx.SelectedIndex = 0;

            // Departments
            departamento.ItemsSource = await APIConnection.GetConnection.GetDepartmentsAsync();
            departamento.SelectedIndex = 0;

            // Hiring date
            FechadeIngreso.Date = DateTime.Now;

            // Birth date
            Nacimiento.Date = DateTime.Now;

            CurriculumInformation.Text = "No curriculum has been uploaded";
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                // Check if parameter is a bool
                isFirstProfessional = (bool)e.Parameter;
            }

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

        private async void btnAgregar_Click(object sender, RoutedEventArgs e)
        {

            if (!FieldsChecker.CheckName(FirstName1_tbx.Text))
            {
                var dialog = new MessageDialog("The name must be valid.");
                await dialog.ShowAsync();
                return;
            }
            if (!FieldsChecker.CheckEmail(Email.Text))
            {
                var dialog = new MessageDialog("The email must be valid.");
                await dialog.ShowAsync();
                return;
            }
            if (!FieldsChecker.CheckDateDown(Nacimiento.Date))
            {
                var dialog = new MessageDialog("The birth date must be valid.");
                await dialog.ShowAsync();
                return;
            }
            if (!FieldsChecker.OnlyFloats(Salario.Text))
            {
                var dialog = new MessageDialog("The salary must be valid.");
                await dialog.ShowAsync();
                return;
            }
            if (!FieldsChecker.OnlyInts(Afp.Text))
            {
                var dialog = new MessageDialog("The Afp must be valid.");
                await dialog.ShowAsync();
                return;
            }
            if (!FieldsChecker.OnlyInts(CodigoPostal.Text))
            {
                var dialog = new MessageDialog("The Code Postal must be valid.");
                await dialog.ShowAsync();
                return;
            }
            if (!FieldsChecker.OnlyLetters(position.Text))
            {
                var dialog = new MessageDialog("The Position must be valid.");
                await dialog.ShowAsync();
                return;
            }
            if (!FieldsChecker.CheckPassword(passwordBox.Password))
            {
                var dialog = new MessageDialog("The password must be valid.");
                await dialog.ShowAsync();
                return;
            }
            try
            {

                var toCreateProfessions = new Professional
                {
                    IdP = "",
                    NameP = FirstName1_tbx.Text,
                    EmailP = Email.Text,
                    Afpp = Afp.Text,
                    Isssp = SeguroSocial.Text,
                    Duip = Dui.Text,
                    DateBirthP = Nacimiento.Date,
                    SalaryP = int.Parse(Salario.Text),
                    SexP = true,
                    PasswordP = passwordBox.Password,
                    PhoneP = Phone_nb.Text,
                    ActiveP = Sexo.SelectedValue == "Male",
                    PictureP = imageString,
                    IdDp1 = (departamento.SelectedItem as Department).IdDp,
                    IdPfs1 = (profession_cbx.SelectedItem as Profession).IdPfs,
                    ZipCodeP = CodigoPostal.Text,
                    CurriculumP = curriculumBytes,
                    HiringDateP = FechadeIngreso.Date
                };

                var result = await APIConnection.GetConnection.PostProfessionalAsync(toCreateProfessions);

            }
            catch (ProFindServicesException ex)
            {
                if (ex.StatusCode == 201 || ex.StatusCode == 200 || ex.StatusCode == 204)
                {
                    var dialog = new MessageDialog("Professional created successfully, ProFind is now ready to work.");
                    await dialog.ShowAsync();
                    new InAppNavigationController().NavigateTo(typeof(ListPage.ReadPage));
                }
                else
                {
                    var dialog = new MessageDialog("Error creating professional");
                    await dialog.ShowAsync();
                }
            }


            if (FieldsChecker.CheckName(FirstName1_tbx.Text))
            {
                var dialog = new MessageDialog("The name must be valid.");
                await dialog.ShowAsync();
                return;
            }
            if (!FieldsChecker.CheckEmail(Email.Text))
            {
                var dialog = new MessageDialog("The email must be valid.");
                await dialog.ShowAsync();
                return;
            }
            if (!FieldsChecker.CheckDateDown(Nacimiento.Date))
            {
                var dialog = new MessageDialog("The birth date must be valid.");
                await dialog.ShowAsync();
                return;
            }
            if (FieldsChecker.OnlyFloats(Salario.Text))
            {
                var dialog = new MessageDialog("The salary must be valid.");
                await dialog.ShowAsync();
                return;
            }
            if (FieldsChecker.OnlyInts(Afp.Text))
            {
                var dialog = new MessageDialog("The Afp must be valid.");
                await dialog.ShowAsync();
                return;
            }
            if (FieldsChecker.OnlyInts(CodigoPostal.Text))
            {
                var dialog = new MessageDialog("The Code Postal must be valid.");
                await dialog.ShowAsync();
                return;
            }
            if (FieldsChecker.OnlyLetters(position.Text))
            {
                var dialog = new MessageDialog("The Position must be valid.");
                await dialog.ShowAsync();
                return;
            }
            if (!FieldsChecker.CheckPassword(passwordBox.Password))
            {
                var dialog = new MessageDialog("The password must be valid.");
                await dialog.ShowAsync();
                return;
            }


        }

        private void ToggleThemeTeachingTip2_ActionButtonClick(TeachingTip sender, object args)
        {

        }

        private void ToggleThemeTeachingTip2_CloseButtonClick(TeachingTip sender, object args)
        {

        }

        private void ToggleThemeTeachingTip2_Closed(TeachingTip sender, TeachingTipClosedEventArgs args)
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

        private async void btnExaminar_Click_1(object sender, RoutedEventArgs e)
        {
            var file = await PickFileHelper.PickImage();
            if (file == null)
            {
                return;
            }

            imageString = await (file).ToBase64StringAsync();

            ProfilePicture_pp.ProfilePicture = await imageString.FromBase64String();
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

        private void position_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
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

        private async void SelectCurriculum_Click_2(object sender, RoutedEventArgs e)
        {
            // Launch curriculum select dialog
            var result = await new Lib.AdminNS.Views.CRUDPages.CurriculumNS.CreatePage.CreateDialog().ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                // Set selected curriculum
                curriculumBytes = CurriculumNS.CreatePage.CreateDialog.curriculumBytes;

                CurriculumInformation.Text = "The curriculum has been uploaded.";
            }
            else
            {
                CurriculumInformation.Text = "No curriculum has been uploaded";
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            new InAppNavigationController().NavigateTo(typeof(AdminNS.ListPage.ListPageAdmin));
        }

        private void Phone_nb_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {

        }
    }
}
