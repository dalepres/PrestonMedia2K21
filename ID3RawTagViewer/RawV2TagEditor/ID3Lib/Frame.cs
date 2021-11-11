using System;
using System.Collections.Generic;
using System.Resources;
using System.Globalization;
using System.Text;
using System.IO;
using System.Reflection;

namespace ID3Lib
{
    public class Frame
    {
        private int headerLength;
        private string frameId;
        private byte[] frameValue;
        private byte[] headerBytes;

        // TODO: Add proper handling of V2.3 frame flags per spec.
        private byte[] flags = new byte[] { 0, 0 };
        private Encoding encoding = Encoding.GetEncoding("iso-8859-1");

        private byte[] stringTerminator = new byte[] { 0 };

        private byte[] bom;


        #region Properties


        public byte[] HeaderBytes
        {
            get { return headerBytes; }
            set { headerBytes = value; }
        }

        protected byte[] BOM
        {
            get { return bom; }
        }

        protected byte[] StringTerminator
        {
            get { return stringTerminator; }
        }

        public Encoding Encoding
        {
            get { return this.encoding; }
        }


        public virtual string FrameDescription
        {
            //get { return string.Empty; }
            get
            {
                ResourceManager rm = new ResourceManager("Preston.Media.Resources",
                    Assembly.GetExecutingAssembly());
                string description = rm.GetString("FrameDesc" + this.FrameId);

                return (description == null || description.Length == 0) ? "Unknown Frame" : description;
            }
        }


        public string FrameId
        {
            get { return frameId; }
            set { frameId = value; }
        }


        public byte[] FrameValue
        {
            get { return frameValue; }
            set { frameValue = value; }
        }


        public byte[] Flags
        {
            get { return flags; }
            set { flags = value; }
        }


        public override string ToString()
        {
            return this.frameId;
        }

        #endregion Properties


        //public void SetEncoding(int id3Encoding)
        //{
        //    SetEncoding((EncodingType)id3Encoding);
        //}

        public void SetEncoding(EncodingType encodingType)
        {
            switch (encodingType)
            {
                case EncodingType.ISO8859 :
                    this.encoding = Encoding.GetEncoding("iso-8859-1");
                    bom = new byte[0];
                    break;
                case EncodingType.Unicode :
                    this.encoding = Encoding.GetEncoding("utf-16");
                    bom = new byte[] { 0xFF, 0xFE };
                    stringTerminator = new byte[] { 0, 0 };
                    break;
                case EncodingType.UnicodeBE :
                    this.encoding = Encoding.GetEncoding("unicodeFFFE");
                    bom = new byte[] { 0xFE, 0xFF };
                    stringTerminator = new byte[] { 0, 0 };
                    break;
                // TODO:  Add this back in when ID3V2.4 is implemented.
                //case EncodingType.UTF8 :
                //    this.encoding = Encoding.GetEncoding("utf-8");
                //    bom = ???
                //    break;
            }
        }

        public int HeaderLength
        {
            get { return headerLength ; }
            set { headerLength = value; }
        }

        protected byte[] EncodeSimpleString(string value)
        {
            byte b = GetEncodingByte();
            List<byte> bytes = new List<byte>();
            bytes.Add(b);
            bytes.AddRange(encoding.GetBytes(value));

            switch (b)
            {
                case 0:
                case 3:
                    bytes.Add(0);
                    break;
                case 1:
                    bytes.InsertRange(1, new byte[] { 0xFF, 0xFE });
                    goto case 2;
                case 2:
                    bytes.Add(0);
                    bytes.Add(0);
                    break;
            }

            return bytes.ToArray();
        }


        protected byte GetEncodingByte()
        {
            if (this.encoding.HeaderName == Encoding.UTF8.HeaderName) // utf-8
            {
                return 3;
            }
            else if (this.encoding.HeaderName == Encoding.Unicode.HeaderName) // utf-16
            {
                return 1;
            }
            else if (encoding.HeaderName == Encoding.BigEndianUnicode.HeaderName) // unicodeFFFE
            {
                return 2;
            }
            else if (this.encoding.HeaderName == "iso-8859-1")
            {
                return 0;
            }
            else
            {
                throw new Exception("Frame Encoding is invalid.");
            }
        }

        internal virtual int GetLength()
        {
            return frameValue.Length + headerLength;
        }

        internal virtual void UpdateFrameValue()
        {
            return;
        }

        public virtual byte[] ToByteArray()
        {
            MemoryStream ms = new MemoryStream();
            Write(ms);
            return ms.ToArray();
        }

        internal virtual int Write(MemoryStream ms)
        {
            long startLength = ms.Length;
            WriteHeader(ms);
            WriteValue(ms);
            return (int)(ms.Length - startLength);
        }

        protected virtual void WriteHeader(MemoryStream ms)
        {
            ms.Write(this.HeaderBytes, 0, this.HeaderBytes.Length);
        }

        protected virtual void WriteValue(MemoryStream ms)
        {
            UpdateFrameValue();
            ms.Write(this.FrameValue, 0, this.FrameValue.Length);
        }

        private static string GetV1TextFrameValue(V1Tag tag, string frameId)
        {
            switch (frameId.ToUpper())
            {
                case "ALBUM" :
                    return tag.Album;
                case "ARTIST" :
                    return tag.Artist;
                case "COMMENT" :
                    return tag.Comment;
                case "TITLE" :
                    return tag.Title;
                case "YEAR" :
                    return tag.Year;
            }

            return string.Empty;
        }

        internal static string[] ParseNullTerminatedStringList(byte[] bytes, byte id3Encoding)
        {
            byte[] BOM = new byte[0];
            byte[] terminationBytes = new byte[] { 0 };

           if (id3Encoding > 0)
            {
                BOM = new byte[] { bytes[0], bytes[1] };
                terminationBytes = new byte[] { 0, 0 };
            }

            System.Text.Encoding encoding = GetID3Encoding(id3Encoding, BOM);

            if (bytes.Length > (terminationBytes.Length + BOM.Length))
            {
                string newString = encoding.GetString(bytes);
                string bomString = encoding.GetString(BOM);

                if (BOM.Length > 0)
                {
                    newString = newString.Replace(bomString, "");
                }
                string[] stringArray = newString.Split((char)0);
                int stringCount = stringArray.Length;

                if (LastStringIsTerminated(newString, terminationBytes, encoding))
                {
                    stringCount--;
                }
                string[] newArray = new string[stringCount];


                Array.Copy(stringArray, 0, newArray, 0, stringCount);
                return newArray;
            }
            else
            {
                return new string[0];
            }
        }

        private static bool LastStringIsTerminated(string strings, byte[] terminationBytes, System.Text.Encoding encoding)
        {
            string termString = encoding.GetString(terminationBytes);
            return strings.EndsWith(termString);
        }


        internal static System.Text.Encoding GetID3Encoding(byte id3Encoding, byte[] BOM)
        {
            if (id3Encoding == 0)
                return Encoding.GetEncoding("iso-8859-1");
            else if (id3Encoding == 1)
            {
                if (BOM[0] == 0xFF && BOM[1] == 0xFE)
                    return Encoding.GetEncoding("utf-16");
                else if (BOM[0] == 0xFE && BOM[1] == 0xFF)
                    return Encoding.GetEncoding("unicodeFFFE");
                else
                {
                    throw new ArgumentException("Invalid BOM value.");
                }
            }
            else
            {
                throw new ArgumentException("Invalid id3Encoding value.");
            }
        }
    }

    public enum EncodingType
    {
        ISO8859,
        Unicode,
        UnicodeBE,
        UTF8
    }
}
