namespace ID3Lib
{
    using System;
    using System.Drawing;
    using System.IO;

    public class ImageEmbeddedEventArgs : EventArgs
    {
        private string description;
        private System.Drawing.Image image;
        private string imageFileName;
        private string mimeType;
        private ID3PictureType pictureType;
        private string trackFileName;

        public ImageEmbeddedEventArgs(string trackFileName, string imageFileName, System.Drawing.Image image, ID3PictureType pictureType, string mimeType, string description)
        {
            this.trackFileName = trackFileName;
            this.imageFileName = imageFileName;
            this.image = image;
            this.pictureType = pictureType;
            this.mimeType = mimeType;
            this.description = description;
        }

        public ImageEmbeddedEventArgs(string trackFileName, string imageFileName, Stream stream, ID3PictureType pictureType, string mimeType, string description)
        {
            this.trackFileName = trackFileName;
            this.imageFileName = imageFileName;
            this.image = System.Drawing.Image.FromStream(stream);
            this.pictureType = pictureType;
            this.mimeType = mimeType;
            this.description = description;
        }

        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }

        public System.Drawing.Image Image
        {
            get
            {
                return this.image;
            }
            set
            {
                this.image = value;
            }
        }

        public string ImageFileName
        {
            get
            {
                return this.imageFileName;
            }
            set
            {
                this.imageFileName = value;
            }
        }

        public string MimeType
        {
            get
            {
                return this.mimeType;
            }
            set
            {
                this.mimeType = value;
            }
        }

        public ID3PictureType PictureType
        {
            get
            {
                return this.pictureType;
            }
            set
            {
                this.pictureType = value;
            }
        }

        public string TrackFileName
        {
            get
            {
                return this.trackFileName;
            }
            set
            {
                this.trackFileName = value;
            }
        }
    }
}

