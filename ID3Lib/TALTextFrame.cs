namespace ID3Lib
{
    using System;

    public class TALTextFrame : V22TextFrame
    {
        public TALTextFrame(byte[] frameValue) : base("TAL", frameValue)
        {
        }

        public TALTextFrame(string albumName)
        {
            base.FrameId = "TAL";
            base.FrameValue = base.EncodeSimpleString(albumName);
        }

        public TALTextFrame(string albumName, EncodingTypes encodingType) : this(albumName)
        {
            base.SetEncoding(encodingType);
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

