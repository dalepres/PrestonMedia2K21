using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ID3AlbumArtFixer
{
    internal class AlbumArtFixerJob
    {
        internal string FolderName { get; set; }
        internal bool IncludeSubfolders { get; set; }
        internal Size MaxSize { get; set; }
        internal string FullControlAccount { get; set; }
        internal string ReadOnlyAccount { get; set; }
        internal int FolderCount { get; set; }
        internal bool IsStarting { get; set; }
        internal AlbumArtSource AlbumArtSource { get; set; }
        internal string AlbumArtSourceFileName { get; set; }
        internal bool CreateAlbumArt { get; set; }
        internal bool EmbedAlbumArt { get; set; }
        internal bool SetAlbumArtSecurity { get; set; }
        internal EmbedPictureJob EmbedPictureJob { get; set; }

        /// <summary>
        /// Image quality in percent.  Determines the compression ratio for jpg images.
        /// </summary>
        internal int ImageQuality { get; set; }

        internal AlbumArtFixerJob()
        {
        }

        internal AlbumArtFixerJob(
            string folderName, 
            bool includeSubfolders, 
            Size maxSize, 
            string fullControlAccount, 
            string readOnlyAccount,
            int folderCount)
        {
            FolderName = folderName;
            IncludeSubfolders = includeSubfolders;
            MaxSize = maxSize;
            FullControlAccount = fullControlAccount;
            ReadOnlyAccount = readOnlyAccount;
            FolderCount = folderCount;
            IsStarting = true;
        }
    }
   
    public enum AlbumArtSource
    {
        FileName,
        LargestImage,
        FileNameThenLargest
    }
}
