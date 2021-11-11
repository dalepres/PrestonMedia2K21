using System;
using System.Collections.Generic;
using System.Text;

namespace ID3Lib
{
    public class TALBTextFrame : V2TextFrame
    {
        public TALBTextFrame(byte[] frameValue)
            : base("TALB", frameValue)
        {
        }

        public TALBTextFrame(string albumName, EncodingTypes encodingType)
            : this(albumName)
        {
            SetEncoding(encodingType);
        }


        public TALBTextFrame(string albumName)
        {
            FrameId = "TALB";
            FrameValue = EncodeSimpleString(albumName);
        }
    }
}
