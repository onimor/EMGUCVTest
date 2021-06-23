using Emgu.CV;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media.Imaging;

namespace EMGUCVTest
{
    public static class BitmapConverter
    {
        /// <param name="o"></param>
        /// <returns></returns>
        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr o);
        public static BitmapSource ToBitmapSource(this Mat image)
        {

            using Bitmap source = image.ToBitmap();
            IntPtr ptr = source.GetHbitmap();

            BitmapSource bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                ptr,
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            DeleteObject(ptr);
            bs.Freeze();
            return bs;
        }
    }
}
