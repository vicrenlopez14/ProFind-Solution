using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.Global.Helpers;
using ProFind.Lib.Global.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.Global.Views.ProfessionalsCatalog
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProfessionalsCatalog : Page
    {
        List<Department> Deparments;
        List<Professional> CandidateProfessionalsColumn1;
        List<Professional> CandidateProfessionalsColumn2;
        Profession SelectedProfession;
        Professional SelectedProfessional;

        bool ReadyToMakeProposal = false;

        public ProfessionalsCatalog()
        {
            this.InitializeComponent();
        }

        #region Search

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            LoadDeparments();
            RestartVars();
        }

        private async Task LoadDeparments()
        {
            {
                Deparments = await APIConnection.GetConnection.GetDepartmentsAsync() as List<Department>;
                DepartmentSelection_cb.ItemsSource = Deparments;
            }
        }

        private async Task LoadCandidates() => await LoadCandidates(null);

        private async Task LoadCandidates(string Name) => await LoadCandidates(Name, null);

        private async Task LoadCandidates(string Name, Department department)
        {
            // Retrieve data
            var CandidateProfessionals = await APIConnection.GetConnection.FilterProfessionalsAsync(professionId: SelectedProfession.IdPfs.ToString(), name: Name, departmentId: department.IdDp.ToString()) as List<Professional>;

            // Split data
            CandidateProfessionalsColumn1 = CandidateProfessionals.Take((CandidateProfessionals.Count + 1) / 2).ToList();
            CandidateProfessionalsColumn2 = CandidateProfessionals.Skip((CandidateProfessionals.Count + 1) / 2).ToList();

            // Show data
            ProfessionalsListViewCol1.ItemsSource = CandidateProfessionalsColumn1;
            ProfessionalsListViewCol2.ItemsSource = CandidateProfessionalsColumn2;
        }


        private void RestartVars()
        {
            {
                StepsFlipView_fv.SelectedIndex = 0;
                SelectedProfession = null;
                SelectedProfessional = null;
                ReadyToMakeProposal = false;
            }
        }

        private void UpdateControlStates()
        {
            SelectedProfessionSection_sp.Visibility = SelectedProfession == null ? Visibility.Visible : Visibility.Collapsed;
            SelectedProfessionalSection_sp.Visibility = SelectedProfessional == null ? Visibility.Visible : Visibility.Collapsed;
            ReadyToMakeProposal = SelectedProfession != null && SelectedProfessional != null;
            MakeAProposal_btn.IsEnabled = ReadyToMakeProposal;
        }

        private async void ProfessionsListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            SelectedProfession = e.ClickedItem as Profession;
            await LoadCandidates();
            StepsFlipView_fv.SelectedIndex++;
            UpdateControlStates();
        }

        private async void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {

        }

        private async void Control2_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            // Since selecting an item will also change the text,
            // only listen to changes caused by user entering text.
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                await LoadCandidates(sender.Text, DepartmentSelection_cb.SelectedItem as Department);
            }

        }

        private async void DepartmentSelection_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await LoadCandidates(SearchBox_asb.Text, DepartmentSelection_cb.SelectedItem as Department);
        }

        #endregion

        #region Selection
        private void ProfessionalsListViewCol1_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedItem = e.ClickedItem as Professional;

            if (SelectedProfessional == clickedItem)
            {
                SelectedProfessional = null;
            }
            else
            {
                SelectedProfessional = clickedItem;
            }

            UpdateControlStates();
        }


        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RestartVars();
            UpdateControlStates();
        }

        private async void ViewCurriculum_btn_Click(object sender, RoutedEventArgs e)
        {
            await new Lib.AdminNS.Views.CRUDPages.CurriculumNS.ReadPage.ReadDialog(SelectedProfessional.CurriculumP.ToPdfLoadedDocument()).ShowAsync();
        }
    }
}
