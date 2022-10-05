using Windows.UI.Xaml.Controls;
using ProFind.Lib.Global.Services;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using Windows.UI.Popups;
using System;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.CurriculumNS.ReadPage
{
    public sealed partial class ReadDialog : ContentDialog
    {
        public ReadDialog(PdfLoadedDocument document)
        {
            if (document == null)
            {
                Hide();
                return;
            }

            PDFPreviewControl.LoadDocument(document);
        }

        private static async void ShowNullAlert()
        {
            // Message dialog
          
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
