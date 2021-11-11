namespace ID3Lib
{
    using System;

    public class TIT2TextFrame : V2TextFrame
    {
        public TIT2TextFrame(byte[] frameValue) : base("TIT2", frameValue)
        {
        }

        public TIT2TextFrame(string trackName)
        {
            base.FrameId = "TIT2";
            base.FrameValue = base.EncodeSimpleString(trackName);
        }

        public TIT2TextFrame(string trackName, EncodingTypes encodingType) : this(trackName)
        {
            base.SetEncoding(encodingType);
        }
    }
}

