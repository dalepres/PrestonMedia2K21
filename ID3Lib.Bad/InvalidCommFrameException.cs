using System;
using System.Collections.Generic;
using System.Text;

namespace ID3Lib
{
    public class InvalidCommFrameException : Exception
    {
        public InvalidCommFrameException(string message)
            :base(message)
        {

        }
    }
}
