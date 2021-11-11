using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using ID3Lib;

namespace ID3AlbumArtFixer
{
    internal class EmbedPictureJob
    {
        internal Size? MaxSize { get; set; }
        internal bool EnforceMaxSize { get; set; }
        internal byte Id3ImageType { get; set; }
        internal string ImageDescription { get; set; }
        internal string ImageFileName { get; set; }
        internal DeletePictures DeleteExistingImages { get; set; }
    }
}
