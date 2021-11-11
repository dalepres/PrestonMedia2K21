using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using WMPLib;

namespace Preston.Media
{
    internal class TagBackupJob
    {
        private string startingFolder;
        private bool recursive;
        private bool removeTags;
        private int fileCount;
        private JobTypes jobType;
        private bool includeDbInTagBackup;
        private bool backupDatabaseAttributes;
        private bool backupTags;
        private string backupFileName;
        private XmlWriter xmlWriter;
        private XPathNodeIterator iterator;
        private string originalStartingFolder;

        private MediaPlayer mediaPlayer;
        private IWMPPlaylist playlist;

        private TagBackupJob()
        {
        }

        public TagBackupJob(string startingFolder, bool recursive, bool removeTags, int fileCount, JobTypes jobType, string backupFileName)
        {
            this.startingFolder = startingFolder;
            this.recursive = recursive;
            this.removeTags = removeTags;
            this.fileCount = fileCount;
            this.jobType = jobType;
            this.backupFileName = backupFileName;
        }

        public string StartingFolder
        {
            get { return startingFolder; }
            set { startingFolder = value; }
        }

        public string OriginalStartingFolder
        {
            get { return originalStartingFolder; }
            set { originalStartingFolder = value; }
        }

        public bool Recursive
        {
            get { return recursive; }
            set { recursive = value; }
        }


        public bool RemoveTags
        {
            get { return removeTags; }
            set { removeTags = value; }
        }

        public int FileCount
        {
            get { return fileCount; }
            set { fileCount = value; }
        }

        public JobTypes JobType
        {
            get { return jobType; }
            set { jobType = value; }
        }

        public bool IncludeDbInTagBackup
        {
            get { return includeDbInTagBackup; }
            set { includeDbInTagBackup = value; }
        }

        public bool BackupDatabaseAttributes
        {
            get { return backupDatabaseAttributes; }
            set { backupDatabaseAttributes = value; }
        }

        public XmlWriter XmlWriter
        {
            get { return xmlWriter; }
            set { xmlWriter = value; }
        }

        public XPathNodeIterator Iterator
        {
            get { return iterator; }
            set { iterator = value; }
        }

        public bool BackupTags
        {
            get { return backupTags; }
            set { backupTags = value; }
        }

        public string BackupFileName
        {
            get { return backupFileName; }
            set { backupFileName = value; }
        }

        public MediaPlayer MediaPlayer
        {
            get { return mediaPlayer; }
            set { mediaPlayer = value; }
        }

        public WMPLib.IWMPPlaylist Playlist
        {
            get { return playlist; }
            set { playlist = value; }
        }
    }
}
