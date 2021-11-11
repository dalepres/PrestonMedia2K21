using System;
using System.Collections.Generic;
using System.Text;

namespace ID3Lib
{
    public class UnknownV2Frame : V2Frame
    {
        public UnknownV2Frame(string frameId, byte[] frameValue)
            : base(frameId, frameValue)
        {
        }
    }
}
