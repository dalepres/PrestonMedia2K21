namespace ID3Lib
{
    using System;

    public class TPE2TextFrame : V2TextFrame
    {
        public TPE2TextFrame(byte[] frameValue) : base("TPE2", frameValue)
        {
        }

        public TPE2TextFrame(string bandName)
        {
            base.FrameId = "TPE2";
            base.FrameValue = base.EncodeSimpleString(bandName);
        }

        public TPE2TextFrame(string bandName, EncodingTypes encodingType) : this(bandName)
        {
            base.SetEncoding(encodingType);
        }
    }
}

