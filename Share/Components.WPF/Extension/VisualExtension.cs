using MyNet.Components.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MyNet.Components.WPF.Extension
{
    public static class VisualExtension
    {
        public static void SaveToImage(this Visual visual, string filePath, int width = 100, int height = 100, bool openImg = false)
        {
            if (filePath.IsEmpty() || visual == null)
            {
                return;
            }

            var bitmap = RenderVisaulToBitmap(visual, width, height);

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                GenerateImage(bitmap, ImageFormat.Png, fs);
            }
            if (openImg)
            {
                Process.Start(filePath);
            }
        }

        public static RenderTargetBitmap RenderVisaulToBitmap(Visual visual, int width, int height)
        {
            //TODO
            //??96
            var rtb = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Default);
            rtb.Render(visual);

            return rtb;
        }

        public static void GenerateImage(BitmapSource bitmap, ImageFormat format, Stream destStream)
        {
            BitmapEncoder encoder = BitmapEncoder.Create(format.Guid);
            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            encoder.Save(destStream);
        }
    }
}
