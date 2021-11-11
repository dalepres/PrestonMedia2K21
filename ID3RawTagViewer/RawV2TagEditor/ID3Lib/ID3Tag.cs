using System;
using System.Collections.Generic;
using System.Text;

namespace ID3Lib
{
    public class ID3Tag
    {
        public static bool IsId3Header(byte[] headerBytes)
        {
            return headerBytes.Length == 10
                && StartsWithID3(headerBytes)
                && headerBytes[3] < 16
                && headerBytes[4] < 16
                && headerBytes[6] < 128
                && headerBytes[7] < 128
                && headerBytes[8] < 128
                && headerBytes[9] < 128;
        }


        public static bool StartsWithID3(byte[] bytes)
        {
            return bytes[0] == 73
                && bytes[1] == 68
                && bytes[2] == 51;
        }
    }
}
