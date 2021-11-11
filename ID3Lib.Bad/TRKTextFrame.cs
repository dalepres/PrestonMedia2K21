using System;
using System.Collections.Generic;
using System.Text;

namespace ID3Lib
{
    public class TRKTextFrame : V22TextFrame
    {
        public TRKTextFrame(byte[] frameValue)
            : base("TRK", frameValue)
        {
        }


        public TRKTextFrame(string trackNumber, EncodingTypes encodingType)
            : this(trackNumber)
        {
            SetEncoding(encodingType);
        }


        public TRKTextFrame(string trackNumber)
        {
            FrameId = "TRK";
            FrameValue = EncodeSimpleString(trackNumber);
        }

        public static explicit operator TRCKTextFrame(TRKTextFrame frame)
        {
            if (frame != null)
            {
                return new TRCKTextFrame(frame.FrameValue);
            }

            return null;
        }
    }
}
