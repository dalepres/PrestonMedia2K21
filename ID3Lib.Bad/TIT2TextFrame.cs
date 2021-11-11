using System;
using System.Collections.Generic;
using System.Text;

namespace ID3Lib
{
    public class TIT2TextFrame : V2TextFrame
    {
        public TIT2TextFrame(byte[] frameValue)
            : base("TIT2", frameValue)
        {
        }


        public TIT2TextFrame(string trackName, EncodingTypes encodingType)
            : this(trackName)
        {
            SetEncoding(encodingType);
        }


        public TIT2TextFrame(string trackName)
        {
            FrameId = "TIT2";
            FrameValue = EncodeSimpleString(trackName);
        }
    }
}
