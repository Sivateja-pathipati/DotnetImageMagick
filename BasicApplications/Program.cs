using BasicApplications.Constants;
using BasicApplications.Services;
using BasicApplications.Utilities;
using ImageMagick;
using System.Reflection;

namespace BasicApplications
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //string inputPath = UserInputService.GetSingleFilePath(FileExtensionTypes.Image);
            //string outputPath = FileUtilities.CreateDefaultFileName(inputPath, ".jpg", false, "Output_");
            //using MagickImage image = new MagickImage(inputPath);
            //MagickImage image2 = ImageTransformationService.GenerateThumbnail(image);
            //image2.Write(outputPath);




            string inputPath = UserInputService.GetSingleFilePath(FileExtensionTypes.Image);
            //var filterTypes = Enum.GetValues(typeof(FilterType));
            for (int angle = 0; angle <= 360; angle += 30)
            {
                string outputPath = FileUtilities.CreateDefaultFileName(inputPath, ".jpg", false, $"Output_{angle}_");

                using MagickImage image = new MagickImage(inputPath);
                //image.FilterType = (FilterType)filter;
                // Resize so filter effect is visible
                //image.Resize(300, 300);
                MagickImage image2 = ImageTransformationService.AddWaterMark(image, "ajetavis");
                image2.Write(outputPath);

            }


            //string directoryPath = UserInputService.GetDirectory();



            //List<string> files = DirectoryUtilites.GetAllFilesFromDirectory(directoryPath,FileExtensionTypes.Image);

            //foreach (string inputPath in files)
            //{
            //    string outputPath = FileUtilities.CreateDefaultFileName(inputPath, ".jpg", false, "Output_");
            //    using MagickImage image = new MagickImage(inputPath);
            //    MagickImage image2 = ImageAdjustmentService.CompressImage(image);
            //    image2.Write(outputPath);

            //}
        }
    }
}
