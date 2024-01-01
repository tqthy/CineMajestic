using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CineMajestic.ViewModels
{
    public class ImageVsSQL
    {
        //chuyển Bitmap sang mảng Byte
        public static byte[] BitmapImageToByteArray(BitmapImage bitmapImage)
        {
            byte[] data;
            PngBitmapEncoder encoder = new PngBitmapEncoder(); // Sử dụng PNG encoder
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));

            using (MemoryStream stream = new MemoryStream())
            {
                encoder.Save(stream);
                data = stream.ToArray();
            }

            return data;
        }


        /// Phương thức chuyển đổi ByteArray thành BitmapImage
        public static BitmapImage ByteArrayToBitmapImage(byte[] byteArray)
        {
            using (var stream = new MemoryStream(byteArray))
            {
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
                bitmapImage.Freeze(); // Đóng băng hình ảnh để sử dụng trên UI
                return bitmapImage;
            }
        }
    }
}
