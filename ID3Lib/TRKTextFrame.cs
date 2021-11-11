namespace ID3Lib
{
    using System;

    public class TRKTextFrame : V22TextFrame
    {
        public TRKTextFrame(byte[] frameValue) : base("TRK", frameValue)
        {
        }

        public TRKTextFrame(string trackNumber)
        {
            base.FrameId = "TRK";
            base.FrameValue = base.EncodeSimpleString(trackNumber);
        }

        public TRKTextFrame(string trackNumber, EncodingTypes encodingType) : this(trackNumber)
        {
            base.SetEncoding(encodingType);
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

