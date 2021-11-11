namespace ID3Lib
{
    using System;

    public class TPATextFrame : V22TextFrame
    {
        public TPATextFrame(byte[] frameValue) : base("TPA", frameValue)
        {
        }

        public TPATextFrame(string bandName)
        {
            base.FrameId = "TPA";
            base.FrameValue = base.EncodeSimpleString(bandName);
        }

        public TPATextFrame(string bandName, EncodingTypes encodingType) : this(bandName)
        {
            base.SetEncoding(encodingType);
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

