using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ID3AlbumArtFixer.Wizard
{
    internal interface IAlbumArtFixerJob
    {
        AlbumArtFixerJob UpdateJobFromForm(AlbumArtFixerJob job);
    }
}
