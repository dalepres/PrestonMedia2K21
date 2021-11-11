using System;
using System.Collections.Generic;
using System.Text;

namespace ID3Lib
{
    public class TPOSTextFrame : V2TextFrame
    {
        public TPOSTextFrame(byte[] frameValue)
            : base("TPOS", frameValue)
        {
        }


        public TPOSTextFrame(string bandName, EncodingTypes encodingType)
            : this(bandName)
        {
            SetEncoding(encodingType);
        }


        public TPOSTextFrame(string bandName)
        {
            FrameId = "TPOS";
            FrameValue = EncodeSimpleString(bandName);
        }
    }
}
