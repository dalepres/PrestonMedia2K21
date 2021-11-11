namespace ID3Lib
{
    using System;

    internal static class Utilities
    {
        internal static short ByteArrayToShort(byte[] flagBytes)
        {
            return (short) ((flagBytes[0] << 8) | flagBytes[1]);
        }

        internal static byte[] Int24ToByteArray(int i)
        {
            byte[] bytes = new byte[3];
            bytes[0] = (byte) ((i & 0xff0000) >> 0x10);
            bytes[1] = (byte) ((i & 0xff00) >> 8);
            bytes[2] = (byte) (i & 0xff);
            return bytes;
        }

        internal static byte[] Int28ToByteArray(int n)
        {
            byte[] buffer = new byte[4];
            buffer[3] = (byte) (n & 0x7f);
            n = n >> 7;
            buffer[2] = (byte) (n & 0x7f);
            n = n >> 7;
            buffer[1] = (byte) (n & 0x7f);
            n = n >> 7;
            buffer[0] = (byte) (n & 0x7f);
            return buffer;
        }

        internal static byte[] Int32ToByteArray(int i)
        {
            return new byte[] { ((byte) ((i & 0xff000000L) >> 0x18)), ((byte) ((i & 0xff0000) >> 0x10)), ((byte) ((i & 0xff00) >> 8)), ((byte) (i & 0xff)) };
        }

        internal static int ReadInt24(byte[] bytes)
        {
            return (((bytes[0] << 0x10) | (bytes[1] << 8)) | bytes[2]);
        }

        internal static int ReadInt28(byte[] bytes)
        {
            return ((((bytes[0] << 0x15) | (bytes[1] << 14)) | (bytes[2] << 7)) | bytes[3]);
        }

        internal static int ReadInt32(byte[] bytes)
        {
            return ((((bytes[0] << 0x18) | (bytes[1] << 0x10)) | (bytes[2] << 8)) | bytes[3]);
        }
    }
}

