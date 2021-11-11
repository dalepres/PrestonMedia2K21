using System;
using System.Collections.Generic;
using System.Text;

namespace ID3Lib
{
    public class TALTextFrame : V22TextFrame
    {
        public TALTextFrame(byte[] frameValue)
            : base("TAL", frameValue)
        {
        }

        public TALTextFrame(string albumName, EncodingTypes encodingType)
            : this(albumName)
        {
            SetEncoding(encodingType);
        }


        public TALTextFrame(string albumName)
        {
            FrameId = "TAL";
            FrameValue = EncodeSimpleString(albumName);
        }


        public static explicit operator TALBTextFrame(TALTextFrame frame)
        {
            if (frame != null)
            {
                return new TALBTextFrame(frame.FrameValue);
            }

            return null;
        }
    }
}
