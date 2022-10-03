using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.Global.Helpers;
using ProFind.Lib.Global.Services;
using Syncfusion.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;


namespace ProFind.Lib.AdminNS.Views.CRUDPages.ProposalNS.CreatePage
{
    public sealed partial class CreatePage : Page
    {
        private string selectedPicture;
        private Proposal ToSendProposal;
        private Professional SelectedProfessional;

        public CreatePage()
        {
            this.InitializeComponent();
            AddEvents();
        }

        private void AddEvents()
        {
            Title_tb.OnEnterNextField();
            Description_tb.OnEnterNextField();
        }

        private void RestartVars()
        {
            ToSendProposal = null;
            SelectedProfessional = null;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Restart vars
            RestartVars();

            try
            {
                this.SelectedProfessional = (Professional)e.Parameter;
            }
            catch
            {
                SelectedProfessional = new Professional();
            }
            finally
            {
                LoadPageDataAsync();
            }
        }

        private async Task LoadPageDataAsync()
        {
            ProfessionalImage_img.Source = await SelectedProfessional.PictureP.FromBase64String();
            ProfessionalProfession_tb.Text = SelectedProfessional.IdPfs1Navigation.NamePfs;
            ProfessionalDepartment_tb.Text = SelectedProfessional.IdDp1Navigation.NameDp;
            ProfessionalHiringDate_tb.Text = SelectedProfessional.HiringDateP.ToString();
        }

        private async void ViewCurriculum_btn_Click(object sender, RoutedEventArgs e)
        {
            await new CurriculumNS.ReadPage.ReadDialog(SelectedProfessional.CurriculumP.ToPdfLoadedDocument()).ShowAsync();
        }

        private async void Create_btn_Click(object sender, RoutedEventArgs e)
        {
            ToSendProposal = new Proposal
            {
                PicturePp = selectedPicture,
                TitlePp = Title_tb.Text,
                DescriptionPp = Description_tb.Text,
                SuggestedStart = SuggestedBegin_dp.ToDateTime(),
                SuggestedEnd = SuggestedEnd_dp.ToDateTime(),
                IdP3 = SelectedProfessional.IdP,
            };

            try
            {
                await APIConnection.GetConnection.PostProposalAsync();
            }
            catch (ProFindServicesException ex)
            {
                if (ex.StatusCode == 200 || ex.StatusCode == 201)
                {
                    var contentDialog = new ContentDialog
                    {
                        Title = "Proposal sent!",
                        Content = "The professional you selected will review the proposal, then you'll receive a response.",
                        PrimaryButtonText = "Ok",
                    };

                    await contentDialog.ShowAsync();

                    new InAppNavigationController().NavigateTo(typeof(Lib.ClientNS.Views.CRUDPages.ProposalsNS.ListPage.ListPAge));
                }
            }

        }

        private async void Cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            new InAppNavigationController().GoBack();
        }
    }
}
