using System;
using System.Collections.Generic;
using System.Text;

namespace ID3Lib
{
    public class TPATextFrame : V22TextFrame
    {
        public TPATextFrame(byte[] frameValue)
            : base("TPA", frameValue)
        {
        }


        public TPATextFrame(string bandName, EncodingTypes encodingType)
            : this(bandName)
        {
            SetEncoding(encodingType);
        }


        public TPATextFrame(string bandName)
        {
            FrameId = "TPA";
            FrameValue = EncodeSimpleString(bandName);
        }


        public static explicit operator TPOSTextFrame(TPATextFrame frame)
        {
            if (frame != null)
            {
                return new TPOSTextFrame(frame.FrameValue);
            }

            return null;
        }
    }
}
