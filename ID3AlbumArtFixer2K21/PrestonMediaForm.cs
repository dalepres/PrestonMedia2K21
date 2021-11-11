using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonControls;
using System.IO;

namespace ID3AlbumArtFixer
{
    public class PrestonMediaForm : Form
    {
        protected bool ValidateFolderSelection(FolderSelector folderSelector, bool folderMustExist)
        {
            bool retVal = true;
            if (string.IsNullOrEmpty(folderSelector.SelectedFolder))
            {
                MessageBox.Show("You must select a folder first.");
                retVal &= false;
            }
            else if (folderMustExist && !Directory.Exists(folderSelector.SelectedFolder))
            {
                MessageBox.Show("The selected folder does not exist.");
                retVal &= false;
            }

            return retVal;
        }
    }
}
