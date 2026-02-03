using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextToImageConverter
{
    internal class InputTextPathUserInterface
    {
        public static string MainInputImagePathPage()
        {
            Console.Clear();
            while (true)
            {
                string inputImagePath = GetValidatedTextFilePath();
                if (inputImagePath == string.Empty)
                {
                    int choice = CommonInterface.OptionsGenerator(new string[] { "Re enter the text file path" },
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
        public static string GetValidatedTextFilePath()
        {

            Console.WriteLine("Please Enter the Text file Path:");
            string? fileInput = Console.ReadLine();
            fileInput = fileInput?.Trim('"');

            if (string.IsNullOrWhiteSpace(fileInput))
            {
                Console.WriteLine("Invalid Path!");
                return String.Empty;
            }

            if (!IsValidTextFile(fileInput))
            {
                Console.WriteLine("Invalid Path!");
                return String.Empty;
            }
            return fileInput;
        }

        public static bool IsValidTextFile(string filePath)
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
            if (Path.GetExtension(filePath).ToLowerInvariant() != ".txt")
            {
                Console.WriteLine("The file extension is not .txt.");
                return false;
            }
            try
            {
                using var reader = new StreamReader(filePath);
                reader.ReadLine();
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("StreamReader failed to read the text");
                return false;
            }
        }


    }
}

