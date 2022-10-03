using Windows.UI.Xaml.Controls;
using ProFind.Lib.Global.Services;
using Professional = ProFind.Lib.Global.Services.Professional;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.ProfessionalNS.Views.CRUDPage.ProjectNS.CreatePage
{
    public sealed partial class SelectProfessionalDialog : ContentDialog
    {
        public static Professional SelectedProfessional { get; set; }
        public SelectProfessionalDialog()
        {
            this.InitializeComponent();
        }

        private  async void LoadData()
        {
         await APIConnection.GetConnection.GetProfessionalsAsync();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ProfessionalsListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            SelectedProfessional = (Professional)e.ClickedItem;
        }
    }
}
