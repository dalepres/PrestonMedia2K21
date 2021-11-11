namespace ID3Lib
{
    using System;

    public class TagBackedUpEventArgs : EventArgs
    {
        private string backupPath;
        private string trackPath;

        public TagBackedUpEventArgs(string trackPath, string backupPath)
        {
            this.trackPath = trackPath;
            this.backupPath = backupPath;
        }

        public string BackupPath
        {
            get
            {
                return this.backupPath;
            }
            set
            {
                this.backupPath = value;
            }
        }

        public string TrackPath
        {
            get
            {
                return this.trackPath;
            }
            set
            {
                this.trackPath = value;
            }
        }
    }
}

