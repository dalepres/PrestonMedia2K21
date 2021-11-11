using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID3Lib
{
    public class LooksLikeAppleCommFrameException : Exception
    {
        public LooksLikeAppleCommFrameException()
            : base("Apple iTunes creates invalid COMM or COM frames and this frame has the pattern of the invalide Apple frames.")
        {
        }
    }
}
