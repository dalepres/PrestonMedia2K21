using System;
using System.Collections.Generic;
using System.Text;

namespace ID3Lib
{
    public class TT2TextFrame : V22TextFrame
    {
        public TT2TextFrame(byte[] frameValue)
            : base("TT2", frameValue)
        {
        }


        public TT2TextFrame(string trackName, EncodingTypes encodingType)
            : this(trackName)
        {
            SetEncoding(encodingType);
        }


        public TT2TextFrame(string trackName)
        {
            FrameId = "TT2";
            FrameValue = EncodeSimpleString(trackName);
        }


        public static explicit operator TIT2TextFrame(TT2TextFrame frame)
        {
            if (frame != null)
            {
                return new TIT2TextFrame(frame.FrameValue);
            }

            return null;
        }
    }
}
