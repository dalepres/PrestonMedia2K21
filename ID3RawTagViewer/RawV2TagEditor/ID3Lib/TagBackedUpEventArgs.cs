using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

namespace ID3Lib
{
	public class TagBackedUpEventArgs : EventArgs
	{
		private string trackPath;
		private string backupPath;

		public TagBackedUpEventArgs(string trackPath, string backupPath)
		{
			this.trackPath = trackPath;
            this.backupPath = backupPath;
		}

		public string TrackPath
		{
			get { return trackPath; }
			set { trackPath = value; }
		}


		public string BackupPath
		{
			get { return backupPath; }
			set { backupPath = value; }
		}
	}
}
