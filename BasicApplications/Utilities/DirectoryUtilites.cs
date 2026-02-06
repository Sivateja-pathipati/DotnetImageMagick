using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicApplications.Utilities
{
    internal class DirectoryUtilites
    {
        public static List<string> GetAllFilesFromDirectory(string directoryPath, HashSet<string> extensionTypes)
        {
            List<string> reqFiles = new List<string>();
            var allFiles = Directory.GetFiles(directoryPath);
            foreach (var file in allFiles)
            {
                if (extensionTypes.Contains(Path.GetExtension(file).ToLowerInvariant()))
                {
                    reqFiles.Add(file);
                }
            }
            return reqFiles;
        }


    }
}
