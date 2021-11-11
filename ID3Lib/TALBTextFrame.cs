namespace ID3Lib
{
    using System;

    public class TALBTextFrame : V2TextFrame
    {
        public TALBTextFrame(byte[] frameValue) : base("TALB", frameValue)
        {
        }

        public TALBTextFrame(string albumName)
        {
            base.FrameId = "TALB";
            base.FrameValue = base.EncodeSimpleString(albumName);
        }

        public TALBTextFrame(string albumName, EncodingTypes encodingType) : this(albumName)
        {
            base.SetEncoding(encodingType);
        }
    }
}

