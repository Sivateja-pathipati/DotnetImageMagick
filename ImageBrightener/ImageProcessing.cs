using ImageMagick;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageBrightener
{
    internal class ImageProcessing
    {
        public static void ImageBrightener(string inputFilePath, string outputFilePath)
        {
            using var img = new MagickImage(inputFilePath);
            img.AutoOrient();
            double mean = MeasureBrightness(img);
            if (mean > 0.9)
            {
                Console.WriteLine("The mean is greater than 0.9\n " +
                    "The Image is already bright No Brightening is required");
                return;
            }
            img.Sharpen();
            img.Normalize();
            img.ColorSpace = ColorSpace.Gray;

            using var illum = img.Clone();

            // Measure brightness of region


            double scale = Math.Min(1.0, 400.0 / Math.Min(img.Width, img.Height));
            illum.Resize(
                (uint)(img.Width * scale),
                (uint)(img.Height * scale)
            );



            int radius = (int)Math.Max(10, Math.Min(illum.Width, illum.Height) / 12);
            illum.GaussianBlur(0, radius);

            // Safer than Normalize for documents
            illum.Level(new Percentage(10), new Percentage(98));

            illum.Resize(img.Width, img.Height);

            img.Composite(illum, CompositeOperator.DivideSrc);
            img.Clamp();

            double gamma = ComputeGamma(mean, 0.5);
            img.GammaCorrect(gamma);
            img.Density = new Density(300);
            img.Write(outputFilePath);

        }

        static double ComputeGamma(double currentMean, double targetMean)
        {
            if (currentMean <= 0 || currentMean >= 1)
                return 1.0;

            double gamma =
                Math.Log(targetMean) /
                Math.Log(currentMean);
            if (double.IsNaN(gamma) || double.IsInfinity(gamma))
                return 1.0;
            return Math.Clamp(gamma, 0.6, 1);
        }

        static double MeasureBrightness(MagickImage image)
        {
            using var gray = image.Clone();
            gray.ColorSpace = ColorSpace.Gray;

            var stats = gray.Statistics();
            var grayStats = stats.GetChannel(PixelChannel.Gray);
            return grayStats.Mean / Quantum.Max;
        }
    }
}
