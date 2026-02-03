using ImageMagick;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageBrightener
{
    internal class InputImagePathInterface
    {
        public static string MainInputImagePathPage()
        {
            Console.Clear();
            while (true)
            {
                string inputImagePath = GetValidatedInputImagePath();
                if (inputImagePath == string.Empty)
                {
                    int choice = CommonInterface.OptionsGenerator(new string[] { "Re enter the Input Image Path" },
                        "Go back to Main Page");
                    if (choice == 1)
                    {
                        Console.Clear();
                        continue;
                    }
                    else
                    {
                        Console.Clear();
                        break;
                    }
                }
                else
                {
                    Console.Clear();
                    return inputImagePath;
                }

            }
            return string.Empty;
        }
        public static string GetValidatedInputImagePath()
        {

            Console.WriteLine("Please Enter the Image Source Path:");
            string? fileInput = Console.ReadLine();
            fileInput = fileInput?.Trim('"');

            if (string.IsNullOrWhiteSpace(fileInput))
            {
                Console.WriteLine("Invalid Path!");
                return String.Empty;
            }

            if (!IsCorrectInputImagePath(fileInput)) 
            {
                Console.WriteLine("Invalid Path!");
                return String.Empty;
            }
            return fileInput;



        }
       
        public static bool IsCorrectInputImagePath(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                Console.WriteLine($"The filepath string is Null or empty");
                return false;

            }

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"The file {filePath} doesn't Exists");
                return false;
            }
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            if (extension is not (".jpg" or ".jpeg" or ".png"))
            {
                Console.WriteLine("Extension is not .jpg or .jpeg or .png");
                return false;
            }
            try
            {
                using var img = new MagickImage(filePath);


                return true;
            }
            catch (MagickException)
            {
                Console.WriteLine("ImageMagick failed to read the image so provide the correct image");
                return false;
            }
        }


    }
}
