using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using WMPLib;

namespace Preston.Media
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

        const int V1_TAG_LENGTH = 128;

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

        public static void Close(FileStream fileStream)
        {
            fileStream.Close();
            fileStream.Dispose();
            fileStream = null;
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

            byte[] v2Tags = GetTagsRaw(id3Stream);
            byte[] v1Tag = GetV1TagRaw(id3Stream);
            Mp3File.Close(id3Stream);

            if (v2Tags.Length == 0 && v1Tag.Length == 0)
            {
                return 0;
            }

            return Mp3File.SaveTags(v2Tags, v1Tag, mp3Path);

        }

        private static byte[] GetSizeBytes(byte[] headerBytes)
        {
            return new byte[] {
                headerBytes[6],
                headerBytes[7],
                headerBytes[8],
                headerBytes[9]};
        }

        public static byte[] GetTagsRaw(FileStream fs)
        {
            MemoryStream ms = new MemoryStream();

            fs.Position = 0;
            byte[] headerBytes = GetHeaderBytes(fs);

            while (headerBytes != null)
            {
                byte[] tagBytes;
                int tagLength;

                int length = ReadInt28(GetSizeBytes(headerBytes));
                tagBytes = new byte[length];
                tagLength = fs.Read(tagBytes, 0, length);
                if (tagLength == length)
                {
                    ms.Write(headerBytes, 0, headerBytes.Length);
                    ms.Write(tagBytes, 0, tagLength);
                }

                headerBytes = GetHeaderBytes(fs);
            }

            return ms.ToArray();
        }

        internal static int ReadInt28(byte[] bytes)
        {
            //if ((bytes[0] & 0x80) != 0 || (bytes[1] & 0x80) != 0 || (bytes[2] & 0x80) != 0 || (bytes[3] & 0x80) != 0)
            //    throw new TagsException("Found invalid syncsafe integer");

            int result = (bytes[0] << 21) | (bytes[1] << 14) | (bytes[2] << 7) | bytes[3];
            return result;
        }

        //public static long BackupID3Tags(string fullFilePath)
        //{
        //    return BackupID3Tags(fullFilePath, true);
        //}

        //public static long BackupID3Tags(string fullFilePath, bool includeDbInTagBackup)
        //{
        //    FileStream mp3Stream = Mp3File.Open(fullFilePath);

        //    byte[] v2Tags = GetTagsRaw(mp3Stream);
        //    byte[] v1Tag = GetV1TagRaw(mp3Stream);

        //    Mp3File.Close(mp3Stream);

        //    MemoryStream dbStream;
        //    byte[] dbData = null;

        //    if (includeDbInTagBackup)
        //    {
        //        XmlDocument xDoc = BackupLibraryDataForTrack(fullFilePath);
        //        dbStream = new MemoryStream();
        //        xDoc.Save(dbStream);
        //        dbData = new byte[dbStream.Length];
        //        dbStream.Position = 0;
        //        dbStream.Read(dbData, 0, (int)dbStream.Length);
        //    }

        //    if (v2Tags.Length == 0 && v1Tag.Length == 0 && dbData == null)
        //    {
        //        return 0;
        //    }

        //    FileStream tagFile = new FileStream(Path.ChangeExtension(fullFilePath, ".id3"), FileMode.Create, FileAccess.Write);

        //    if (v2Tags.Length > 0)
        //    {
        //        tagFile.Write(v2Tags, 0, v2Tags.Length);
        //    }

        //    if (dbData != null)
        //    {
        //        tagFile.Write(dbData, 0, dbData.Length);
        //    }

        //    if (v1Tag.Length > 0)
        //    {
        //        tagFile.Write(v1Tag, 0, v1Tag.Length);
        //    }

        //    tagFile.Flush();
        //    long fileSize = tagFile.Length;
        //    tagFile.Close();
        //    tagFile.Dispose();

        //    return fileSize;
        //}

        //internal static XmlDocument BackupLibraryDataForTrack(string fullFilePath)
        //{
        //    XmlDocument xDoc = new XmlDocument();
        //    XmlNode docNode = xDoc.CreateNode(XmlNodeType.Element, "LibraryData", "");
        //    xDoc.AppendChild(docNode);

        //    BackupLibraryData(fullFilePath, xDoc, docNode);

        //    return xDoc;
        //}

        internal static void BackupLibraryData(MediaPlayer mediaPlayer, IWMPPlaylist playlist, string fullFilePath, XmlWriter writer) // XmlDocument xDoc)
        {
            playlist = mediaPlayer.Player.mediaCollection.getByAttribute(
                "SourceURL", 
                fullFilePath);
            if (playlist.count == 1)
            {
                IWMPMedia media = playlist.get_Item(0);
                if (media.getItemInfo("MediaType") == "audio")
                {
                    mediaPlayer.BackupLibraryData(playlist.get_Item(0), writer); // xDoc);
                }
            }

            return;            
        }

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
            Mp3File.SaveTags(null, null, filePath);
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
                if ((mp3Stream.Read(header, 0, 10) != 10) || (!IsId3Header(header)))
                {
                    mp3Stream.Position = pos;
                    return;
                }

                tagSize = GetTagSize(header);
                mp3Stream.Position = pos + tagSize;
                pos = mp3Stream.Position;

            } while (true);
        }

        public static int GetTagSize(byte[] headerBytes)
        {
            byte[] sizeBytes = new byte[4];
            Array.Copy(headerBytes, 6, sizeBytes, 0, 4);
            int tagSize = ReadInt28(sizeBytes);
            return tagSize;
        }

        protected static byte[] GetHeaderBytes(FileStream fs)
        {
            long position = fs.Position;
            byte[] bytes = new byte[10];
            fs.Read(bytes, 0, 10);

            if (!IsId3Header(bytes))
            {
                fs.Position = position;
                return null;
            }

            return bytes;
        }

        public static bool IsId3Header(byte[] headerBytes)
        {
            return headerBytes.Length == 10
                && StartsWithID3(headerBytes)
                && headerBytes[3] < 16
                && headerBytes[4] < 16
                && headerBytes[6] < 128
                && headerBytes[7] < 128
                && headerBytes[8] < 128
                && headerBytes[9] < 128;
        }


        public static bool StartsWithID3(byte[] bytes)
        {
            return bytes[0] == 73
                && bytes[1] == 68
                && bytes[2] == 51;
        }

        //public static void BackupFolderId3Tags(string folderName, string searchFilter, bool recursive)
        //{
        //    BackupFolderId3Tags(folderName, searchFilter, recursive, false);
        //}

        //public static void BackupFolderId3Tags(string folderName, string searchFilter, bool recursive, bool removeTrackTag)
        //{
        //    if (!Directory.Exists(folderName))
        //    {
        //        throw new DirectoryNotFoundException("Directory " + folderName + " could not be found.");
        //    }

        //    string[] files = Directory.GetFiles(folderName, searchFilter);
        //    if (files.Length > 0)
        //    {
        //        for (int count = 0; count < files.Length; count++)
        //        {
        //            BackupID3Tags(files[count]);

        //            if (removeTrackTag)
        //            {
        //                RemoveTrackTags(files[count]);
        //            }

        //            OnTagBackedUp(null, new TagBackedUpEventArgs(files[count], files[count] + ".id3"));
        //        }
        //    }

        //    if (recursive)
        //    {
        //        string[] folders = Directory.GetDirectories(folderName);
        //        foreach (string folder in folders)
        //        {
        //            BackupFolderId3Tags(folder, searchFilter, recursive, removeTrackTag);
        //        }
        //    }
        //}


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


        public static byte[] GetV1TagRaw(FileStream fs)
        {
            if (fs.Length > V1_TAG_LENGTH)
            {
                long position = fs.Position;
                fs.Position = fs.Length - V1_TAG_LENGTH;
                byte[] tagBytes = new byte[V1_TAG_LENGTH];
                int tagLength = fs.Read(tagBytes, 0, V1_TAG_LENGTH);
                fs.Position = position;

                if (tagLength == V1_TAG_LENGTH && IsId3V1Tag(tagBytes))
                {
                    return tagBytes;
                }
            }

            return new byte[0];
        }
        private static bool IsId3V1Tag(byte[] tagBytes)
        {
            return tagBytes.Length == 128 && StartsWithID3(tagBytes);
        }

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

    }
}
