using System;
using System.Collections.Generic;
using System.Text;

namespace ID3Lib
{
    public class V1TextFrame : V1Frame
    {
        string text;

        public V1TextFrame(string frameId, byte[] frameValue)
            : base(frameId, frameValue)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            text = encoding.GetString(FrameValue);
        }


        public virtual string Text
        {
            get
            {
                return text;
            }
        }
    }
}
