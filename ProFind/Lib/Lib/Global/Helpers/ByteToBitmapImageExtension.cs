using Org.BouncyCastle.Bcpg;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Security.Credentials;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace ProFind.Lib.Global.Helpers
{
    public static class   ByteToBitmapImage
    {
        public static BitmapImage ToBitmapImage(this byte[] bytes)
        {
            if (bytes == null)
                return null;
            BitmapImage image = new BitmapImage();
            using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
            {
                stream.AsStreamForWrite().Write(bytes, 0, bytes.Length);
                image.SetSource(stream);
            }
            return image;
        }

        public static WriteableBitmap ToWriteableBitmap(this BitmapImage img)
        {
            if (img == null) return null;
            WriteableBitmap wb = new WriteableBitmap(img.PixelWidth, img.PixelHeight);

            using (Stream stream = wb.PixelBuffer.AsStream())
            {
                if (stream.CanWrite)
                {
                    // Start at the beginning
                    stream.Position = 0;

                    for (int row = 0; row < (uint)img.PixelHeight; row++)
                    {
                        for (int col = 0; col < (uint)img.PixelWidth; col++)
                        {
                            stream.WriteByte(0x00);  // Red
                            stream.WriteByte(0xFF);  // Green
                            stream.WriteByte(0x00);  // Blue
                            stream.WriteByte(0xFF);  // Alpha
                        }
                    }

                    stream.Flush();
                }
            }

            return wb;
        }
    }
}
