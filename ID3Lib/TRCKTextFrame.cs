namespace ID3Lib
{
    using System;

    public class TRCKTextFrame : V2TextFrame
    {
        public TRCKTextFrame(byte[] frameValue) : base("TRCK", frameValue)
        {
        }

        public TRCKTextFrame(string trackNumber)
        {
            base.FrameId = "TRCK";
            base.FrameValue = base.EncodeSimpleString(trackNumber);
        }

        public TRCKTextFrame(string trackNumber, EncodingTypes encodingType) : this(trackNumber)
        {
            base.SetEncoding(encodingType);
        }
    }
}

