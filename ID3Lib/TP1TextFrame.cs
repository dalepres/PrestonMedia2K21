namespace ID3Lib
{
    using System;

    public class TP1TextFrame : V22TextFrame
    {
        public TP1TextFrame(byte[] frameValue) : base("TP1", frameValue)
        {
        }

        public TP1TextFrame(string artistName)
        {
            base.FrameId = "TP1";
            base.FrameValue = base.EncodeSimpleString(artistName);
        }

        public TP1TextFrame(string artistName, EncodingTypes encodingType) : this(artistName)
        {
            base.SetEncoding(encodingType);
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

