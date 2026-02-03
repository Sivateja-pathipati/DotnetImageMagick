namespace TextToImageConverter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                int userChoice = CommonInterface.OptionsGenerator(new string[] { "Start the Conversion Process" });
                if (userChoice != 1)
                    break;
                string inputPath = InputTextPathUserInterface.MainInputImagePathPage();

                if (inputPath == String.Empty)
                {
                    Console.WriteLine("File Reading Failed");
                    int choice = CommonInterface.OptionsGenerator(new string[] { "Do you want to enter the path again" });
                    if (choice == 1)
                    {
                        continue;
                    }
                    else if (choice == 2)
                    {
                        Console.WriteLine("Thanks for using the application");
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                string outputDirectory = OutputDirectoryPathUserInterface.MainOutputImagePathPage(inputPath);
                if (outputDirectory == String.Empty)
                {
                    Console.WriteLine("Directory Reading Failed");
                    int choice = CommonInterface.OptionsGenerator(new string[] { "Do you want to enter the Repeat the process" });
                    if (choice == 1)
                    {
                        continue;
                    }
                    else if (choice == 2)
                    {
                        Console.WriteLine("Thanks for using the application");
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                try
                {
                    Console.Clear();
                    Console.WriteLine("Started the Text to Image Conversion Process");
                    TextToImageProcessor.CreatingImagesFromText(inputPath, outputDirectory);
                    Console.WriteLine("Successfully Completed the Conversion Process");
                    int choice = CommonInterface.OptionsGenerator(new string[] { "Do you want to process the next text file" });
                    if (choice == 1)
                        continue;
                    else
                        break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Creating Images failed {ex}");
                    continue;
                }
            }

        }
    }
}
