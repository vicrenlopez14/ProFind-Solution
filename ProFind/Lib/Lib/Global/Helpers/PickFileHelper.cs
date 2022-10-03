using Syncfusion.Pdf.Parsing;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;

namespace ProFind.Lib.Global.Helpers
{
    public static class PickFileHelper
    {
        public static async Task<StorageFile> PickImage()
        {
            /* var picker = new Windows.Storage.Pickers.FileOpenPicker
             {
                 ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail,
                 SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary
             };
             picker.FileTypeFilter.Add(".jpg");
             picker.FileTypeFilter.Add(".jpeg");
             picker.FileTypeFilter.Add(".png");

             var file = await picker.PickSingleFileAsync();
             return file;*/

            //Opens a file picker.
            var picker = new FileOpenPicker();
            picker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            picker.ViewMode = PickerViewMode.List;
            //Filters PDF files in the documents library.
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            var file = await picker.PickSingleFileAsync();
            if (file == null)
            {
                // Content dialog
                await new MessageDialog("No image has been loaded").ShowAsync();
                return null;
            }

            return file;
        }

        public static async Task<byte[]> PickImageBytes()
        {
            var file = await PickImage();
            if (file == null) return null;
            var stream = await file.OpenStreamForReadAsync();
            var bytes = new byte[stream.Length];
            await stream.ReadAsync(bytes, 0, (int)stream.Length);
            return bytes;
        }

        public static async Task<byte[]> PickPDF()
        {
            //Opens a file picker.
            var picker = new FileOpenPicker();
            picker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            picker.ViewMode = PickerViewMode.List;
            //Filters PDF files in the documents library.
            picker.FileTypeFilter.Add(".pdf");
            var file = await picker.PickSingleFileAsync();
            if (file == null)
            {
                // Content dialog
                await new MessageDialog("No PDF has been loaded").ShowAsync();
                return null;
            }


            return await file.ToByteArrayAsync();
        }

        public static PdfLoadedDocument ToPdfLoadedDocument(this byte[] file)
        {
            if (file == null)
            {
                // Content dialog
                new MessageDialog("No PDF has been loaded").ShowAsync();
                return null;
            }

            return new PdfLoadedDocument(file);
        }
    }
}