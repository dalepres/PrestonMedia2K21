namespace ID3Lib
{
    using System;

    public class TT2TextFrame : V22TextFrame
    {
        public TT2TextFrame(byte[] frameValue) : base("TT2", frameValue)
        {
        }

        public TT2TextFrame(string trackName)
        {
            base.FrameId = "TT2";
            base.FrameValue = base.EncodeSimpleString(trackName);
        }

        public TT2TextFrame(string trackName, EncodingTypes encodingType) : this(trackName)
        {
            base.SetEncoding(encodingType);
        }

        public static explicit operator TIT2TextFrame(TT2TextFrame frame)
        {
            if (frame != null)
            {
                return new TIT2TextFrame(frame.FrameValue);
            }
            return null;
        }
    }
}

