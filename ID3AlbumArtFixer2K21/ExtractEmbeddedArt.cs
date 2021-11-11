using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ID3Lib;

namespace ID3AlbumArtFixer
{
    public partial class ExtractEmbeddedArt : PrestonMediaForm
    {
        public ExtractEmbeddedArt()
        {
            InitializeComponent();
        }

        private void btnExtractEmbedded_Click(object sender, EventArgs e)
        {
            if (!ValidateFolderSelection(folderSelector2, true))
            {
                return;
            }

            string[] mp3Files = Directory.GetFiles(folderSelector2.SelectedFolder, "*.mp3", SearchOption.TopDirectoryOnly);

            foreach (string mp3file in mp3Files)
            {
                Mp3File file = new Mp3File(mp3file);
                Image embeddedImage = file.GetEmbeddedImage(3);
                if (embeddedImage != null)
                {
                    embeddedImage.Save(mp3file + ".jpg");
                }
            }

        }
    }
}
