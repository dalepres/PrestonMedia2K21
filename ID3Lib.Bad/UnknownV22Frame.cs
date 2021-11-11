using System;
using System.Collections.Generic;
using System.Text;

namespace ID3Lib
{
    public class UnknownV22Frame : V22Frame
    {
        public UnknownV22Frame(string frameId, byte[] frameValue)
            : base(frameId, frameValue)
        {
        }
    }
}
