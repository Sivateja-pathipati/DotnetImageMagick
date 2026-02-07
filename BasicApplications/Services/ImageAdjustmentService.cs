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
            image.Modulate((Percentage)(brightness+100));
            return image;
        }

        public static MagickImage AdjustContrast(MagickImage image, int contrast)
        {
            image.BrightnessContrast((Percentage)0, (Percentage)contrast);
            
            return image;
        }

        public static MagickImage AdjustSaturation(MagickImage image, int saturation)
        {
            image.Modulate((Percentage)100,(Percentage)(saturation+100));

            return image;
        }

        public static MagickImage AdjustHue(MagickImage image, int hue)
        {
            image.Modulate((Percentage)100, (Percentage)100, (Percentage)(hue + 100));
            return image;
        }
    }
}
