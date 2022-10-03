using Nito.AsyncEx.Synchronous;
using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using static System.Net.WebRequestMethods;

namespace ProFind.Lib.Global.Helpers
{
    public static class StorageFileToByteArrayExtension
    {
        public static async Task<byte[]> ToByteArrayAsync(this StorageFile file)
        {
            if (file == null)
                return null;
            
            using (var inputStream = await file.OpenSequentialReadAsync())
            {
                var readStream = inputStream.AsStreamForRead();

                var byteArray = new byte[readStream.Length];
                await readStream.ReadAsync(byteArray, 0, byteArray.Length);
                return byteArray;
            }
        }

        public static async Task<ImageSource> FromBase64(string base64)
        {
            if (base64 == null)
            {
                return null;
            }


            // read stream
            var bytes = Convert.FromBase64String(base64);
            var image = bytes.AsBuffer().AsStream().AsRandomAccessStream();
            // decode image
            var decoder = await BitmapDecoder.CreateAsync(image);
            image.Seek(0);

            var output = new WriteableBitmap((int)decoder.PixelWidth, (int)decoder.PixelHeight);
            await output.SetSourceAsync(image);
            return output;
        }

        public static async Task<ImageSource> FromBase64String(this string base64)
        {
            if (base64 == null)
            {
                return null;
            }

            return await FromBase64(base64);
        }

        public static ImageSource FromBase64Sync(string base64)
        {
            if (base64 == null)
            {
                return null;
            }


            var ims = new InMemoryRandomAccessStream();
            var bytes = Convert.FromBase64String(base64);
            var dataWriter = new DataWriter(ims);
            dataWriter.WriteBytes(bytes);
            dataWriter.StoreAsync();
            ims.Seek(0);
            var img = new BitmapImage();
            img.SetSource(ims);
            return img;
        }

        public static ImageSource FromBase64StringSync(this string base64)
        {
            if (base64 == null)
            {
                return null;
            }


            return FromBase64Sync(base64);
        }

        public static async Task<ImageSource> FromStorageFile(this StorageFile file)
        {
            if (file == null)
            {
                return null;
            }
            
            return await FromBase64(await file.ToBase64StringAsync());
        }

        public static async Task<string> ToBase64StringAsync(this StorageFile bitmap)
        {
            if (bitmap == null)
            {
                return "";
            }
            
            var stream = await bitmap.OpenAsync(Windows.Storage.FileAccessMode.Read);
            var decoder = await BitmapDecoder.CreateAsync(stream);
            var pixels = await decoder.GetPixelDataAsync();
            var bytes = pixels.DetachPixelData();
            return await ToBase64(bytes, (uint)decoder.PixelWidth, (uint)decoder.PixelHeight, decoder.DpiX,
                decoder.DpiY);
        }

        private static async Task<string> ToBase64(byte[] image, uint height = 0, uint width = 0, double xpiX = 96,
            double dpiY = 96)
        {
            if (image == null)
            {
                return null;
            }


            var encoded = new InMemoryRandomAccessStream();
            var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, encoded);
            encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight, height, width, dpiY, dpiY, image);
            await encoder.FlushAsync();
            encoded.Seek(0);

            // read bytes
            var bytes = new byte[encoded.Size];
            await encoded.AsStream().ReadAsync(bytes, 0, bytes.Length);

            return Convert.ToBase64String(bytes);
        }
    }
}