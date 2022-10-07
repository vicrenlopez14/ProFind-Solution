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

namespace ProFind.Lib.AdminNS.Views.CRUDPages.ProfessionalNS.UpdatePage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Editar_User : Page
    {


        Profession selectedProfession;
        Department selectedDepartment;

        Professional ToManipulateProfessional = new Professional();

        private string imageString;

        private byte[] curriculumBytes;

        private bool _isFirstAdmin;

        public Editar_User()
        {
            this.InitializeComponent();
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

            curriculumBytes = ToManipulateProfessional.CurriculumP;
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

            if (ToManipulateProfessional.IdPfs1 == 2) profession_cbx.SelectedIndex = 1;

            if (ToManipulateProfessional.IdPfs1 == 3) profession_cbx.SelectedIndex = 2;

            departamento.SelectedIndex = (ToManipulateProfessional.IdDp1 == null ? 0 : (int)ToManipulateProfessional.IdDp1 - 1);
            Phone_nb.Text = ToManipulateProfessional.PhoneP;

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

        }

        private async void btnExaminar_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                imageString = await (await PickFileHelper.PickImage()).ToBase64StringAsync();
                ProfilePicture_pp.ProfilePicture = await imageString.FromBase64String();

            }
            catch
            {
                // Message dialog
                var dialog = new MessageDialog("Error while picking image.");
                await dialog.ShowAsync();
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

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            bool ChangeThePassword = false;

            if (ToManipulateProfessional != null)
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
                if (!FieldsChecker.OnlyInts(SeguroSocial.Text))
                {
                    var dialog = new MessageDialog("The Isss must be valid.");
                    await dialog.ShowAsync();
                    return;
                }


                if (string.IsNullOrEmpty(ToManipulateProfessional.PasswordP) && !string.IsNullOrEmpty(passwordBox.Password))
                {

                    if (!FieldsChecker.CheckPassword(passwordBox.Password))
                    {
                        var dialog = new MessageDialog("The password must be valid.");
                        await dialog.ShowAsync();
                        return;
                    }
                    ChangeThePassword = true;
                }

                try
                {


                    ToManipulateProfessional.IdP = ToManipulateProfessional.IdP;
                    ToManipulateProfessional.NameP = FirstName1_tbx.Text;
                    ToManipulateProfessional.EmailP = Email.Text;
                    ToManipulateProfessional.Afpp = Afp.Text;
                    ToManipulateProfessional.Isssp = SeguroSocial.Text;
                    ToManipulateProfessional.Duip = Dui.Text;
                    ToManipulateProfessional.DateBirthP = (DateTimeOffset)Nacimiento.Date;
                    ToManipulateProfessional.SalaryP = int.Parse(Salario.Text);
                    ToManipulateProfessional.ActiveP = true;
                    ToManipulateProfessional.SexP = Sexo.SelectedValue == "Male" ? true : false;
                    ToManipulateProfessional.CurriculumP = curriculumBytes;
                    ToManipulateProfessional.PhoneP = Phone_nb.Text;
                    ToManipulateProfessional.PictureP = imageString;
                    ToManipulateProfessional.IdDp1 = (int)departamento.SelectedIndex + 1;
                    ToManipulateProfessional.IdPfs1 = (int)profession_cbx.SelectedIndex + 1;
                    ToManipulateProfessional.ZipCodeP = CodigoPostal.Text;
                    ToManipulateProfessional.HiringDateP = (DateTimeOffset)FechadeIngreso.Date;
                    ToManipulateProfessional.PasswordP = ToManipulateProfessional.PasswordP;

                    if (passwordBox.Password.Length > 0) ToManipulateProfessional.PasswordP = passwordBox.Password;

                    await APIConnection.GetConnection.PutProfessionalAsync(ToManipulateProfessional.IdP, ToManipulateProfessional);


                    if (ChangeThePassword)
                        await APIConnection.GetConnection.ChangePasswordAdminsAsync(ToManipulateProfessional.EmailP, passwordBox.Password);
                    var dialog = new MessageDialog("The Professional has been updated successfully");
                    await dialog.ShowAsync();
                }
                catch (ProFindServicesException ex)
                {
                    if (ex.StatusCode == 201 || ex.StatusCode == 200 || ex.StatusCode == 204)
                    {
                        // Success message dialog
                        var dialog = new MessageDialog("The Professional has been updated successfully");
                        await dialog.ShowAsync();
                    }
                    else
                    {
                        // Error message dialog
                        var dialog = new MessageDialog("There was a problem updating the professional, pleasy try again later.");
                        //"There was a problem updating the professional, pleasy try again later."
                        await dialog.ShowAsync();
                    }
                }
                catch
                {
                    {
                        // Error message dialog
                        var dialog = new MessageDialog("There was a problem updating the professional, pleasy try again later.");
                        //"There was a problem updating the professional, pleasy try again later."
                        await dialog.ShowAsync();
                    }
                }
                finally
                {
                    // Back to profesionals list
                    new InAppNavigationController().NavigateTo(typeof(Lib.AdminNS.Views.CRUDPages.ProfessionalNS.ListPage.ReadPage));
                }
            }


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

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (ToManipulateProfessional != null)
            {
                try
                {
                    await APIConnection.GetConnection.DeleteProfessionalAsync(ToManipulateProfessional.IdP);

                    // Success message dialog
                    var dialog = new MessageDialog("The Professional has been deleted successfully");
                    await dialog.ShowAsync();
                }
                catch (ProFindServicesException ex)
                {
                    if (ex.StatusCode == 201 || ex.StatusCode == 200)
                    {
                        // Success message dialog
                        var dialog = new MessageDialog("The Professional has been deleted successfully");
                        await dialog.ShowAsync();
                    }
                    else
                    {
                        // Error message dialog
                        var dialog = new MessageDialog("There was a problem deleting the professional, pleasy try again later.");
                        await dialog.ShowAsync();
                    }
                }
                finally
                {
                    // Back to profesionals list
                    new InAppNavigationController().NavigateTo(typeof(Lib.AdminNS.Views.CRUDPages.ProfessionalNS.ListPage.ReadPage));
                }
            }
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


    }
}