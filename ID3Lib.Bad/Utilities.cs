using System;
using System.Collections.Generic;
using System.Text;

namespace ID3Lib
{
    internal static class Utilities
    {
        internal static int ReadInt28(byte[] bytes)
        {
            //if ((bytes[0] & 0x80) != 0 || (bytes[1] & 0x80) != 0 || (bytes[2] & 0x80) != 0 || (bytes[3] & 0x80) != 0)
            //    throw new TagsException("Found invalid syncsafe integer");

            int result = (bytes[0] << 21) | (bytes[1] << 14) | (bytes[2] << 7) | bytes[3];
            return result;
        }

        internal static int ReadInt32(byte[] bytes)
        {
            int result = (bytes[0] << 24) | (bytes[1] << 16) | (bytes[2] << 8) | bytes[3];
            return result;
        }


        internal static int ReadInt24(byte[] bytes)
        {
            int result = (bytes[0] << 16) | (bytes[1] << 8) | bytes[2];
            return result;
        }

        internal static short ByteArrayToShort(byte[] flagBytes)
        {
            short frameFlags = (short)((flagBytes[0] << 8) | flagBytes[1]);
            return frameFlags;
        }


        internal static byte[] Int28ToByteArray(int n)
        {
            byte[] buffer = new byte[4];

            buffer[3] = (byte)(n & 0x7F);
            n >>= 7;
            buffer[2] = (byte)(n & 0x7F);
            n >>= 7;
            buffer[1] = (byte)(n & 0x7F);
            n >>= 7;
            buffer[0] = (byte)(n & 0x7F);

            return buffer;
        }


        internal static byte[] Int32ToByteArray(int i)
        {
            byte[] bytes = new byte[4];
            bytes[0] = (byte)((i & 0xff000000) >> 24);
            bytes[1] = (byte)((i & 0x00ff0000) >> 16);
            bytes[2] = (byte)((i & 0x0000ff00) >> 8);
            bytes[3] = (byte)(i & 0x000000ff);

            return bytes;
        }


        internal static byte[] Int24ToByteArray(int i)
        {
            byte[] bytes = new byte[0];
            bytes[0] = (byte)((i & 0x00ff0000) >> 16);
            bytes[1] = (byte)((i & 0x0000ff00) >> 8);
            bytes[2] = (byte)(i & 0x000000ff);

            return bytes;
        }
    }
}
