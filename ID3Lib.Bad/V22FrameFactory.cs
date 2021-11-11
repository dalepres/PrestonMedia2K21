using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace ID3Lib
{
    public static class V22FrameFactory
    {
        internal const byte FRAME_ID_LENGTH = 3;
        internal const byte FRAME_HEADER_LENGTH = 6;
        internal const byte HEADER_SIZE_LENGTH = 3;
        internal const byte HEADER_SIZE_START = 3;
        internal const byte FRAME_ID_START = 0;
        internal const byte V22_TAG_MAJOR_VERSION = 2;

        public static V22Frame CreateFrame(string frameId, byte[] frameValue)
        {
            if (frameValue.Length == 0)
            {
                return null;
            }

            switch (frameId)
            {
                case "TCO" :
                    return new TCOTextFrame(frameValue);
                case "TT2" :
                    return new TT2TextFrame(frameValue);
                case "TRK" :
                    return new TRKTextFrame(frameValue);
                case "TAL" :
                    return new TALTextFrame(frameValue);
                case "TP1" :
                    return new TP1TextFrame(frameValue);
                case "TP2" :
                    return new TP2TextFrame(frameValue);
                case "TPA":
                    return new TPATextFrame(frameValue);
                case "TYE":
                    return new TYETextFrame(frameValue);
                case "COM" :
                    return new COMFrame(frameValue);
                case "PIC" :
                    return new PICFrame(frameValue);
                default :
                    return new UnknownV22Frame(frameId, frameValue);
            }
        }


        public static V22Frame CreateFrame(FileStream fs, int majorVersion, byte[] headerBytes)
        {
            if (headerBytes.Length != FRAME_HEADER_LENGTH || fs == null)
            {
                return null;
            }

            string frameId = Encoding.UTF8.GetString(headerBytes, FRAME_ID_START, FRAME_ID_LENGTH);
            byte[] lengthBytes = new byte[HEADER_SIZE_LENGTH];
            Array.Copy(headerBytes, HEADER_SIZE_START, lengthBytes, 0, HEADER_SIZE_LENGTH);

            int length = 0;
            if (majorVersion == V22_TAG_MAJOR_VERSION)
                length = Utilities.ReadInt24(lengthBytes);
            else
            {
                return null;
            }

            if (length == 0)
            {
                return null;
            }

            byte[] frameValue = new byte[length];
            if (fs.Read(frameValue, 0, length) == length)
            {
                V22Frame frame = CreateFrame(frameId, frameValue);
                return frame;
            }

            return null;
        }

        public static V22Frame CreateTextFrame(string frameId, string frameText, int id3Encoding)
        {
            return CreateFrame(frameId, EncodeSimpleString(frameText, id3Encoding));
        }


        /// <summary>
        /// Encodes a string using the ID3 encoding type specified and formats it as 
        /// a single byte for the ID3 encoding type + the encoded bytes + the appropriate
        /// null termination for the encoding type.  This meets the specifications for
        /// many, but not all, ID3 text frames.
        /// </summary>
        /// <param name="text">The text to encode</param>
        /// <param name="id3Encoding">0 or 1 equal to one of the documented encoding
        /// types specified in the ID3V2.2 standard.</param>
        /// <returns>A byte array containing the ID3 encoding type byte + the encoded 
        /// string + the null termination appropriate to the ID3 encoding type.</returns>
        public static byte[] EncodeSimpleString(string text, int id3Encoding)
        {
            List<byte> bytes = new List<byte>();
            bytes.Add((byte)id3Encoding);

            if (id3Encoding == 1)
            {
                bytes.InsertRange(1, new byte[] { 0xFF, 0xFE });
            }

            bytes.AddRange(GetId3EncodingType(id3Encoding).GetBytes(text));

            switch (id3Encoding)
            {
                case 0:
                    bytes.Add(0);
                    break;
                case 1:
                    bytes.Add(0);
                    bytes.Add(0);
                    break;
            }

            return bytes.ToArray();
        }


        internal static Encoding GetId3EncodingType(int id3Encoding)
        {
            switch (id3Encoding)
            {
                case 0:
                    return Encoding.GetEncoding("iso-8859-1");
                case 1:
                    return Encoding.GetEncoding("utf-16");
                //case 2:
                //    return Encoding.GetEncoding("unicodeFFFE");
                //case 3:
                //    return Encoding.GetEncoding("utf-8");
                default:
                    throw new FormatException("Invalid id3Encoding parameter.  Must be 0 or 1.");
            }
        }
    }
}
