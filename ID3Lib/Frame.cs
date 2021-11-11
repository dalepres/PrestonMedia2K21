namespace ID3Lib
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Resources;
    using System.Text;

    public class Frame
    {
        private byte[] bom;
        private System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("iso-8859-1");
        private byte[] flags = new byte[2];
        private string frameId;
        private byte[] frameValue;
        private int headerLength;
        private byte[] stringTerminator = new byte[1];

        protected byte[] EncodeSimpleString(string value)
        {
            byte b = this.GetEncodingByte();
            List<byte> bytes = new List<byte>();
            bytes.Add(b);
            bytes.AddRange(this.encoding.GetBytes(value));
            switch (b)
            {
                case 0:
                case 3:
                    bytes.Add(0);
                    goto Label_0085;

                case 1:
                    bytes.InsertRange(1, new byte[] { 0xff, 0xfe });
                    break;

                case 2:
                    break;

                default:
                    goto Label_0085;
            }
            bytes.Add(0);
            bytes.Add(0);
        Label_0085:
            return bytes.ToArray();
        }

        protected byte GetEncodingByte()
        {
            if (this.encoding.HeaderName == System.Text.Encoding.UTF8.HeaderName)
            {
                return 3;
            }
            if (this.encoding.HeaderName == System.Text.Encoding.Unicode.HeaderName)
            {
                return 1;
            }
            if (this.encoding.HeaderName == System.Text.Encoding.BigEndianUnicode.HeaderName)
            {
                return 2;
            }
            if (this.encoding.HeaderName != "iso-8859-1")
            {
                throw new Exception("Frame Encoding is invalid.");
            }
            return 0;
        }

        internal static System.Text.Encoding GetID3Encoding(byte id3Encoding, byte[] BOM)
        {
            if (id3Encoding == 0)
            {
                return System.Text.Encoding.GetEncoding("iso-8859-1");
            }
            if (id3Encoding != 1)
            {
                throw new ArgumentException("Invalid id3Encoding value.");
            }
            if ((BOM[0] == 0xff) && (BOM[1] == 0xfe))
            {
                return System.Text.Encoding.GetEncoding("utf-16");
            }
            if ((BOM[0] != 0xfe) || (BOM[1] != 0xff))
            {
                throw new ArgumentException("Invalid BOM value.");
            }
            return System.Text.Encoding.GetEncoding("unicodeFFFE");
        }

        internal virtual int GetLength()
        {
            return (this.frameValue.Length + this.headerLength);
        }

        public static string GetTextFrameValue(object tag, string frameId)
        {
            if (tag is V23Tag)
            {
                return GetV23TextFrameValue((V23Tag) tag, frameId);
            }
            if (tag is V22Tag)
            {
                return GetV22TextFrameValue((V22Tag) tag, frameId);
            }
            if (!(tag is V1Tag))
            {
                throw new ArgumentException("Invalid ID3Tag type.");
            }
            return GetV1TextFrameValue((V1Tag) tag, frameId);
        }

        internal static byte GetTrackNumber(object tag)
        {
            if (tag is V23Tag)
            {
                return ParseTrackString(GetV23TextFrameValue((V23Tag) tag, "TRCK"));
            }
            if (tag is V22Tag)
            {
                return ParseTrackString(GetV22TextFrameValue((V22Tag) tag, "TRK"));
            }
            if (tag is V1Tag)
            {
                V1Tag v1 = (V1Tag) tag;
                return ((v1.MajorVersion == 1) ? v1.TrackNumber : ((byte) 0));
            }
            return 0;
        }

        private static string GetV1TextFrameValue(V1Tag tag, string frameId)
        {
            switch (frameId.ToUpper())
            {
                case "ALBUM":
                    return tag.Album;

                case "ARTIST":
                    return tag.Artist;

                case "COMMENT":
                    return tag.Comment;

                case "TITLE":
                    return tag.Title;

                case "YEAR":
                    return tag.Year;
            }
            return string.Empty;
        }

        internal static string GetV22TextFrameValue(V22Tag tag, string frameId)
        {
            V22Frame frame = tag.Frames.GetFrame(frameId);
            return ((frame == null) ? string.Empty : ((V22TextFrame) frame).Text);
        }

        internal static string GetV23TextFrameValue(V23Tag tag, string frameId)
        {
            V2Frame frame = tag.Frames.GetFrame(frameId);
            return ((frame == null) ? string.Empty : ((V2TextFrame) frame).Text);
        }

        private static bool LastStringIsTerminated(string strings, byte[] terminationBytes, System.Text.Encoding encoding)
        {
            string termString = encoding.GetString(terminationBytes);
            return strings.EndsWith(termString);
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
                string[] stringArray = newString.Split(new char[1]);
                int stringCount = stringArray.Length;
                if (LastStringIsTerminated(newString, terminationBytes, encoding))
                {
                    stringCount--;
                }
                string[] newArray = new string[stringCount];
                Array.Copy(stringArray, 0, newArray, 0, stringCount);
                return newArray;
            }
            return new string[0];
        }

        private static byte ParseTrackString(string trackString)
        {
            string trackNumber;
            byte trackByte;
            if (trackString.IndexOf("/") > 0)
            {
                trackNumber = trackString.Split(new char[] { '/' })[0];
            }
            else
            {
                trackNumber = trackString;
            }
            byte.TryParse(trackNumber, out trackByte);
            return trackByte;
        }

        public void SetEncoding(EncodingTypes encodingType)
        {
            switch (encodingType)
            {
                case EncodingTypes.ISO8859:
                    this.encoding = System.Text.Encoding.GetEncoding("iso-8859-1");
                    this.bom = new byte[0];
                    break;

                case EncodingTypes.Unicode:
                    this.encoding = System.Text.Encoding.GetEncoding("utf-16");
                    this.bom = new byte[] { 0xff, 0xfe };
                    this.stringTerminator = new byte[2];
                    break;

                case EncodingTypes.UnicodeBE:
                    this.encoding = System.Text.Encoding.GetEncoding("unicodeFFFE");
                    this.bom = new byte[] { 0xfe, 0xff };
                    this.stringTerminator = new byte[2];
                    break;
            }
        }

        public override string ToString()
        {
            return this.frameId;
        }

        internal virtual void UpdateFrameValue()
        {
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
            this.WriteHeader(ms);
            this.WriteValue(ms);
            return (int) (ms.Length - startLength);
        }

        protected virtual void WriteHeader(MemoryStream ms)
        {
            throw new NotImplementedException();
        }

        protected virtual void WriteValue(MemoryStream ms)
        {
            this.UpdateFrameValue();
            ms.Write(this.FrameValue, 0, this.FrameValue.Length);
        }

        protected byte[] BOM
        {
            get
            {
                return this.bom;
            }
        }

        public System.Text.Encoding Encoding
        {
            get
            {
                return this.encoding;
            }
        }

        public byte[] Flags
        {
            get
            {
                return this.flags;
            }
            set
            {
                this.flags = value;
            }
        }

        public virtual string FrameDescription
        {
            get
            {
                ResourceManager rm = new ResourceManager("ID3Lib.Resources", Assembly.GetExecutingAssembly());
                return rm.GetString("FrameDesc" + this.FrameId, new CultureInfo("es-MX"));
            }
        }

        public string FrameId
        {
            get
            {
                return this.frameId;
            }
            set
            {
                this.frameId = value;
            }
        }

        public byte[] FrameValue
        {
            get
            {
                return this.frameValue;
            }
            set
            {
                this.frameValue = value;
            }
        }

        public int HeaderLength
        {
            get
            {
                return this.headerLength;
            }
            set
            {
                this.headerLength = value;
            }
        }

        protected byte[] StringTerminator
        {
            get
            {
                return this.stringTerminator;
            }
        }
    }
}

