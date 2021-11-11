namespace ID3Lib
{
    using System;

    public class TPE1TextFrame : V2TextFrame
    {
        public TPE1TextFrame(byte[] frameValue) : base("TPE1", frameValue)
        {
        }

        public TPE1TextFrame(string artistName)
        {
            base.FrameId = "TPE1";
            base.FrameValue = base.EncodeSimpleString(artistName);
        }

        public TPE1TextFrame(string artistName, EncodingTypes encodingType) : this(artistName)
        {
            base.SetEncoding(encodingType);
        }
    }
}

