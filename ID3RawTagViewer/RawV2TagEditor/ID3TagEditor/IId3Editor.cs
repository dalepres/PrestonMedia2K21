using System;
using System.Collections.Generic;
using System.Text;

namespace Preston.Media
{
    internal interface IID3Editor
    {
        object Id3Tag { get; set; }
        void ClearTag();
    }
}
