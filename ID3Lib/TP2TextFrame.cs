namespace ID3Lib
{
    using System;

    public class TP2TextFrame : V22TextFrame
    {
        public TP2TextFrame(byte[] frameValue) : base("TP2", frameValue)
        {
        }

        public TP2TextFrame(string bandName)
        {
            base.FrameId = "TP2";
            base.FrameValue = base.EncodeSimpleString(bandName);
        }

        public TP2TextFrame(string bandName, EncodingTypes encodingType) : this(bandName)
        {
            base.SetEncoding(encodingType);
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

