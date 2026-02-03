using ImageMagick;
using System.Diagnostics;
namespace ImageBrightener
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello user for choices use integers 1,0 to navigate");
            while (true)
            {

                int inputChoice = CommonInterface.OptionsGenerator(new string[] { "Lets start Image Brightening" });
                if (inputChoice == 1)
                {
                    string inputFilePath = InputImagePathInterface.MainInputImagePathPage();
                    if (string.IsNullOrEmpty(inputFilePath))
                    {
                        Console.Clear();
                        continue;
                    }
                    Console.Clear();
                    Console.WriteLine("The input File Path is " + inputFilePath);

                    string outputFilePath = OutputImagePathInterface.MainOutputImagePathPage(inputFilePath);
                    if (string.IsNullOrEmpty(outputFilePath))
                    {
                        Console.Clear();
                        continue;
                    }
                    Console.Clear();
                    Console.WriteLine("The input File Path is " + inputFilePath);
                    Console.WriteLine("The output File Path is " + outputFilePath);

                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    ImageProcessing.ImageBrightener(inputFilePath, outputFilePath);
                    sw.Stop();
                    Console.WriteLine($"Total Time taken is : {sw.Elapsed.TotalSeconds}");

                }
                else if (inputChoice == 0)
                {
                    Console.WriteLine("Thanks for using the application");
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect Command! Exiting the application");
                    break;
                }
            }

        }
       
    }
}
