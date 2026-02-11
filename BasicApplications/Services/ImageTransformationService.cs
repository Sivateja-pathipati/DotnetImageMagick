using ImageMagick;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicApplications.Services
{
    internal class ImageTransformationService
    {

        public static MagickImage GenerateThumbnail(MagickImage image, int maxSize = 200)
        {
            MagickImage img = (MagickImage)image.Clone();
            img.AutoOrient();
            img.FilterType = FilterType.Lanczos;

            MagickGeometry geometry = new MagickGeometry((uint)maxSize, (uint)maxSize)
            {
                IgnoreAspectRatio = false,
                Greater = false
            };
            img.Resize(geometry);
            img.Extent((uint)maxSize, (uint)maxSize, Gravity.Center, MagickColors.White);
            img.Quality = 75;
            img.Strip();

            return img;
        }

        public static  MagickImage RotateImage(MagickImage image, double angle)
        {
            MagickImage img = (MagickImage)image.Clone();
            img.Rotate(angle);
            return img;
        }

        public static MagickImage CropImage(MagickImage image, double width, double height)
        {
            MagickImage img = (MagickImage)image.Clone();
            img.Crop(new MagickGeometry((Percentage)width, (Percentage)height));
            return img;
        }
    }
}
