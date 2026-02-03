using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextToImageConverter
{
    internal class OutputDirectoryPathUserInterface
    {
        public static string MainOutputImagePathPage(string inputImagePath)
        {
            Console.Clear();
            while (true)
            {
                string outputImagePath = GetValidatedOutputDirectoryPath(inputImagePath);
                if (outputImagePath == string.Empty)
                {
                    int choice = CommonInterface.OptionsGenerator(new string[] { "Re enter the Output Directory Path" },
                        "Go back to Main Page ");
                    if (choice == 1)
                    {
                        Console.Clear();
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    return outputImagePath;
                }

            }
            return string.Empty;
        }
        public static string GetValidatedOutputDirectoryPath(string inputFile)
        {
            var directory = Path.GetDirectoryName(inputFile);
            var fileName = Path.GetFileNameWithoutExtension(inputFile);

            if (directory == null)
            {
                Console.WriteLine("Invalid Path!");
                return String.Empty;
            }
            string imageOutputDirectory = Path.Combine(directory, fileName +"_Images" );

            Console.WriteLine($"\nWhere do you want to save the processed images \nThe default generated Directory is {imageOutputDirectory}\n");
            int userChoice = CommonInterface.OptionsGenerator(new string[] { "Default Directory", "Existing Directory" },
                "Go back to the Main Output Page");
            if (userChoice == 1)
            {
                Console.Clear();
                Console.WriteLine("The default Directory is selected " + imageOutputDirectory);
                if (Directory.Exists(imageOutputDirectory))
                {
                    Console.WriteLine("Directory already exists so there is a chance of overwrite Do you want to proceed");
                    int userChoice2 = CommonInterface.OptionsGenerator(new string[] { "Yes" }, "Exiting to the Outer Page");
                    if (userChoice2 != 1)
                    {
                        return string.Empty;
                    }
                }
                try
                {
                    Directory.CreateDirectory(imageOutputDirectory);
                }
                catch
                {
                    Console.WriteLine("Creating Directory Falied Navigating to Outer page" );
                    return String.Empty;
                }
                if (!IsCorrectDirectoryPath(imageOutputDirectory))
                {
                    return string.Empty;
                }

            }
            else if (userChoice == 2)
            {
                Console.Clear();
                while (true)
                {
                    Console.WriteLine("Please Enter the Existing Full Directory Path");
                    imageOutputDirectory = Console.ReadLine();
                    imageOutputDirectory = imageOutputDirectory.Trim('"');
                    if (!IsCorrectDirectoryPath(imageOutputDirectory))
                    {
                        return string.Empty;
                    }
                    return imageOutputDirectory;

                }

            }
            else if (userChoice == 0)
            {
                Console.Clear();
                Console.WriteLine("Exiting to the Main Output Page");
                return string.Empty;
            }


            return imageOutputDirectory;

        }

        public static bool IsCorrectDirectoryPath(string directory)
        {

            if (string.IsNullOrEmpty(directory))
                return false;

            if (!Directory.Exists(directory))
                return false;

            // Optional: test write permission
            try
            {
                string testFile = Path.Combine(directory, Path.GetRandomFileName());
                using (File.Create(testFile)) { }
                File.Delete(testFile);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
