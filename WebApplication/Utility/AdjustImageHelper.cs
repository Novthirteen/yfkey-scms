using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace com.Sconit.Utility
{
    public static class AdjustImageHelper
    {
        public static void AdjustImage(int thumbnailSize,string fileFullPath, Stream fileContent)
        {
            Bitmap originalBMP = new Bitmap(fileContent);

            int newWidth, newHeight;
            if (originalBMP.Width > originalBMP.Height)
            {
                newWidth = thumbnailSize;
                newHeight = originalBMP.Height * thumbnailSize / originalBMP.Width;
            }
            else
            {
                newWidth = originalBMP.Width * thumbnailSize / originalBMP.Height;
                newHeight = thumbnailSize;
            }
            Bitmap newBMP = new Bitmap(originalBMP, newWidth, newHeight);
            Graphics oGraphics = Graphics.FromImage(newBMP);

            oGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            oGraphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            oGraphics.DrawImage(originalBMP, 0, 0, newWidth, newHeight);

            newBMP.Save(fileFullPath, System.Drawing.Imaging.ImageFormat.Jpeg);

            originalBMP.Dispose();
            newBMP.Dispose();
            oGraphics.Dispose();
        }
    }
}
