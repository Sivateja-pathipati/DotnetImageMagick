using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicApplications.Constants
{
    public static class FileExtensionTypes
    {
        public static readonly HashSet<string> Image = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".jpg",
            ".jpeg",
            ".png"
        };

        public static readonly HashSet<string> Text = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".txt"
        };

        public static readonly HashSet<string> Pdf = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".pdf"

        };

    }
    public  enum ImageExtension
    {
        Jpg,
        Jpeg,
        Png,
        bmp,
        Gif,
        Webp
    }
}
