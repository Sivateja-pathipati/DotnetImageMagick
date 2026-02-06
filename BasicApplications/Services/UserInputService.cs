using BasicApplications.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicApplications.Services
{
    internal class UserInputService
    {

        public static string GetSingleImagePath(string prompt = "Go back to Outer page")
        {
            string imagePath = String.Empty;
            while (true)
            {
                Console.WriteLine("Please Enter a valid Image Path");
                imagePath = Console.ReadLine();
                imagePath = imagePath.Trim(' ').Trim('"');
                bool fileValidation = true;
                if (String.IsNullOrEmpty(imagePath) || !CommonUtilities.IsValidInputPath(imagePath, CommonUtilities.validImageExtensionTypes))
                {
                    Console.WriteLine("Invalid Path");
                    int choice = CommonUtilities.OptionsGenerator(new string[] { "Do you want to re-enter the path" }, prompt);
                    if (choice != 1)
                    {
                        break;
                    }
                    continue;
                }
                return imagePath;

            }
            return String.Empty;
        }


        public static List<string> GetMultipleImagePaths(int totalImages)
        {
            List<string> imagePaths = new List<string>();
            if (totalImages <= 0)
                return imagePaths;
            for (int i = 0; i<totalImages; i++)
            {
                Console.Clear();
                Console.WriteLine($"{totalImages-i} more image(s) still need to be added");
                Console.WriteLine("List of Images Added: ");
                CommonUtilities.PrintAll(imagePaths);

                Console.WriteLine($"\nCurrently you are adding Image no {i+1}");
                string imagePath = GetSingleImagePath("Skip adding this image file");
                if (String.IsNullOrEmpty(imagePath))
                {
                    Console.WriteLine("You have not entered a valid Image Path or you skipped adding this image try");
                    continue;
                }
                imagePaths.Add(imagePath);
            }


            return imagePaths;

        }

        public static int GetIntegerInput(string prompt)
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

        
    }
}
