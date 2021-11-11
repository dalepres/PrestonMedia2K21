using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Imaging;

namespace ID3Lib
{
    public class ImageFrame : V2Frame
    {
        public static ImageFormat GetImageFormatFromMimeType(string mimeType)
        {
            switch (mimeType)
            {
                case "image/jpeg":
                    return ImageFormat.Jpeg;
                case "image/bmp":
                    return ImageFormat.Bmp;
                case "image/gif":
                    return ImageFormat.Gif;
                case "image/tiff":
                    return ImageFormat.Tiff;
                case "image/png":
                    return ImageFormat.Png;
                case "image/x-icon":
                    return ImageFormat.Icon;
                default:
                    return ImageFormat.Jpeg;
            }
        }

        public static string GetImageFileExtensionFromMimeType(string mimeType)
        {
            switch (mimeType)
            {
                case "image/jpeg":
                    return "jpg";
                case "image/bmp":
                    return "bmp";
                case "image/gif":
                    return "gif";
                case "image/tiff":
                    return "tif";
                case "image/png":
                    return "png";
                case "image/x-icon":
                    return "ico";
                default:
                    return "jpg";
            }
        }
    }
}
