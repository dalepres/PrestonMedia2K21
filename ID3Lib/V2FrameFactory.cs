namespace ID3Lib
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public static class V2FrameFactory
    {
        internal const byte FRAME_HEADER_LENGTH = 10;
        internal const byte FRAME_ID_LENGTH = 4;
        internal const byte FRAME_ID_START = 0;
        internal const byte HEADER_SIZE_LENGTH = 4;
        internal const byte HEADER_SIZE_START = 4;
        internal const byte V23_TAG_MAJOR_VERSION = 3;
        internal const byte V24_TAG_MAJOR_VERSION = 4;

        public static V2Frame CreateFrame(string frameId, byte[] frameValue)
        {
            if (frameValue.Length == 0)
            {
                return null;
            }
            switch (frameId)
            {
                case "TCON":
                    return new TCONTextFrame(frameValue);

                case "TIT2":
                    return new TIT2TextFrame(frameValue);

                case "TRCK":
                    return new TRCKTextFrame(frameValue);

                case "TALB":
                    return new TALBTextFrame(frameValue);

                case "TPE1":
                    return new TPE1TextFrame(frameValue);

                case "TPE2":
                    return new TPE2TextFrame(frameValue);

                case "TPOS":
                    return new TPOSTextFrame(frameValue);

                case "TYER":
                    return new TYERTextFrame(frameValue);

                case "COMM":
                    if (frameValue.Length == 5) { return null; }
                    return new COMMFrame(frameValue);

                case "APIC":
                    return new APICFrame(frameValue);
            }
            return new UnknownV2Frame(frameId, frameValue);
        }

        public static V2Frame CreateFrame(FileStream fs, int majorVersion, byte[] headerBytes)
        {
            string frameId;
            int length;
            if ((headerBytes.Length == 10) && (fs != null))
            {
                frameId = Encoding.UTF8.GetString(headerBytes, 0, 4);
                byte[] lengthBytes = new byte[4];
                Array.Copy(headerBytes, 4, lengthBytes, 0, 4);
                length = 0;
                if (majorVersion == 3)
                {
                    length = Utilities.ReadInt32(lengthBytes);
                    goto Label_0075;
                }
                if (majorVersion == 4)
                {
                    length = Utilities.ReadInt28(lengthBytes);
                    goto Label_0075;
                }
            }
            return null;
        Label_0075:
            if (length != 0)
            {
                byte[] frameValue = new byte[length];
                if (fs.Read(frameValue, 0, length) == length)
                {
                    V2Frame frame = CreateFrame(frameId, frameValue);
                    if (frame != null) {
                        frame.Flags = new byte[] { headerBytes[8], headerBytes[9] };
                     }
                    return frame;
                }
            }
            return null;
        }

        public static V2Frame CreateTextFrame(string frameId, string frameText, int id3Encoding)
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

