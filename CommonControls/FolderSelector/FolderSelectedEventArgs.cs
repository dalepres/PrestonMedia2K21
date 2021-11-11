using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonControls
{
    public class FolderSelectedEventArgs: EventArgs
    {
        public string Path { get; set; }
        public bool SettingsSaved { get; set; }

        public FolderSelectedEventArgs()
        {
            Path = "";
            SettingsSaved = false;
        }
    }
}
