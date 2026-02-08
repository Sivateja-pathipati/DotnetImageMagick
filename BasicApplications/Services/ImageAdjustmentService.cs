using BasicApplications.Utilities;
using ImageMagick;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicApplications.Services
{
    internal class ImageAdjustmentService
    {
        public static MagickImage AdjustBrightness(MagickImage image, int brightness)
        {
            MagickImage img = (MagickImage)image.Clone();
            img.Modulate((Percentage)(brightness+100));
            return img;
        }

        public static MagickImage AdjustContrast(MagickImage image, int contrast)
        {
            MagickImage img = (MagickImage)image.Clone();
            img.BrightnessContrast((Percentage)0, (Percentage)contrast);
            
            return img;
        }

        public static MagickImage AdjustSaturation(MagickImage image, int saturation)
        {
            MagickImage img = (MagickImage) image.Clone();
            img.Modulate((Percentage)100,(Percentage)(saturation+100));

            return img;
        }

        public static MagickImage AdjustHue(MagickImage image, int hue)
        {
            MagickImage img = (MagickImage)image.Clone();
            img.Modulate((Percentage)100, (Percentage)100, (Percentage)(hue + 100));
            return img;
        }

        public static MagickImage Resize(MagickImage image, uint width, uint height)
        {
            MagickImage img = (MagickImage)(image.Clone());
            img.Resize(width, height);
            return img;
        }

        public static MagickImage CompressImage(MagickImage image, long targetSize = 500*1024)
        {
            MagickImage img = (MagickImage)image.Clone();

            uint quality = img.Quality;
            byte[] imageBytes = img.ToByteArray();
            long length = imageBytes.Length;
            while (length > targetSize)
            {

                Console.WriteLine($"Current Quality of Image is {quality} and updating quality to {quality-5}");
                quality = quality - 5;
                if (quality <= 6)
                {
                    Console.WriteLine("quality has degraded to less than 6");
                    break;
                }
                img.Quality = quality;
                imageBytes = img.ToByteArray();
                length = imageBytes.Length;
                Console.WriteLine($"Current Image Size is {length/1024}kb");
            }


            return img;

        }
    }
}
