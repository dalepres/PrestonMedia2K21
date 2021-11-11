namespace ID3Lib
{
    using System;

    public class TYERTextFrame : V2TextFrame
    {
        public TYERTextFrame(byte[] frameValue) : base("TYER", frameValue)
        {
        }

        public TYERTextFrame(string year)
        {
            base.FrameId = "TYER";
            base.FrameValue = base.EncodeSimpleString(year);
        }

        public TYERTextFrame(string year, EncodingTypes encodingType) : this(year)
        {
            base.SetEncoding(encodingType);
        }
    }
}

