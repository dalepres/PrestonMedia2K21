using System;
using System.Collections.Generic;
using System.Text;

namespace ID3Lib
{
    public abstract class V2TextFrame : V2Frame
    {
        public V2TextFrame()
        {
        }


        public V2TextFrame(string frameId, byte[] frameValue)
            : base(frameId, frameValue)
        {
            SetTextEncoding();
        }


        public virtual void SetTextEncoding()
        {
            SetEncoding((EncodingTypes)FrameValue[0]);
        }

        public virtual string Text
        {
            get
            {
                byte terminatorLength = 0;
                byte start = 1;
                byte b = GetEncodingByte();
                switch (b)
                {
                    case 0:
                    case 3:
                        if (FrameValue[FrameValue.Length - 1] == 0)
                        {
                            terminatorLength = 1;
                        }
                        return base.Encoding.GetString(FrameValue, 1, FrameValue.Length - (start + terminatorLength));
                    case 1:
                        return GetUTF16LEValue();
                    case 2:
                        if (FrameValue[FrameValue.Length - 1] == 0 && FrameValue[FrameValue.Length - 2] == 0)
                        {
                            terminatorLength = 2;
                        }
                        return base.Encoding.GetString(FrameValue, 1, FrameValue.Length - (start + terminatorLength));
                    default:
                        throw new Exception("Cannot return Text value.  Frame.Encoding is invalid.");
                }
            }

            set
            {
                FrameValue = EncodeSimpleString(value);
            }
        }

        protected string GetUTF16LEValue()
        {
            byte start = 1;
            byte terminatorLength = 0;

            if (FrameValue[1] == 0xFF && FrameValue[2] == 0xFE)
                start = 3;

            if (FrameValue[FrameValue.Length - 1] == 0 && FrameValue[FrameValue.Length - 2] == 0)
            {
                terminatorLength = 2;
            }
            return base.Encoding.GetString(FrameValue, start, FrameValue.Length - (start + terminatorLength));
        }
    }
}
