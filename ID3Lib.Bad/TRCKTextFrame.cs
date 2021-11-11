using System;
using System.Collections.Generic;
using System.Text;

namespace ID3Lib
{
    public class TRCKTextFrame : V2TextFrame
    {
        public TRCKTextFrame(byte[] frameValue)
            : base("TRCK", frameValue)
        {
        }


        public TRCKTextFrame(string trackNumber, EncodingTypes encodingType)
            : this(trackNumber)
        {
            SetEncoding(encodingType);
        }


        public TRCKTextFrame(string trackNumber)
        {
            FrameId = "TRCK";
            FrameValue = EncodeSimpleString(trackNumber);
        }
    }
}
