namespace ID3Lib
{
    using System;

    public class V1Frame
    {
        private string frameId;
        private byte[] frameValue;

        private V1Frame()
        {
        }

        protected V1Frame(string frameId, byte[] frameValue)
        {
            this.frameId = frameId;
            this.frameValue = frameValue;
        }

        public override string ToString()
        {
            return this.frameId;
        }

        public string FrameId
        {
            get
            {
                return this.frameId;
            }
        }

        public byte[] FrameValue
        {
            get
            {
                return this.frameValue;
            }
        }
    }
}

