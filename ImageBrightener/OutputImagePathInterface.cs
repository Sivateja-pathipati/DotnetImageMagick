using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageBrightener
{
    internal class OutputImagePathInterface
    {
        public static string MainOutputImagePathPage(string inputImagePath)
        {
            Console.Clear();
            while (true)
            {
                string outputImagePath = GetValidatedOutputImagePath(inputImagePath);
                if (outputImagePath == string.Empty)
                {
                    int choice = CommonInterface.OptionsGenerator(new string[] { "Re enter the Output Image Path" },
                        "Go back to Main Page (Note the current Image Process will be terminated)");
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
        public static string GetValidatedOutputImagePath(string inputFile)
        {
            var directory = Path.GetDirectoryName(inputFile);
            var fileName = Path.GetFileName(inputFile);

            if (directory == null)
            {
                Console.WriteLine("Invalid Path!");
                return String.Empty;
            }
            string fileOutput = Path.Combine(directory, "Out_" + fileName);

            Console.WriteLine($"\nWhere do you want to save the processed image \nThe default generated path is {fileOutput}\n");
            int userChoice = CommonInterface.OptionsGenerator(new string[] { "Default output path", "New Output Path" },
                "Go back to the Main Output Page");
            if (userChoice == 1)
            {
                Console.Clear();
                Console.WriteLine("The default Path is selected " + fileOutput);
                if (!IsCorrectOutputImagePath(fileOutput))
                {
                    return string.Empty;
                }
                if (File.Exists(fileOutput))
                {
                    Console.WriteLine("File Name already exists! ");
                    int choice = CommonInterface.OptionsGenerator(new string[] { "Proceed with Current name"},
                        "Go back to MainOutputPage and enter new Name");
                    if (choice == 1)
                    {
                        return fileOutput;
                    }
                    else 
                        Console.Clear();
                        return string.Empty;
                    }
                
            }
            else if (userChoice == 2)
            {
                Console.Clear();
                while (true)
                {
                    Console.WriteLine("Please Enter the Full output Path");
                    fileOutput = Console.ReadLine();
                    fileOutput = fileOutput.Trim('"');
                    if (!IsCorrectOutputImagePath(fileOutput))
                    {
                        return string.Empty;
                    }
                    if (File.Exists(fileOutput))
                    {
                        Console.WriteLine("File Name already exists! ");
                        int choice = CommonInterface.OptionsGenerator(new string[] { "Proceed with Current name",
                    "Re Enter New Name again" },
                            "Go back to MainOutputPage");
                        if (choice == 1)
                        {
                            return fileOutput;
                        }
                        else if (choice == 2)
                        {
                            Console.Clear();
                            continue;
                        }
                        else
                        {
                            return string.Empty;
                        }

                    }
                    return fileOutput;

                }

            }
            else if (userChoice == 0)
            {
                Console.Clear();
                Console.WriteLine("Exiting to the Main Output Page");
                return string.Empty;
            }

          
            return fileOutput;

        }

        public static bool IsCorrectOutputImagePath(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return false;

            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            if (extension is not (".jpg" or ".jpeg" or ".png"))
            {
                Console.WriteLine("Extension must be .jpg, .jpeg, or .png");
                return false;
            }


            // OUTPUT validation
            var directory = Path.GetDirectoryName(filePath);
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
