using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;
namespace TextToImageConverter
{
    internal class TextToImageProcessor
    {
        public static void CreatingImagesFromText(string inputFilePath, string outputDirectory)
        {
            string text = string.Empty;
            using (var reader = new StreamReader(inputFilePath)) {text = reader.ReadToEnd();}
            const int PageWidth = 2480;
            const int PageHeight = 3508;
            const int Dpi = 300;
            const int Margin = 150;

            var tallSettings = new MagickReadSettings
            {
                Width = PageWidth - (Margin * 2),
                Height = 36000, // tall canvas worth 10 pages
                Density = new Density(Dpi),
                BackgroundColor = MagickColors.White,
                FillColor = MagickColors.Black,
                Font = "DejaVu-Sans",
                FontPointsize = 12,
                TextGravity = Gravity.Northwest
            };

            using var tallImage = new MagickImage();
            tallImage.Read("caption:" + text, tallSettings);
            tallImage.Trim();
            tallImage.ResetPage(); // resets virtual canvas
            int usableHeight = PageHeight - (Margin * 2);
            int y = 0;
            var pages = new MagickImageCollection();
            while (y < tallImage.Height)
            {
                //Console.WriteLine($"Current y is {y}");
                int preferredCut = y + usableHeight;
                if (preferredCut >= tallImage.Height)
                    preferredCut = (int)tallImage.Height;
                //Console.WriteLine($"Current preferredCut is {preferredCut}");


                int safeCut = FindSafeCut(tallImage, preferredCut);
                //Console.WriteLine($"Current safeCut is {safeCut}");


                int sliceHeight = safeCut - y;
                if (sliceHeight <= 0)
                    sliceHeight = usableHeight;

                var page = tallImage.CloneArea(new MagickGeometry(
                    0, y,
                    tallImage.Width,
                    (uint)sliceHeight
                ));

                // Pad to full page
                page.Extent(PageWidth, PageHeight, Gravity.Center, MagickColors.White);

                // Fax-safe cleanup
                page.ColorSpace = ColorSpace.Gray;
                page.Strip();
                page.Quality = 95;

                pages.Add(page);
                y += sliceHeight;
            }
            if (pages.Count > 1 && IsArtifactPage(pages.Last()))
            {
                pages.RemoveAt(pages.Count - 1);
            }
            Console.WriteLine("Total Number of Images Created " + pages.Count);
            int count = 1;
            foreach (var img in pages)
            {
                img.Write(Path.Combine(outputDirectory,$"img{count++}.jpg"));
            }
        }


        public static bool IsBlankRow(MagickImage img, int y)
        {
            var pixels = img.GetPixels();

            for (int x = 0; x < img.Width; x++)
            {
                var pixel = pixels.GetPixel(x, y);

                // For grayscale images, R == G == B
                if (pixel.ToColor().R < 250)
                    return false;
            }

            return true;
        }

        public static int FindSafeCut(MagickImage img, int preferredY, int scanUp = 50)
        {
            int start = (int)Math.Min(preferredY, img.Height - 1);
            int end = Math.Max(0, start - scanUp);

            for (int y = start; y >= end; y--)
            {
                if (IsBlankRow(img, y))
                    return y;
            }

            return preferredY; // fallback
        }

        static bool IsArtifactPage(IMagickImage<ushort> img)
        {
            var pixels = img.GetPixels();
            int darkCount = 0;

            for (int y = 0; y < img.Height; y += 5)
            {
                for (int x = 0; x < img.Width; x += 5)
                {
                    if (pixels.GetPixel(x, y).ToColor().R < 240)
                        darkCount++;
                }
            }

            return darkCount < 50; // threshold tweakable
        }
    }
}
