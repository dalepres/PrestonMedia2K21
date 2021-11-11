using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ID3Lib
{
    public delegate void TrackTagBackedUpEventHandler(object sender, TagBackedUpEventArgs e);

    public class Mp3File : IDisposable
    {
        string filePath = null;
        FileStream mp3Stream = null;

        string fileDirectory;
        string fileName;
        string fileNameWithoutExtension;
        string fileExension;

        public static event TrackTagBackedUpEventHandler TrackTagBackedUp;

        private const int MAJOR_VERSION = 3;

        public Mp3File(string filePath)
        {
            fileDirectory = Path.GetDirectoryName(filePath);
            fileName = Path.GetFileName(filePath);
            fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
            fileExension = Path.GetExtension(filePath);

            this.filePath = filePath;
        }

        #region Properties

        public string FileDirectory
        {
            get { return fileDirectory; }
        }

        public string FileName
        {
            get { return fileName; }
        }

        public string FileNameWithoutExtension
        {
            get { return fileNameWithoutExtension; }
        }

        public string FileExtension
        {
            get { return fileExension; }
        }


        #endregion Properties
        /// <summary>
        /// Gets all V2 tags, converting V2.2 tags to V2.3
        /// </summary>
        /// <returns>A generic list of all V2 tags</returns>
        public List<V2Tag> GetV2Tags()
        {
            return GetV2Tags(false);
        }


        public List<V2Tag> GetV2Tags(bool convertV22Tags)
        {
            List<V2Tag> v2Tags = new List<V2Tag>();

            if (this.mp3Stream == null)
            {
                mp3Stream = this.Open();
            }

            V2Tag tag;
            while ((tag = GetV2Tag(mp3Stream)) != null)
            {
                v2Tags.Add(tag);
            }

            return v2Tags;
        }


        public void Close()
        {
            if (mp3Stream != null)
            {
                try
                {
                    mp3Stream.Close();
                    mp3Stream.Dispose();
                    mp3Stream = null;
                }
                catch { }
            }
        }


        public static void Close(FileStream fileStream)
        {
            fileStream.Close();
            fileStream.Dispose();
            fileStream = null;
        }


        public FileStream Open()
        {
            try
            {
                this.mp3Stream = File.Open(this.filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                return mp3Stream;
            }
            catch (Exception e)
            {
                throw(e);
            }
        }


        public static FileStream Open(string filePath)
        {
            try
            {
                return File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (Exception e)
            {
                throw (e);
            }
        }


        private V2Tag GetV2Tag(FileStream fs)
        {
            long position = fs.Position;
            byte[] headerBytes = new byte[10];

            if (fs.Read(headerBytes, 0, 10) != 10
                || Encoding.UTF8.GetString(headerBytes, 0, 3) != "ID3")
            {
                fs.Position = position;
                return null;
            }

            if (headerBytes[MAJOR_VERSION] == 2)
            {
                return null;
                //return new V22Tag(fs, headerBytes);
            }

            if (headerBytes[MAJOR_VERSION] == 3)
            {
                return new V23Tag(fs, headerBytes);
            }
            else
            {
                fs.Position = position;
                return null;
            }
        }

        public V1Tag GetV1Tag()
        {
            if (this.mp3Stream == null)
            {
                mp3Stream = this.Open();
            }

            long position = mp3Stream.Position;

            V1Tag v1Tag = null;
            if (mp3Stream.Length > 128)
            {
                mp3Stream.Position = mp3Stream.Length - 128;
                byte[] tagBytes = new byte[128];
                mp3Stream.Read(tagBytes, 0, 128);
                string tagId = Encoding.UTF8.GetString(tagBytes, 0, 3);

                if (tagId == "TAG")
                {
                    v1Tag = new V1Tag(tagBytes);
                }

            }

            this.Close();

            return v1Tag;

        }


        public V22Tag GetV22Tag()
        {
            V22Tag tag;
            if (this.mp3Stream == null)
            {
                mp3Stream = this.Open();
            }

            long position = mp3Stream.Position;

            byte[] headerBytes = new byte[10];

            if (mp3Stream.Read(headerBytes, 0, 10) == 10
                && Encoding.UTF8.GetString(headerBytes, 0, 3) == "ID3"
                && headerBytes[MAJOR_VERSION] == 2)
            {
                tag = new V22Tag(mp3Stream, headerBytes);
            }
            else
            {
                tag = null;
            }

            mp3Stream.Position = position;
            return tag;
        }

        public static long RestoreTagsFromBackup(string mp3Path)
        {
            if (!File.Exists(mp3Path))
            {
                return 0;
            }

            string id3Path = CreateTagBackupFileName(mp3Path);
            if (!File.Exists(id3Path))
            {
                return 0;
            }

            FileStream id3Stream = Mp3File.Open(id3Path);
            //Mp3File id3File = new Mp3File(id3Path);

            byte[] v2Tags = V2Tag.GetTagsRaw(id3Stream);
            byte[] v1Tag = V1Tag.GetTagRaw(id3Stream);
            Mp3File.Close(id3Stream);

            if (v2Tags.Length == 0 && v1Tag.Length == 0)
            {
                return 0;
            }

            return Mp3File.SaveTags(v2Tags, v1Tag, mp3Path);

        }

        public static long BackupID3Tags(string fullFilePath)
        {
            FileStream mp3Stream = Mp3File.Open(fullFilePath);

            byte[] v2Tags = V2Tag.GetTagsRaw(mp3Stream);
            byte[] v1Tag = V1Tag.GetTagRaw(mp3Stream);

            Mp3File.Close(mp3Stream);

            if (v2Tags.Length == 0 && v1Tag.Length == 0)
            {
                return 0;
            }

            FileStream tagFile = new FileStream(Path.ChangeExtension(fullFilePath, ".id3"), FileMode.Create, FileAccess.Write);

            tagFile.Write(v2Tags, 0, v2Tags.Length);
            tagFile.Write(v1Tag, 0, v1Tag.Length);

            tagFile.Flush();
            long fileSize = tagFile.Length;
            tagFile.Close();
            tagFile.Dispose();

            return fileSize;
        }

        //public void SplitFileAndTag()
        //{
        //    SaveTagsAsBackup(this.filePath);
        //    this.Close();
        //    this.SaveTags(null, null);
        //}

        private static string CreateMp3FileNameFromBackup(string filePath)
        {
            return Path.ChangeExtension(filePath, ".mp3");
        }

        private static string CreateTagBackupFileName(string filePath)
        {
            return Path.ChangeExtension(filePath, ".id3");
        }

        public static void RemoveTrackTags(string filePath)
        {
            Mp3File file = new Mp3File(filePath);
            file.SaveTags(null, null);
            file.Dispose();
        }


        private static long SaveTags(byte[] v2Tags, byte[] v1Tag, string mp3Path)
        {
            if (!File.Exists(mp3Path))
            {
                throw new Exception("File \"" + mp3Path + "\" does not exist.");
            }

            FileStream mp3Stream = Mp3File.Open(mp3Path);

            long lastByte = mp3Stream.Length;

            if (HasV1Tag(mp3Stream))
            {
                lastByte = mp3Stream.Length - 128;
            }

            MoveToMusicStart(mp3Stream);

            long firstByte = mp3Stream.Position;
            long lengthToCopy = lastByte - firstByte;
            int copyCount = (int)(lengthToCopy / 4096L);
            int lastCopy = (int)(lengthToCopy % 4096L);

            mp3Stream.Position = firstByte;

            const int size = 4096;
            byte[] bytes = new byte[size];
            int numBytes;

            FileStream fs = File.Open(mp3Path + ".tmp", FileMode.Create);
            if (v2Tags != null)
            {
                fs.Write(v2Tags, 0, v2Tags.Length);
            }

            for (int count = 0; count < copyCount; count++)
            {
                numBytes = mp3Stream.Read(bytes, 0, size);
                if (numBytes > 0)
                {
                    fs.Write(bytes, 0, size);
                }
            }

            if (lastCopy > 0)
            {
                numBytes = mp3Stream.Read(bytes, 0, lastCopy);
                if (numBytes > 0)
                {
                    fs.Write(bytes, 0, numBytes);
                }
            }

            if (v1Tag != null)
            {
                fs.Write(v1Tag, 0, v1Tag.Length);
            }

            fs.Flush();
            long fileSize = fs.Length;
            fs.Close();
            fs.Dispose();
            mp3Stream.Close();
            mp3Stream.Dispose();
            File.Delete(mp3Path);
            File.Move(mp3Path + ".tmp", mp3Path);

            return fileSize;
        }

        public long SaveTags(List<V2Tag> v2Tags, V1Tag v1Tag)
        {
            return SaveTags(v2Tags, v1Tag, Path.GetDirectoryName(filePath), Path.GetFileName(filePath));
        }

        public long SaveTags(List<V2Tag> v2Tags, V1Tag v1Tag, string destinationPath, string newFIleName)
        {
            // TODO:    If possible, check to see if the size of the tag in memory is equal
            //          to or smaller than the size of the tag in the file.  If so, write 
            //          only the tag bytes instead of the entire file to speed up operation.

            if (this.mp3Stream == null)
            {
                mp3Stream = this.Open();
            }

            long lastByte = mp3Stream.Length;

            if (HasV1Tag(mp3Stream))
            {
                lastByte = mp3Stream.Length - 128;
            }

            MoveToMusicStart(mp3Stream);

            long firstByte = mp3Stream.Position;
            long lengthToCopy = lastByte - firstByte;
            int copyCount = (int)(lengthToCopy / 4096L);
            int lastCopy = (int)(lengthToCopy % 4096L);

            mp3Stream.Position = firstByte;

            const int size = 4096;
            byte[] bytes = new byte[size];
            int numBytes;

            string destinationFile = Path.Combine(destinationPath, newFIleName);
            FileStream fs = File.Open(destinationFile + ".tmp", FileMode.Create);
            WriteV2Tags(v2Tags, fs);
            numBytes = WriteMp3Data(copyCount, lastCopy, size, bytes, fs);
            WriteV1Tag(v1Tag, fs);

            fs.Flush();
            long fileSize = fs.Length;
            fs.Close();
            fs.Dispose();
            mp3Stream.Close();
            mp3Stream.Dispose();
            File.Delete(destinationFile);
            File.Move(destinationFile + ".tmp", destinationFile);

            return fileSize;
        }

        private static void WriteV1TagRaw(V1Tag v1Tag, FileStream fs)
        {
            if (v1Tag != null)
            {
                byte[] tagBytes = v1Tag.TagBytes;
                fs.Write(tagBytes, 0, 128);
            }
        }

        private static void WriteV1Tag(V1Tag v1Tag, FileStream fs)
        {
            if (v1Tag != null)
            {
                byte[] tagBytes = v1Tag.ToByteArray();
                fs.Write(tagBytes, 0, 128);
            }
        }


        private int WriteMp3Data(int copyCount, int lastCopy, int size, byte[] bytes, FileStream fs)
        {
            int numBytes = 0;
            for (int count = 0; count < copyCount; count++)
            {
                numBytes = mp3Stream.Read(bytes, 0, size);
                if (numBytes > 0)
                {
                    fs.Write(bytes, 0, size);
                }
            }

            if (lastCopy > 0)
            {
                numBytes = mp3Stream.Read(bytes, 0, lastCopy);
                if (numBytes > 0)
                {
                    fs.Write(bytes, 0, numBytes);
                }
            }
            return numBytes;
        }

        private void WriteV2Tags(List<V2Tag> v2Tags, FileStream fs)
        {
            if (v2Tags != null && v2Tags.Count > 0)
            {
                foreach (V2Tag tag in v2Tags)
                {
                    WriteV2Tag(tag, fs);
                }
            }
        }

        private static bool HasV1Tag(FileStream mp3Stream)
        {
            long position = mp3Stream.Position;

            if (mp3Stream != null && mp3Stream.Length > 128)
            {
                mp3Stream.Position = mp3Stream.Length - 128;
                byte[] tagBytes = new byte[3];
                mp3Stream.Read(tagBytes, 0, 3);
                mp3Stream.Position = position;
                string tagId = Encoding.UTF8.GetString(tagBytes, 0, 3);

                return (tagId == "TAG");
            }
            else
            {
                return false;
            }
        }

        private static void MoveToMusicStart(FileStream mp3Stream)
        {
            mp3Stream.Position = 0;
            long pos = 0;
            byte[] header = new byte[10];

            int tagSize;

            do
            {
                if ((mp3Stream.Read(header, 0, 10) != 10) || (!ID3Tag.IsId3Header(header)))
                {
                    mp3Stream.Position = pos;
                    return;
                }

                tagSize = V2Tag.GetTagSize(header);
                mp3Stream.Position = pos + tagSize;
                pos = mp3Stream.Position;

            } while (true);
        }

        private void WriteV2Tag(V2Tag tag, FileStream fs)
        {
            if (tag is V23Tag)
            {
                ((V23Tag)tag).Write(fs);
            }

            if (tag is V22Tag)
            {
                ((V22Tag)tag).Write(fs);
            }
        }

        public static void BackupFolderId3Tags(string folderName, string searchFilter, bool recursive)
        {
            BackupFolderId3Tags(folderName, searchFilter, recursive, false);
        }

        public static void BackupFolderId3Tags(string folderName, string searchFilter, bool recursive, bool removeTrackTag)
        {
            if (!Directory.Exists(folderName))
            {
                throw new DirectoryNotFoundException("Directory " + folderName + " could not be found.");
            }

            string[] files = Directory.GetFiles(folderName, searchFilter);
            if (files.Length > 0)
            {
                for (int count = 0; count < files.Length; count++)
                {
                    BackupID3Tags(files[count]);

                    if (removeTrackTag)
                    {
                        RemoveTrackTags(files[count]);
                    }

                    OnTagBackedUp(null, new TagBackedUpEventArgs(files[count], files[count] + ".id3"));
                }
            }

            if (recursive)
            {
                string[] folders = Directory.GetDirectories(folderName);
                foreach (string folder in folders)
                {
                    BackupFolderId3Tags(folder, searchFilter, recursive, removeTrackTag);
                }
            }
        }

        public static void OnTagBackedUp(Mp3File file, TagBackedUpEventArgs e)
        {
            try
            {
                if (TrackTagBackedUp != null)
                {
                    TrackTagBackedUp(file, e);
                }
            }
            catch
            {
            }
        }

        //private V2Tag ConvertV22TagToV23(FileStream fs, byte[] headerBytes)
        //{
        //    V23Tag tag = new V23Tag();

        //    //int flags = headerBytes[5];

        //    byte[] sizeBytes = new byte[4];
        //    Array.Copy(headerBytes, 6, sizeBytes, 0, 4);
        //    int tagSize = Utilities.ReadInt28(sizeBytes);

        //    long nextPossibleTagPosition = fs.Position + tagSize;
        //    long tagEndPosition = nextPossibleTagPosition - 11;

        //    //ParseFlags(headerBytes[5]);

        //    //if (HasExtendedHeader)
        //    //{
        //    //    byte[] exHeader = PopExtendedHeader(fs);
        //    //}

        //    bool frameAdded = true;
        //    while ((fs.Position < tagEndPosition) && frameAdded)
        //    {
        //        frameAdded = AddFrame(fs);
        //    }

        //    fs.Position = nextPossibleTagPosition;

        //    return tag;
        //}


        #region IDisposable Members

        public void Dispose()
        {
            if (this.mp3Stream != null)
            {
                try
                {
                    this.mp3Stream.Dispose();
                }
                catch { }
            }
        }

        #endregion


        public enum TagTypes
        {
            All,
            ID3V1,
            ID3V2
        }
    }
}
