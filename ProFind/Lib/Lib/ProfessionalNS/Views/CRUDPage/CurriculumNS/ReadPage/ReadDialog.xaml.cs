using Windows.UI.Xaml.Controls;
using ProFind.Lib.Global.Services;
using Syncfusion.Pdf;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.ProfessionalNS.Views.CRUDPage.CurriculumNS.ReadPage
{
    public sealed partial class ReadDialog : ContentDialog
    {
        public ReadDialog(PdfDocument document)
        {
            

            //PDFPreview.Load(document);
        }


        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            const string projectIdSelected = "1";
            const string Titulo = "Carlitos";


            this.Hide();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
