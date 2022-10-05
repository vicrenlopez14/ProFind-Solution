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
        byte[] pdfBytes;
        PdfLoadedDocument document;
        public ReadDialog(byte[] documentBytes)
        {
            this.pdfBytes = documentBytes;
        }

        
        private static async void ShowNullAlert()
        {
            // Message dialog

        }

        private async void ContentDialog_PrimaryButtonClick_1(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick_1(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
           
        }

        private void ContentDialog_Loaded_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (document == null)
            {
                Hide();
                return;
            }

            try
            {
                document = new PdfLoadedDocument(pdfBytes);
                PDFPreview.LoadDocument(document);
            }
            catch (Exception ex)
            {
                Hide();
                return;
            }
        }
    }
}
