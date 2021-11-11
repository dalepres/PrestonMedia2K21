using System;
using System.Collections.Generic;
using System.Text;

namespace ID3Lib
{
    public class TP1TextFrame : V22TextFrame
    {
        public TP1TextFrame(byte[] frameValue)
            : base("TP1", frameValue)
        {
        }


        public TP1TextFrame(string artistName, EncodingTypes encodingType)
            : this(artistName)
        {
            SetEncoding(encodingType);
        }


        public TP1TextFrame(string artistName)
        {
            FrameId = "TP1";
            FrameValue = EncodeSimpleString(artistName);
        }


        public static explicit operator TPE1TextFrame(TP1TextFrame frame)
        {
            if (frame != null)
            {
                return new TPE1TextFrame(frame.FrameValue);
            }

            return null;
        }
    }
}
