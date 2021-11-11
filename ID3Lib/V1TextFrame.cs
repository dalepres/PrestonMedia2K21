namespace ID3Lib
{
    using System;
    using System.Text;

    public class V1TextFrame : V1Frame
    {
        private string text;

        public V1TextFrame(string frameId, byte[] frameValue) : base(frameId, frameValue)
        {
            this.text = new UTF8Encoding().GetString(base.FrameValue);
        }

        public virtual string Text
        {
            get
            {
                return this.text;
            }
        }
    }
}

