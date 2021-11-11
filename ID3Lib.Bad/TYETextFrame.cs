using System;
using System.Collections.Generic;
using System.Text;

namespace ID3Lib
{
    public class TYETextFrame : V22TextFrame
    {
        public TYETextFrame(byte[] frameValue)
            : base("TYE", frameValue)
        {
        }


        public TYETextFrame(string year, EncodingTypes encodingType)
            : this(year)
        {
            SetEncoding(encodingType);
        }


        public TYETextFrame(string year)
        {
            FrameId = "TYE";
            FrameValue = EncodeSimpleString(year);
        }


        public static explicit operator TYERTextFrame(TYETextFrame frame)
        {
            if (frame != null)
            {
                return new TYERTextFrame(frame.FrameValue);
            }

            return null;
        }
    }
}
