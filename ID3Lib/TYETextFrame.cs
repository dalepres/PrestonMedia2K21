namespace ID3Lib
{
    using System;

    public class TYETextFrame : V22TextFrame
    {
        public TYETextFrame(byte[] frameValue) : base("TYE", frameValue)
        {
        }

        public TYETextFrame(string year)
        {
            base.FrameId = "TYE";
            base.FrameValue = base.EncodeSimpleString(year);
        }

        public TYETextFrame(string year, EncodingTypes encodingType) : this(year)
        {
            base.SetEncoding(encodingType);
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

