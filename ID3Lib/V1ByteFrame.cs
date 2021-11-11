namespace ID3Lib
{
    using System;

    public class V1ByteFrame : V1Frame
    {
        private byte byteValue;

        public V1ByteFrame(string frameId, byte[] frameValue) : base(frameId, frameValue)
        {
            if (frameValue.Length != 1)
            {
                throw new FormatException("FrameValue for V1ByteFrame must be a byte array containing one and only one byte.");
            }
            this.byteValue = frameValue[0];
        }

        public virtual byte ByteValue
        {
            get
            {
                return this.byteValue;
            }
        }
    }
}

