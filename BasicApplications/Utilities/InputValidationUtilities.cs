using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicApplications.Utilities
{
    internal class InputValidationUtilities
    {

        public static bool IsValidInputPath(string inputPath, HashSet<string> validExtensionTypes)
        {
            if (string.IsNullOrWhiteSpace(inputPath))
                return false;
            if (!File.Exists(inputPath))
            {
                Console.WriteLine("The file doesn't exist in the given input Path");
                return false;
            }
            if (!FileUtilities.SampleReader(inputPath, validExtensionTypes)) { return false; }

            return true;
        }

        public static bool IsValidDirectory(string directoryPath)
        {
            if (string.IsNullOrWhiteSpace(directoryPath)) { return false; }

            if (!Directory.Exists(directoryPath)) { return false; }
            if (!InputValidationUtilities.CanWriteToDirectory(directoryPath)) { return false; }



            return true;
        }

        public static bool IsValidFileName(string fileName)
        {
            if (String.IsNullOrWhiteSpace(fileName)) { return false; }
            if (fileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0) { return false; }
            if (fileName.Length >= 255) { return false; }
            if (fileName.EndsWith(' ') || fileName.EndsWith(' ')) { return false; }
            return true;
        }

        public static bool IsValidOutputPath(string outputPath, HashSet<string> extensionTypes, out bool fileAlreadyExists)
        {
            
            fileAlreadyExists = false;
            if (String.IsNullOrWhiteSpace(outputPath)) { return false; }

            string directory = Path.GetDirectoryName(outputPath) ?? String.Empty;
            if (!IsValidDirectory(directory)) { return false; }

            if (!extensionTypes.Contains(Path.GetExtension(outputPath)))
            {
                Console.WriteLine($"Your file path extension {Path.GetExtension(outputPath)} is not of type ({String.Join(" or ",extensionTypes)})");
                return false;
            }

            fileAlreadyExists = File.Exists(outputPath);

            return true;
        }

        public static bool CanWriteToDirectory(string directoryPath)
        {
            try
            {
                string tempFile = Path.Combine(directoryPath, Path.GetRandomFileName());
                using (File.Create(tempFile)) { }
                File.Delete(tempFile);
                return true;
            }
            catch
            {
                Console.WriteLine("Creating a File in the Directory failed");
                return false;
            }
        }

    }
}
