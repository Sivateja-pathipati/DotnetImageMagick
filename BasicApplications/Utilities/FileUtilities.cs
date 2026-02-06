using ImageMagick;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicApplications.Utilities
{
    internal class FileUtilities
    {
        public static bool SampleReader(string inputPath, HashSet<string> validExtensionTypes)
        {

            string extension = Path.GetExtension(inputPath).ToLowerInvariant();
            if (!validExtensionTypes.Contains(extension))
            {
                Console.WriteLine($"Your file path Extension {extension} is not of type  ({String.Join(" or ", validExtensionTypes)})");
                return false;

            }
            if (extension == ".txt")
            {
                return CanReadTextFile(inputPath);
            }
            else if (extension is (".jpg" or ".jpeg" or ".png"))
            {
                return CanReadImage(inputPath);
            }
            return false;
        }

        public static bool CanReadTextFile(string inputPath)
        {
            try
            {
                using var reader = new StreamReader(inputPath);
                reader.ReadLine();
                return true;
            }
            catch
            {
                Console.WriteLine("Unable to read the file using stream reader");
                return false;
            }
        }

        public static bool CanReadImage(string inputPath)
        {
            try
            {
                using var image = new MagickImage(inputPath);
                return true;
            }
            catch
            {
                Console.WriteLine("Unable to read the file using Image Magick");
                return false;
            }
        }
        public static string CreateDefaultFileName(string path, string extension, bool isDirectory = false, string appendWord = "Output")
        {
            string directory;
            string fileName;
            if (isDirectory)
            {
                directory = path;
                fileName = appendWord;
            }
            else
            {
                directory = Path.GetDirectoryName(path) ?? String.Empty;
                fileName = Path.GetFileNameWithoutExtension(path);
            }
            string defaultFile = Path.Combine(directory, fileName + extension);
            return defaultFile;
        }
    }
}
