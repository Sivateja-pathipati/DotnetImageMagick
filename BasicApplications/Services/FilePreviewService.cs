using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicApplications.Services
{
    internal class FilePreviewService
    {
        public static void PreviewFileWithDefaultApp(string inputPath)
        {

            var process = Process.Start(new ProcessStartInfo
            {
                FileName = inputPath,
                UseShellExecute = true,
            });
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            try
            {
                process.Kill();
            }
            catch { }
        }

        public static void PreviewFileWithRequestedApp(string applicationName, string FileName)
        {
            var process = new Process();
            process.StartInfo = new ProcessStartInfo
            {
                FileName = $"{applicationName}",
                Arguments = $"\"{FileName}\"",
                UseShellExecute = true

            };
            process.Start();
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            if (!process.HasExited)
            {
                process.Kill();
            }
        }
    }
}
