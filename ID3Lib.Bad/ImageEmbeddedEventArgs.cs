using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

namespace ID3Lib
{
    public class ImageEmbeddedEventArgs : EventArgs
    {
        private string trackFileName;
        private string imageFileName;
        private Image image = null;
        private ID3PictureType pictureType = null;
        private string mimeType = "";
        private string description = "";


        public ImageEmbeddedEventArgs(
string trackFileName,
 string imageFileName,
 Image image,
 ID3PictureType pictureType,
 string mimeType,
 string description)
        {
            this.trackFileName = trackFileName;
            this.imageFileName = imageFileName;
            this.image = image;
            this.pictureType = pictureType;
            this.mimeType = mimeType;
            this.description = description;
        }

        public ImageEmbeddedEventArgs(
string trackFileName,
 string imageFileName,
 Stream stream,
 ID3PictureType pictureType,
 string mimeType,
 string description)
        {
            this.trackFileName = trackFileName;
            this.imageFileName = imageFileName;
            if (stream != null)
            {
                this.image = Image.FromStream(stream);
                this.pictureType = pictureType;
                this.mimeType = mimeType;
                this.description = description;
            }
        }

        public string ImageFileName
        {
            get { return imageFileName; }
            set { imageFileName = value; }
        }


        public Image Image
        {
            get { return image; }
            set { image = value; }
        }



        public string TrackFileName
        {
            get { return trackFileName; }
            set { trackFileName = value; }
        }


        public ID3PictureType PictureType
        {
            get { return pictureType; }
            set { pictureType = value; }
        }


        public string MimeType
        {
            get { return mimeType; }
            set { mimeType = value; }
        }


        public string Description
        {
            get { return description; }
            set { description = value; }
        }
    }
}
