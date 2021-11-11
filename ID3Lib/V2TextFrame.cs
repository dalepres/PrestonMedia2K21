namespace ID3Lib
{
    using System;

    public abstract class V2TextFrame : V2Frame
    {
        public V2TextFrame()
        {
        }

        public V2TextFrame(string frameId, byte[] frameValue) : base(frameId, frameValue)
        {
            this.SetTextEncoding();
        }

        protected string GetUTF16LEValue()
        {
            byte start = 1;
            byte terminatorLength = 0;
            if ((base.FrameValue[1] == 0xff) && (base.FrameValue[2] == 0xfe))
            {
                start = 3;
            }
            if ((base.FrameValue[base.FrameValue.Length - 1] == 0) && (base.FrameValue[base.FrameValue.Length - 2] == 0))
            {
                terminatorLength = 2;
            }
            return base.Encoding.GetString(base.FrameValue, start, base.FrameValue.Length - (start + terminatorLength));
        }

        public virtual void SetTextEncoding()
        {
            base.SetEncoding((EncodingTypes) base.FrameValue[0]);
        }

        public virtual string Text
        {
            get
            {
                byte terminatorLength = 0;
                byte start = 1;
                switch (base.GetEncodingByte())
                {
                    case 0:
                    case 3:
                        if (base.FrameValue[base.FrameValue.Length - 1] == 0)
                        {
                            terminatorLength = 1;
                        }
                        return base.Encoding.GetString(base.FrameValue, 1, base.FrameValue.Length - (start + terminatorLength));

                    case 1:
                        return this.GetUTF16LEValue();

                    case 2:
                        if ((base.FrameValue[base.FrameValue.Length - 1] == 0) && (base.FrameValue[base.FrameValue.Length - 2] == 0))
                        {
                            terminatorLength = 2;
                        }
                        return base.Encoding.GetString(base.FrameValue, 1, base.FrameValue.Length - (start + terminatorLength));
                }
                throw new Exception("Cannot return Text value.  Frame.Encoding is invalid.");
            }
            set
            {
                base.FrameValue = base.EncodeSimpleString(value);
            }
        }
    }
}

