namespace ID3Lib
{
    using System;

    public class TPOSTextFrame : V2TextFrame
    {
        public TPOSTextFrame(byte[] frameValue) : base("TPOS", frameValue)
        {
        }

        public TPOSTextFrame(string bandName)
        {
            base.FrameId = "TPOS";
            base.FrameValue = base.EncodeSimpleString(bandName);
        }

        public TPOSTextFrame(string bandName, EncodingTypes encodingType) : this(bandName)
        {
            base.SetEncoding(encodingType);
        }
    }
}

