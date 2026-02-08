using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicApplications.Constants;

namespace BasicApplications.Utilities
{
    public static class ExtensionHelpers
    {
        public static string GetImageExtension(this ImageExtension ext)
        {
            return ext switch
            {
                ImageExtension.Jpg => ".jpg",
                ImageExtension.Jpeg => ".jpeg",
                ImageExtension.Png => ".png",
                ImageExtension.Gif => ".gif",
                ImageExtension.Webp => ".webp",
                ImageExtension.bmp => ".bmp",
                _ => throw new ArgumentOutOfRangeException(nameof(ext), ext, null)
            };
        }
    }
}
