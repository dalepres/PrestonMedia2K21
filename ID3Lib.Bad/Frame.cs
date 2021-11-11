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

        // TODO: Add proper handling of V2.3 frame headers per spec.
        private byte[] flags = new byte[] { 0, 0 };
        private Encoding encoding = Encoding.GetEncoding("iso-8859-1");

        private byte[] stringTerminator = new byte[] { 0 };

        private byte[] bom = new byte[0];


        #region Properties

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
                ResourceManager rm = new ResourceManager("ID3Lib.Resources",
                    Assembly.GetExecutingAssembly());
                return rm.GetString("FrameDesc" + this.FrameId, new CultureInfo("es-MX"));
                //System.Threading.Thread.CurrentThread.CurrentUICulture);
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

        public void SetEncoding(EncodingTypes encodingType)
        {
            switch (encodingType)
            {
                case EncodingTypes.ISO8859 :
                    this.encoding = Encoding.GetEncoding("iso-8859-1");
                    bom = new byte[0];
                    break;
                case EncodingTypes.Unicode :
                    this.encoding = Encoding.GetEncoding("utf-16");
                    bom = new byte[] { 0xFF, 0xFE };
                    stringTerminator = new byte[] { 0, 0 };
                    break;
                case EncodingTypes.UnicodeBE :
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

        internal virtual int Write(MemoryStream ms)
        {
            long startLength = ms.Length;
            WriteHeader(ms);
            WriteValue(ms);
            return (int)(ms.Length - startLength);
        }

        protected virtual void WriteHeader(MemoryStream ms)
        {
            throw new NotImplementedException();
        }

        protected virtual void WriteValue(MemoryStream ms)
        {
            UpdateFrameValue();
            ms.Write(this.FrameValue, 0, this.FrameValue.Length);
        }


        public static string GetTextFrameValue(object tag, string frameId)
        {
            if (tag is V23Tag)
            {
                return GetV23TextFrameValue((V23Tag)tag, frameId);
            }
            else if (tag is V22Tag)
            {
                return GetV22TextFrameValue((V22Tag)tag, frameId);
            }
            else if (tag is V1Tag)
            {
                return GetV1TextFrameValue((V1Tag)tag, frameId);
            }
            else
            {
                throw new ArgumentException("Invalid ID3Tag type.");
            }
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

        internal static string GetV22TextFrameValue(V22Tag tag, string frameId)
        {
            V22Frame frame = tag.Frames.GetFrame(frameId);
            return (frame == null) ? string.Empty : ((V22TextFrame)frame).Text;
        }

        internal static string GetV23TextFrameValue(V23Tag tag, string frameId)
        {
            V2Frame frame = tag.Frames.GetFrame(frameId);
            return (frame == null) ? string.Empty : ((V2TextFrame)frame).Text;
        }

        internal static byte GetTrackNumber(object tag)
        {
            if (tag is V23Tag)
            {
                return ParseTrackString(Frame.GetV23TextFrameValue((V23Tag)tag, "TRCK"));
            }
            else if (tag is V22Tag)
            {
                return ParseTrackString(Frame.GetV22TextFrameValue((V22Tag)tag, "TRK"));
            }
            else if (tag is V1Tag)
            {
                V1Tag v1 = (V1Tag)tag;
                return v1.MajorVersion == 1 ? v1.TrackNumber : (byte)0;
            }
            else 
            {
                return 0;
            }
        }

        private static byte ParseTrackString(string trackString)
        {
            string trackNumber;
            byte trackByte;

            if (trackString.IndexOf("/") > 0)
            {
                trackNumber = trackString.Split('/')[0];
            }
            else
            {
                trackNumber = trackString;
            }

            byte.TryParse(trackNumber, out trackByte);

            return trackByte;
        }


        internal static string[] ParseNullTerminatedStringList(byte[] bytes, byte id3Encoding)
        {
            return ParseNullTerminatedStringList(bytes, id3Encoding, 0);
        }

        internal static string[] ParseNullTerminatedStringList(byte[] bytes, byte id3Encoding, int maxLength)
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
                string[] newArray = new string[GetOptionalMaxCount(maxLength, stringCount)];


                Array.Copy(stringArray, 0, newArray, 0, GetOptionalMaxCount(maxLength, stringCount));
                return newArray;
            }
            else
            {
                return new string[0];
            }
        }

        private static int GetOptionalMaxCount(int maxLength, int count)
        {
            return maxLength > 0 ? Math.Min(count, maxLength) : count;
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

    public enum EncodingTypes
    {
        ISO8859,
        Unicode,
        UnicodeBE,
        UTF8
    }
}
