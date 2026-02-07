using BasicApplications.Constants;
using BasicApplications.Utilities;
using ImageMagick;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicApplications.Services
{
    internal class CreatingPDFService
    {
        public static void UserPrompt()
        {
            Console.WriteLine("Lets Start Creating a Pdf");
            List<string> multipleImagePaths = new List<string>(); 
            int choice = ConsoleUtilities.OptionsGenerator(new string[] 
                        { 
                            "Provide a directory - all images will be converted into a single pdf",
                            "Provide individual image paths"                
                           
                        }, "Exit The Service");
            if (choice == 1)
            {
                string directory = UserInputService.GetDirectory();
                multipleImagePaths = DirectoryUtilites.GetAllFilesFromDirectory(directory, FileExtensionTypes.Image);

            }
            else if (choice == 2)
            {
                int totalImages = UserInputService.GetIntegerInput("Enter Number of Images Required");
                multipleImagePaths = UserInputService.GetMultipleFilePaths(totalImages,FileExtensionTypes.Image);
            }
            else
            {
                return;
            }

            string outputPath = UserInputService.PromptForDefaultOrCustomOutputPath(multipleImagePaths[0], ".pdf",
                                                            FileExtensionTypes.Pdf, false);
            try
            {
                Console.WriteLine("Started the Conversion Process");
                CreatingPdfFromImages(multipleImagePaths, outputPath);
                Console.WriteLine("Successfully Converted the images");

            }
            catch (Exception ex) 
            {
                Console.WriteLine("Error occured while converting the images");
                Console.WriteLine(ex.ToString()); 
            }

        }
        public static void CreatingPdfFromImages(List<string> imagePaths, string outputPath)
        {
            var images = new MagickImageCollection();
            foreach (var  image in imagePaths)
            {
                images.Add(image);
            }
            images.Write(outputPath);
        }

    }
}
