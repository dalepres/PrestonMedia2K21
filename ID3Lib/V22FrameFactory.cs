namespace ID3Lib
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public static class V22FrameFactory
    {
        internal const byte FRAME_HEADER_LENGTH = 6;
        internal const byte FRAME_ID_LENGTH = 3;
        internal const byte FRAME_ID_START = 0;
        internal const byte HEADER_SIZE_LENGTH = 3;
        internal const byte HEADER_SIZE_START = 3;
        internal const byte V22_TAG_MAJOR_VERSION = 2;

        public static V22Frame CreateFrame(string frameId, byte[] frameValue)
        {
            if (frameValue.Length == 0)
            {
                return null;
            }
            switch (frameId)
            {
                case "TCO":
                    return new TCOTextFrame(frameValue);

                case "TT2":
                    return new TT2TextFrame(frameValue);

                case "TRK":
                    return new TRKTextFrame(frameValue);

                case "TAL":
                    return new TALTextFrame(frameValue);

                case "TP1":
                    return new TP1TextFrame(frameValue);

                case "TP2":
                    return new TP2TextFrame(frameValue);

                case "TPA":
                    return new TPATextFrame(frameValue);

                case "TYE":
                    return new TYETextFrame(frameValue);

                case "COM":
                    return new COMFrame(frameValue);

                case "PIC":
                    return new PICFrame(frameValue);
            }
            return new UnknownV22Frame(frameId, frameValue);
        }

        public static V22Frame CreateFrame(FileStream fs, int majorVersion, byte[] headerBytes)
        {
            if ((headerBytes.Length == 6) && (fs != null))
            {
                string frameId = Encoding.UTF8.GetString(headerBytes, 0, 3);
                byte[] lengthBytes = new byte[3];
                Array.Copy(headerBytes, 3, lengthBytes, 0, 3);
                int length = 0;
                if (majorVersion == 2)
                {
                    length = Utilities.ReadInt24(lengthBytes);
                    if (length != 0)
                    {
                        byte[] frameValue = new byte[length];
                        if (fs.Read(frameValue, 0, length) == length)
                        {
                            return CreateFrame(frameId, frameValue);
                        }
                    }
                    return null;
                }
            }
            return null;
        }

        public static V22Frame CreateTextFrame(string frameId, string frameText, int id3Encoding)
        {
            return CreateFrame(frameId, EncodeSimpleString(frameText, id3Encoding));
        }

        public static byte[] EncodeSimpleString(string text, int id3Encoding)
        {
            List<byte> bytes = new List<byte>();
            bytes.Add((byte) id3Encoding);
            if (id3Encoding == 1)
            {
                bytes.InsertRange(1, new byte[] { 0xff, 0xfe });
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
            }
            throw new FormatException("Invalid id3Encoding parameter.  Must be 0 or 1.");
        }
    }
}

