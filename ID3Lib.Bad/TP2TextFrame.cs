using System;
using System.Collections.Generic;
using System.Text;

namespace ID3Lib
{
    public class TP2TextFrame : V22TextFrame
    {
        public TP2TextFrame(byte[] frameValue)
            : base("TP2", frameValue)
        {
        }


        public TP2TextFrame(string bandName, EncodingTypes encodingType)
            : this(bandName)
        {
            SetEncoding(encodingType);
        }


        public TP2TextFrame(string bandName)
        {
            FrameId = "TP2";
            FrameValue = EncodeSimpleString(bandName);
        }


        public static explicit operator TPE2TextFrame(TP2TextFrame frame)
        {
            if (frame != null)
            {
                return new TPE2TextFrame(frame.FrameValue);
            }

            return null;
        }
    }
}
