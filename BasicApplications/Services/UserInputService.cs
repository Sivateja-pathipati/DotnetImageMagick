using BasicApplications.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace BasicApplications.Services
{
    internal class UserInputService
    {

        public static string GetSingleFilePath(HashSet<string> validExtensionTypes, string prompt = "Go back to Outer page")
        {
            string imagePath = String.Empty;
            while (true)
            {
                Console.WriteLine("Please Enter a valid File Path");
                imagePath = Console.ReadLine();
                imagePath = imagePath.Trim(' ').Trim('"');
                if (String.IsNullOrEmpty(imagePath) 
                    || !InputValidationUtilities.IsValidInputPath(imagePath,validExtensionTypes))
                {
                    Console.WriteLine("Invalid Path");
                    int choice = ConsoleUtilities.OptionsGenerator(new string[] { "Do you want to re-enter the path" }, prompt);
                    if (choice != 1)
                    {
                        return String.Empty;
                    }
                    continue;
                }
                return imagePath;

            }
        }


        public static List<string> GetMultipleFilePaths(int totalImages,HashSet<string> validExtensionTypes)
        {
            List<string> imagePaths = new List<string>();
            if (totalImages <= 0)
                return imagePaths;
            for (int i = 0; i<totalImages; i++)
            {
                Console.Clear();
                Console.WriteLine($"{totalImages-i} more image(s) still need to be added");
                Console.WriteLine("List of Images Added: ");
                ConsoleUtilities.PrintAll(imagePaths);

                Console.WriteLine($"\nCurrently you are adding Image no {i+1}");
                string imagePath = GetSingleFilePath(validExtensionTypes,"Skip adding this image file");
                if (String.IsNullOrEmpty(imagePath))
                {
                    Console.WriteLine("You have not entered a valid Image Path or you skipped adding this image try");
                    continue;
                }
                imagePaths.Add(imagePath);
            }


            return imagePaths;

        }

        public static int GetIntegerInput(string prompt = "Please enter a Valid Number")
        {
            Console.WriteLine(prompt);
            int result = 0;
            string resultString = String.Empty;
            try
            {
                resultString = Console.ReadLine();
                result = Convert.ToInt32(resultString);
                Console.WriteLine($"You have entered {result}");
                return result;

            }
            catch
            {
                Console.WriteLine($"You have entered Incorrect Input {resultString}");
                return result;

            }
        }

        public static string GetDirectory(string directoryPath = "",string prompt = "Go back to Outer Page")
        {
            while (true)
            {
                if (String.IsNullOrEmpty(directoryPath))

                {
                    Console.WriteLine("Please Enter a valid directory Path");
                    directoryPath = Console.ReadLine();
                    directoryPath = directoryPath.Trim(' ').Trim('"');
                }
                if (String.IsNullOrEmpty(directoryPath) || !Directory.Exists(directoryPath))
                {
                    Console.WriteLine($"Invalid Path {directoryPath}");
                    directoryPath = String.Empty;
                    int choice = ConsoleUtilities.OptionsGenerator(new string[] { "Do you want to re-enter the path" }, prompt);
                    if (choice != 1)
                    {
                        break;
                    }

                    continue;
                }
                return directoryPath;

            }
            return String.Empty;

        }

        public static string GetFileName(string fileName ="",string prompt = "Go back to the Outer Page")
        {
            while (true)
            {
                if (String.IsNullOrEmpty(fileName))
                {
                    Console.WriteLine("Please enter a valid file Name");
                    fileName = Console.ReadLine();
                }
                if (!InputValidationUtilities.IsValidFileName(fileName))
                {
                    int choice = ConsoleUtilities.OptionsGenerator(new string[] { "Re-enter the fileName" }, prompt);
                    if (choice != 1)
                    {
                        return string.Empty;
                    }
                    fileName = string.Empty;
                    continue;
                }
                return fileName;
            }
        }

        public static string GetCompleteOutputFilePath(HashSet<string> validExtensionTypes, string outputPath = "", string prompt = "Go back to Outer Page")
        {
            while (true)
            {
                int choice = 0;
                Console.Clear();
                if (String.IsNullOrEmpty(outputPath))
                {
                    Console.WriteLine("Please enter a valid Output Path");
                    outputPath = Console.ReadLine() ?? String.Empty;
                }
                outputPath = outputPath.Trim(' ').Trim('"');
                if (!InputValidationUtilities.IsValidOutputPath(outputPath,validExtensionTypes, out bool fileAlreadyExists))
                {
                    Console.WriteLine("Invalid Path");
                    choice = ConsoleUtilities.OptionsGenerator(new string[] { "Do you want to re-enter the path" }, prompt);
                    if (choice != 1) 
                        return String.Empty;
                    else
                    {
                        outputPath = String.Empty;
                        continue;
                    }
                }
                if (fileAlreadyExists)
                {
                    Console.WriteLine("The File Already Exists");
                    choice = ConsoleUtilities.OptionsGenerator(new string[] { "Replace Existing file", "re-enter the path" },prompt);
                    if (choice == 1)
                    {
                        return outputPath;
                    }
                    else if (choice == 2)
                    {
                        outputPath = String.Empty;
                        continue;
                    }
                    else
                    {
                        return String.Empty;
                    }
                }
                return outputPath;
            }
        }

        public static string PromptForDefaultOrCustomOutputPath(string inputPath,string extension, HashSet<string> validExtensionTypes, bool isDirectory)
        {
            string defaultPath = FileUtilities.CreateDefaultFileName(inputPath, extension,isDirectory);
            Console.WriteLine($"The Default generated Output Path is {defaultPath}");
            int choice = ConsoleUtilities.OptionsGenerator(new string[] { "Proceed with Default Path", "Enter new OutputPath" });

            if (choice == 1)
            {
                return UserInputService.GetCompleteOutputFilePath(validExtensionTypes, defaultPath);


            }
            else if (choice == 2)
            {
                return UserInputService.GetCompleteOutputFilePath(validExtensionTypes);

            }

            return String.Empty;
        }


        public static string PromptForDefaultOrCustomDirectoryPath(string inputPath)
        {
            string defaultPath = FileUtilities.CreateDefaultDirectory(inputPath);
            Console.WriteLine($"The Default generated Output Path is {defaultPath}");
            int choice = ConsoleUtilities.OptionsGenerator(new string[] { "Proceed with default path", "Enter new directoryPath" });
            if (choice == 1)
            {
                return UserInputService.GetDirectory(defaultPath);
            }
            else if (choice == 2)
            {
                return UserInputService.GetDirectory();
            }
            return String.Empty;

        }

        
    }
}
