namespace ID3Lib
{
    using System;
    using System.Drawing;
    using System.IO;

    public class ImageLoadedEventArgs : EventArgs
    {
        private System.Drawing.Image image;
        private string imageName;

        public ImageLoadedEventArgs(string imageName, System.Drawing.Image image)
        {
            this.imageName = imageName;
            this.image = image;
        }

        public ImageLoadedEventArgs(string imageName, Stream stream)
        {
            this.ImageName = imageName;
            this.image = System.Drawing.Image.FromStream(stream);
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

        public string ImageName
        {
            get
            {
                return this.imageName;
            }
            set
            {
                this.imageName = value;
            }
        }
    }
}

