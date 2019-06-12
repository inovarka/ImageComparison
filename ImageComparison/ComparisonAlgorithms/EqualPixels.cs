using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.IO;

namespace ImageComparison.ComparisonAlgorithms
{
    class EqualPixels
    {
        private static Bitmap BitmapImageToBitmap(BitmapImage bitmapImage)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                Bitmap bitmap = new Bitmap(outStream);

                return new Bitmap(bitmap);
            }

        }

        public static List<bool> GetHash(BitmapImage bitmapImage)
        {
            Bitmap bmpSource = BitmapImageToBitmap(bitmapImage);
            List<bool> listResult = new List<bool>();
            //creating new image 16x16 pixels
            Bitmap bmpMin = new Bitmap(bmpSource, new Size(16, 16));

            for (int i = 0; i < bmpMin.Height; i++)
            {
                for (int j = 0; j < bmpMin.Width; j++)
                {
                    listResult.Add(bmpMin.GetPixel(i, j).GetBrightness() < 0.5f);
                }
            }
            return listResult;


        }
    }
}
