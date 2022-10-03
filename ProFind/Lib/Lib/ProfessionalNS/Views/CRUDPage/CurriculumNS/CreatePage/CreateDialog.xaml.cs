using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ProFind.Lib.Global.Helpers;
using Syncfusion.Pdf;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.ProfessionalNS.Views.CRUDPage.CurriculumNS.CreatePage
{
    public sealed partial class CreateDialog : ContentDialog
    {
        const string SideText_CREATEMODE = "Use our uploader to save your resume and reach thousands of potential Clients.";
        const string SideHeader_CREATEMODE = "Save your resume";
        const string SideText_UPDATEMODE = "Update your information and provide the most recent data to your Clients.";
        const string SideHeader_UPDATEMODE = "Update your resume";

        private byte[] newCurriculum;

        public CreateDialog()
        {
            this.InitializeComponent();
            CreateMode();
        }

        public CreateDialog(PdfDocument document)
        {
            UpdateMode();

           // PDFPreview.LoadDocument(document);
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.Hide();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.Hide();
        }

        private async void AnimateButtons()
        {
        }

        private void PDFPreviewMode()
        {
        }

        private async void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            var pickedFile = await PickFileHelper.PickPDF();

            if (pickedFile != null) 
            {

                UploadResume_btn.IsChecked = true;
                UploadResume_btn.Content = "Loaded file";
            }

            PDFPreview.LoadDocument(pickedFile.ToPdfLoadedDocument());
        }


        private void CreateMode()
        {
            SideHeader_txt.Text = SideHeader_CREATEMODE;
            SideText_txt.Text = SideText_CREATEMODE;
        }

        private void UpdateMode()
        {
            SideHeader_txt.Text = SideHeader_UPDATEMODE;
            SideText_txt.Text = SideText_UPDATEMODE;

            
        }

    }
}
