using System;
using System.Collections.Generic;
using System.Text;

namespace ID3Lib
{
    public class TYERTextFrame : V2TextFrame
    {
        public TYERTextFrame(byte[] frameValue)
            : base("TYER", frameValue)
        {
        }


        public TYERTextFrame(string year, EncodingTypes encodingType)
            : this(year)
        {
            SetEncoding(encodingType);
        }


        public TYERTextFrame(string year)
        {
            FrameId = "TYER";
            FrameValue = EncodeSimpleString(year);
        }
    }
}
