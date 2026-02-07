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
    internal class ConvertingImageFormatService
    {
        public static void UserPrompt()
        {
            string inputPath = String.Empty;
            // Getting User Input
            inputPath = UserInputService.GetSingleFilePath(FileExtensionTypes.Image,"Exit the Service");
            if (String.IsNullOrWhiteSpace(inputPath))
            {
                return;
            }

            // Getting the File Format
            Console.Clear();
            Console.WriteLine(inputPath);
            Console.WriteLine("Please Select the Format You want");
            var values = Enum.GetValues(typeof(ImageExtension)).Cast<ImageExtension>().Select(e => e.GetImageExtension()).ToArray();
            int choice = ConsoleUtilities.OptionsGenerator(values, "Go back ");
            if (choice == 0)
            {
                return;
            }

            // Saving in the Output Path
            string extension = values[choice - 1];
            Console.WriteLine($"You have choosen {values[choice-1]}");

            // Getting Directory  to save in the outputPath
            string directoryPath = UserInputService.PromptForDefaultOrCustomDirectoryPath(inputPath);
            if ( String.IsNullOrEmpty(directoryPath))
            {
                return;
            }
            string outputPath = String.Empty;

            while (true)
            { 
                string fileName = UserInputService.GetFileName();
                if (String.IsNullOrEmpty(fileName))
                {
                    return;
                }
                string backPrompt = "Exit the service";
                outputPath= Path.Combine(directoryPath, fileName + extension);
                if (!InputValidationUtilities.IsValidOutputPath(outputPath, new HashSet<string> { extension }, out bool fileAlreadyExists))
                {
                    Console.WriteLine("Invalid Path");
                    choice = ConsoleUtilities.OptionsGenerator(new string[] { "Do you want to re-enter file Name" }, backPrompt);
                    if (choice != 1)
                        return ;
                    else
                    {
                        continue;
                    }
                }
                if (fileAlreadyExists)
                {
                    Console.WriteLine("The File Already Exists");
                    choice = ConsoleUtilities.OptionsGenerator(new string[] { "Replace Existing file", "re-enter the path" }, backPrompt);
                    if (choice == 1)
                    {
                        break;
                    }
                    if (choice == 2)
                    {
                        outputPath = String.Empty;
                        continue;
                    }
                    else
                    {
                        return;
                    }
                }
                break;
            }

            Console.WriteLine($"Output path is {outputPath}");
            try
            {
                Console.WriteLine("Starting Conversion Process");
                ConvertFormat(inputPath, outputPath);
                Console.WriteLine("Ending Conversion Process");

            }
            catch(Exception ex) 
            {
                Console.WriteLine($"An Exception occured while converting {ex}");
            }

        }

        public static void ConvertFormat(string inputPath, string outputPath)
        {
            using MagickImage img = new MagickImage(inputPath);
            img.Write(outputPath);
        }
    }
}
