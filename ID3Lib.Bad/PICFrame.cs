using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Text;
using System.IO;


namespace ID3Lib
{
    public class PICFrame : V22Frame
    {
        private string mimeType = string.Empty;
        private string description = string.Empty;
        private byte pictureType = 0;
        private MemoryStream pictureData = null;


        public PICFrame()
        {
            FrameId = "PIC";
        }


        public PICFrame(byte[] frameValue)
            //: base("APIC", frameValue)
        {
            FrameId = "PIC";
            ParseValue(frameValue);
        }
        

        public PICFrame(string mimeType, string description, byte pictureType, Image picture)
        {
            base.FrameId = "PIC";
            this.mimeType = mimeType;
            this.description = description;
            this.pictureType = pictureType;

            this.pictureData = new MemoryStream();
            picture.Save(pictureData, picture.RawFormat);
        }


        internal override void UpdateFrameValue()
        {
            MemoryStream ms = new MemoryStream();

            this.WriteValue(ms);
            this.FrameValue = ms.ToArray();
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


        public byte PictureType
        {
            get { return pictureType; }
            set { pictureType = value; }
        }


        public MemoryStream PictureData
        {
            get { return pictureData; }
            set { pictureData = value; }
        }


        protected override void WriteHeader(System.IO.MemoryStream ms)
        {
            ms.Write(this.Encoding.GetBytes(this.FrameId), 0, 4);
            ms.Write(Utilities.Int32ToByteArray(CalculateFrameLength()), 0, 4);
            ms.Write(Flags, 0, 2);

        }

        private int CalculateFrameLength()
        {
            byte b = GetEncodingByte();
            int frameLength = 0;

            frameLength += mimeType.Length + 3; // mimeType + null termination + encoding byte + pictureType byte

            switch (b)
            {
                case 0 :
                case 3 :
                    frameLength += description.Length + 1; // description + 8-bit null termination
                    break;
                case 1 :
                    frameLength += description.Length + 4; // FFFE + description + 16-bit null termination
                    break;
                case 2 :
                    frameLength += description.Length + 2; // description + 16-bit null termination
                    break;
            }

            if (pictureData != null)
            {
                frameLength += (int)pictureData.Length;
            }

            return frameLength;
        }


        protected override void WriteValue(System.IO.MemoryStream ms)
        {
            ms.WriteByte(GetEncodingByte());
            WriteMimeType(ms);
            ms.WriteByte(pictureType);
            WriteDescription(ms);
            WritePictureData(ms);
        }

        private void WritePictureData(MemoryStream ms)
        {
            if (pictureData != null && pictureData.Length > 0)
            {
                pictureData.Position = 0;
                const int size = 4096;
                byte[] bytes = new byte[4096];
                int numBytes;
                while ((numBytes = pictureData.Read(bytes, 0, size)) > 0)
                    ms.Write(bytes, 0, numBytes);
            }
        }

        private void WriteDescription(MemoryStream ms)
        {
            byte b = GetEncodingByte();

            if (b == 1)
            {
                ms.Write(BOM, 0, 2);
            }

            ms.Write(Encoding.GetBytes(description), 0, Encoding.GetByteCount(description));

            if (b == 0 || b == 3)
            {
                ms.WriteByte(0);
            }

            if (b == 1 || b == 2)
            {
                ms.WriteByte(0);
                ms.WriteByte(0);
            }
        }


        private void WriteMimeType(MemoryStream ms)
        {
            ms.Write(Encoding.GetEncoding("iso-8859-1").GetBytes(mimeType), 0, mimeType.Length);
            ms.WriteByte(0);
        }


        private void ParseValue(byte[] frameValue)
        {
            int pointer = 0;
            LoadPicDescriptionEncoding(frameValue, ref pointer);
            LoadPicMimeType(frameValue, ref pointer);
            LoadPictureType(frameValue, ref pointer);
            LoadDescription(frameValue, ref pointer);
            LoadPictureData(frameValue, ref pointer);
        }


        private void LoadPicDescriptionEncoding(byte[] frameValue, ref int pointer)
        {
            SetEncoding((EncodingTypes)frameValue[pointer++]);
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


        private void LoadPictureType(byte[] frameValue, ref int pointer)
        {
            this.pictureType = frameValue[pointer++];
        }


        private void LoadDescription(byte[] frameValue, ref int pointer)
        {
            byte[] bytes;
            byte b = GetEncodingByte();

            switch (b)
            {
                case 0 :
                case 3 :
                    bytes = Get8BitStringBytes(frameValue, ref pointer);
                    break;
                case 1 :
                case 2 :
                    bytes = Get16BitStringBytes(frameValue, ref pointer);
                    break;
                default :
                    throw new Exception("Cannot get picture description.  Frame Encoding is invalid.");
            }

            description = this.Encoding.GetString(bytes);
        }


        private byte[] Get8BitStringBytes(byte[] frameValue, ref int pointer)
        {
            List<byte> bytes = new List<byte>();
            byte b;
            while ((pointer < frameValue.Length) && ((b = frameValue[pointer++]) != 0))
            {
                bytes.Add(b);
            }

            return bytes.ToArray();
        }


        private byte[] Get16BitStringBytes(byte[] frameValue, ref int pointer)
        {
            List<byte> bytes = new List<byte>();
            byte b1, b2;
            do
            {
                b1 = frameValue[pointer++];
                b2 = frameValue[pointer++];
                if (b1 == 0 && b2 == 0)
                {
                    return bytes.ToArray();
                }

                if (b1 == 0xFF && b2 == 0xFE && GetEncodingByte() == 1)
                {
                    continue;
                }

                bytes.Add(b1);
                bytes.Add(b2);

            } while (pointer < frameValue.Length - 1);

            return bytes.ToArray();
        }


        private void LoadPictureData(byte[] frameValue, ref int pointer)
        {
            this.pictureData = new MemoryStream();
            int length = frameValue.Length - pointer;
            pictureData.Write(frameValue,pointer,length);
        }


        public static explicit operator APICFrame(PICFrame frame)
        {
            if (frame != null)
            {
                return new APICFrame(frame.FrameValue);
            }

            return null;
        }


        #region Properties
        
        


        #endregion Properties
    }
}
