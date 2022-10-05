using Windows.UI.Xaml.Controls;
using ProFind.Lib.Global.Services;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using Windows.UI.Popups;
using System;
using System.Reflection.Metadata;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.CurriculumNS.ReadPage
{
    public sealed partial class ReadDialog : ContentDialog
        
    {
        PdfLoadedDocument document;
        public ReadDialog(PdfLoadedDocument document)
        {
            this.document = document;

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

        private void ContentDialog_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (document == null)
            {
                Hide();
                return;
            }

            try
            {
                PDFPreviewControl.LoadDocument(document);
            }
            catch (Exception ex)
            {
                Hide();
                return;
            }
        }
    }
}
