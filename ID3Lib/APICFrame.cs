namespace ID3Lib
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Text;

    public class APICFrame : ImageFrame
    {
        private string description;
        private string mimeType;
        private MemoryStream pictureData;
        private byte pictureType;

        public APICFrame()
        {
            this.mimeType = string.Empty;
            this.description = string.Empty;
            this.pictureType = 0;
            this.pictureData = null;
            base.FrameId = "APIC";
        }

        public APICFrame(byte[] frameValue)
        {
            this.mimeType = string.Empty;
            this.description = string.Empty;
            this.pictureType = 0;
            this.pictureData = null;
            base.FrameId = "APIC";
            this.ParseValue(frameValue);
            base.FrameValue = frameValue;
        }

        public APICFrame(string description, byte pictureType, Image picture)
        {
            this.mimeType = string.Empty;
            this.description = string.Empty;
            this.pictureType = 0;
            this.pictureData = null;
            base.FrameId = "APIC";
            this.mimeType = this.GetMimeTypeForImage(picture);
            this.description = description;
            this.pictureType = pictureType;
            this.pictureData = new MemoryStream();
            picture.Save(this.pictureData, picture.RawFormat);
        }

        private int CalculateFrameLength()
        {
            byte b = base.GetEncodingByte();
            int frameLength = 0;
            frameLength += this.mimeType.Length + 3;
            switch (b)
            {
                case 0:
                case 3:
                    frameLength += this.description.Length + 1;
                    break;

                case 1:
                    frameLength += this.description.Length + 4;
                    break;

                case 2:
                    frameLength += this.description.Length + 2;
                    break;
            }
            if (this.pictureData != null)
            {
                frameLength += (int) this.pictureData.Length;
            }
            return frameLength;
        }

        public override bool Equals(object obj)
        {
            APICFrame frame = obj as APICFrame;
            if (frame == null)
            {
                return false;
            }
            return ((frame.PictureType == this.pictureType) && (string.Compare(frame.Description, this.description, true) == 0));
        }

        private byte[] Get16BitStringBytes(byte[] frameValue, ref int pointer)
        {
            List<byte> bytes = new List<byte>();
            do
            {
                byte b1 = frameValue[pointer++];
                byte b2 = frameValue[pointer++];
                if ((b1 == 0) && (b2 == 0))
                {
                    return bytes.ToArray();
                }
                if (((b1 != 0xff) || (b2 != 0xfe)) || (base.GetEncodingByte() != 1))
                {
                    bytes.Add(b1);
                    bytes.Add(b2);
                }
            }
            while (pointer < (frameValue.Length - 1));
            return bytes.ToArray();
        }

        private byte[] Get8BitStringBytes(byte[] frameValue, ref int pointer)
        {
            byte b;
            List<byte> bytes = new List<byte>();
            while ((pointer < frameValue.Length) && ((b = frameValue[pointer++]) != 0))
            {
                bytes.Add(b);
            }
            return bytes.ToArray();
        }

        public override int GetHashCode()
        {
            return (this.description + this.pictureType.ToString()).GetHashCode();
        }

        private string GetMimeTypeForImage(Image img)
        {
            if (img.RawFormat.Equals(ImageFormat.Jpeg))
            {
                return "image/jpeg";
            }
            if (img.RawFormat.Equals(ImageFormat.Png))
            {
                return "image/png";
            }
            if (img.RawFormat == ImageFormat.Gif)
            {
                return "image/gif";
            }
            if (img.RawFormat == ImageFormat.Bmp)
            {
                return "image/bmp";
            }
            return "image/";
        }

        public Image GetPicture()
        {
            return Image.FromStream(this.pictureData);
        }

        public Image GetPicture(Size size)
        {
            Image mg = this.GetPicture();
            double ratio = 0.0;
            double myThumbWidth = 0.0;
            double myThumbHeight = 0.0;
            int x = 0;
            int y = 0;
            if ((((double) mg.Width) / Convert.ToDouble(size.Width)) > (((double) mg.Height) / Convert.ToDouble(size.Height)))
            {
                ratio = Convert.ToDouble(mg.Width) / Convert.ToDouble(size.Width);
            }
            else
            {
                ratio = Convert.ToDouble(mg.Height) / Convert.ToDouble(size.Height);
            }
            myThumbHeight = Math.Ceiling((double) (((double) mg.Height) / ratio));
            myThumbWidth = Math.Ceiling((double) (((double) mg.Width) / ratio));
            Size thumbSize = new Size((int) myThumbWidth, (int) myThumbHeight);
            Bitmap bp = new Bitmap(size.Width, size.Height);
            x = (size.Width - thumbSize.Width) / 2;
            y = size.Height - thumbSize.Height;
            Graphics g = Graphics.FromImage(bp);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            Rectangle rect = new Rectangle(x, y, thumbSize.Width, thumbSize.Height);
            g.DrawImage(mg, rect, 0, 0, mg.Width, mg.Height, GraphicsUnit.Pixel);
            return bp;
        }

        internal bool IsMatch(byte pictureType, string description)
        {
            return (((pictureType == 1) || (pictureType == 2)) || ((this.pictureType == pictureType) && (string.Compare(this.description, description, true) == 0)));
        }

        internal bool IsMatch(byte pictureType)
        {
            return pictureType == 1 || pictureType == 2 || this.pictureType == pictureType;
        }

        private void LoadDescription(byte[] frameValue, ref int pointer)
        {
            byte[] bytes;
            switch (base.GetEncodingByte())
            {
                case 0:
                case 3:
                    bytes = this.Get8BitStringBytes(frameValue, ref pointer);
                    break;

                case 1:
                case 2:
                    bytes = this.Get16BitStringBytes(frameValue, ref pointer);
                    break;

                default:
                    throw new Exception("Cannot get picture description.  Frame Encoding is invalid.");
            }
            this.description = base.Encoding.GetString(bytes);
        }

        private void LoadPicDescriptionEncoding(byte[] frameValue, ref int pointer)
        {
            base.SetEncoding((EncodingTypes) frameValue[pointer++]);
        }

        private void LoadPicMimeType(byte[] frameValue, ref int pointer)
        {
            int start = pointer;
            while (frameValue[pointer++] != 0)
            {
            }
            int terminatedLength = pointer - start;
            if (terminatedLength > 1)
            {
                this.mimeType = Encoding.GetEncoding("iso-8859-1").GetString(frameValue, start, terminatedLength - 1);
            }
            else
            {
                this.mimeType = string.Empty;
            }
        }

        private void LoadPictureData(byte[] frameValue, ref int pointer)
        {
            this.pictureData = new MemoryStream();
            int length = frameValue.Length - pointer;
            this.pictureData.Write(frameValue, pointer, length);
        }

        private void LoadPictureType(byte[] frameValue, ref int pointer)
        {
            this.pictureType = frameValue[pointer++];
        }

        private void ParseValue(byte[] frameValue)
        {
            int pointer = 0;
            this.LoadPicDescriptionEncoding(frameValue, ref pointer);
            this.LoadPicMimeType(frameValue, ref pointer);
            this.LoadPictureType(frameValue, ref pointer);
            this.LoadDescription(frameValue, ref pointer);
            this.LoadPictureData(frameValue, ref pointer);
        }

        internal override void UpdateFrameValue()
        {
            MemoryStream ms = new MemoryStream();
            this.WriteValue(ms);
            base.FrameValue = ms.ToArray();
        }

        private void WriteDescription(MemoryStream ms)
        {
            byte b = base.GetEncodingByte();
            if (b == 1)
            {
                ms.Write(base.BOM, 0, 2);
            }
            ms.Write(base.Encoding.GetBytes(this.description), 0, base.Encoding.GetByteCount(this.description));
            switch (b)
            {
                case 0:
                case 3:
                    ms.WriteByte(0);
                    break;
            }
            if ((b == 1) || (b == 2))
            {
                ms.WriteByte(0);
                ms.WriteByte(0);
            }
        }

        protected override void WriteHeader(MemoryStream ms)
        {
            ms.Write(base.Encoding.GetBytes(base.FrameId), 0, 4);
            ms.Write(Utilities.Int32ToByteArray(this.CalculateFrameLength()), 0, 4);
            ms.Write(base.Flags, 0, 2);
        }

        private void WriteMimeType(MemoryStream ms)
        {
            ms.Write(Encoding.GetEncoding("iso-8859-1").GetBytes(this.mimeType), 0, this.mimeType.Length);
            ms.WriteByte(0);
        }

        private void WritePictureData(MemoryStream ms)
        {
            if ((this.pictureData != null) && (this.pictureData.Length > 0L))
            {
                int numBytes;
                this.pictureData.Position = 0L;
                byte[] bytes = new byte[0x1000];
                while ((numBytes = this.pictureData.Read(bytes, 0, 0x1000)) > 0)
                {
                    ms.Write(bytes, 0, numBytes);
                }
            }
        }

        protected override void WriteValue(MemoryStream ms)
        {
            ms.WriteByte(base.GetEncodingByte());
            this.WriteMimeType(ms);
            ms.WriteByte(this.pictureType);
            this.WriteDescription(ms);
            this.WritePictureData(ms);
        }

        public static Image ResizePicture(Image image, Size size)
        {
            double ratio = 0d;
            double myThumbWidth = 0d;
            double myThumbHeight = 0d;
            int x = 0;
            int y = 0;

            Bitmap bp;

            if ((image.Width / Convert.ToDouble(size.Width)) > (image.Height / Convert.ToDouble(size.Height)))
                ratio = Convert.ToDouble(image.Width) / Convert.ToDouble(size.Width);
            else
                ratio = Convert.ToDouble(image.Height) / Convert.ToDouble(size.Height);
            myThumbHeight = Math.Min(Math.Ceiling(image.Height / ratio), size.Height);
            myThumbWidth = Math.Min(Math.Ceiling(image.Width / ratio), size.Width);

            Size thumbSize = new Size((int)myThumbWidth, (int)myThumbHeight);
            bp = new Bitmap(thumbSize.Width, thumbSize.Height);
            //x = (size.Width - thumbSize.Width) / 2;
            //y = (size.Height - thumbSize.Height);

            Graphics g = Graphics.FromImage(bp);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            Rectangle rect = new Rectangle(x, y, thumbSize.Width, thumbSize.Height);
            g.DrawImage(image, rect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
            g.Dispose();
            return bp;
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

        public MemoryStream PictureData
        {
            get
            {
                return this.pictureData;
            }
            set
            {
                this.pictureData = value;
            }
        }

        public byte PictureType
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
    }
}

