using System;
using System.Collections.Generic;
using System.Text;

namespace ID3Lib
{
    public class TPE2TextFrame : V2TextFrame
    {
        public TPE2TextFrame(byte[] frameValue)
            : base("TPE2", frameValue)
        {
        }


        public TPE2TextFrame(string bandName, EncodingTypes encodingType)
            : this(bandName)
        {
            SetEncoding(encodingType);
        }


        public TPE2TextFrame(string bandName)
        {
            FrameId = "TPE2";
            FrameValue = EncodeSimpleString(bandName);
        }
    }
}
