using System;
using System.Collections.Generic;
using System.Text;

namespace ID3Lib
{
    public class TPE1TextFrame : V2TextFrame
    {
        public TPE1TextFrame(byte[] frameValue)
            : base("TPE1", frameValue)
        {
        }


        public TPE1TextFrame(string artistName, EncodingTypes encodingType)
            : this(artistName)
        {
            SetEncoding(encodingType);
        }


        public TPE1TextFrame(string artistName)
        {
            FrameId = "TPE1";
            FrameValue = EncodeSimpleString(artistName);
        }
    }
}
