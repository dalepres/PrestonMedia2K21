using System;
using System.Collections.Generic;
using System.Text;

namespace ID3Lib
{
    public class V1Frame
    {
        private string frameId;
        private byte[] frameValue;

        protected V1Frame(string frameId, byte[] frameValue)
        {
            this.frameId = frameId;
            this.frameValue = frameValue;
        }

        private V1Frame()
        {
        }

        public string FrameId
        {
            get { return frameId; }
        }

        public byte[] FrameValue
        {
            get { return frameValue; }
        }

        public override string ToString()
        {
            return this.frameId;
        }
    }
}
