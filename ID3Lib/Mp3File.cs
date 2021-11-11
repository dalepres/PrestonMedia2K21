namespace ID3Lib
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
using System.Drawing;
    using System.Drawing.Imaging;

    public class Mp3File : IDisposable
    {
        private string fileDirectory;
        private string fileExension;
        private string fileName;
        private string fileNameWithoutExtension;
        private string filePath = null;
        private const int MAJOR_VERSION = 3;
        private FileStream mp3Stream = null;

        public static  event ImageEmbeddedEventHandler ImageEmbedded;

        public static  event ImageLoadedEventHandler ImageLoaded;

        public static  event TrackTagBackedUpEventHandler TrackTagBackedUp;

        public Mp3File(string filePath)
        {
            this.fileDirectory = Path.GetDirectoryName(filePath);
            this.fileName = Path.GetFileName(filePath);
            this.fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
            this.fileExension = Path.GetExtension(filePath);
            this.filePath = filePath;
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
                    SaveTagsAsBackup(files[count]);
                    if (removeTrackTag)
                    {
                        RemoveTrackTags(files[count]);
                    }
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

        private static string CapitalizeFirstLetters(string text)
        {
            if (text.IndexOf(' ') > -1)
            {
                string[] words = text.Split(new char[] { ' ' });
                for (int count = 0; count < words.Length; count++)
                {
                    words[count] = CapitalizeWord(words[count]);
                }
                return string.Join(" ", words);
            }
            return CapitalizeWord(text);
        }

        private static string CapitalizeWord(string word)
        {
            if (word.Length > 1)
            {
                return (word.Substring(0, 1).ToUpper() + word.Substring(1));
            }
            return word.ToUpper();
        }

        public void Close()
        {
            if (this.mp3Stream != null)
            {
                try
                {
                    this.mp3Stream.Close();
                    this.mp3Stream.Dispose();
                    this.mp3Stream = null;
                }
                catch
                {
                }
            }
        }

        private static string CreateMp3FileNameFromBackup(Mp3File file)
        {
            return Path.Combine(file.FileDirectory, file.FileNameWithoutExtension + ".mp3");
        }

        private static string CreateTagBackupFileName(Mp3File file)
        {
            return Path.Combine(file.FileDirectory, file.FileNameWithoutExtension + ".id3");
        }

        public void Dispose()
        {
            if (this.mp3Stream != null)
            {
                try
                {
                    this.mp3Stream.Dispose();
                }
                catch
                {
                }
            }
        }

        public static void EmbedFolderImage(string folderName, string imageFileName, string searchFilter, byte pictureType, string mimeType, string description)
        {
            EmbedFolderImage(folderName, imageFileName, searchFilter, pictureType, mimeType, description, false);
        }

        public static void EmbedFolderImage(string folderName, string imageFileName, string searchFilter, byte pictureType, string mimeType, string description, bool recursive)
        {
            EmbedFolderImage(folderName, imageFileName, searchFilter, pictureType, mimeType, description, false, null);
        }

        public static void EmbedFolderImage(string folderName, string imageFileName, string searchFilter, byte pictureType, string mimeType, string description, bool recursive, Size? maxSize)
        {

            EmbedFolderImage(folderName, imageFileName, searchFilter, pictureType, mimeType, description, recursive, maxSize, DeletePictures.None);
        }

        public static void EmbedFolderImage(string folderName, string imageFileName, string searchFilter, byte pictureType, string mimeType, string description, bool recursive, Size? maxSize, DeletePictures deleteMode)
        {
            if (!Directory.Exists(folderName))
            {
                throw new DirectoryNotFoundException("Directory " + folderName + " could not be found.");
            }

            if (File.Exists(Path.Combine(folderName, imageFileName)))
            {
                string[] files = Directory.GetFiles(folderName, searchFilter);
                if (files.Length > 0)
                {

                    MemoryStream ms;

                    if (maxSize == null)
                    {
                        ms = LoadPicureFromFile(Path.Combine(folderName, imageFileName));
                    }
                    else
                    {
                        ms = new MemoryStream();
                        Image img = Image.FromFile(Path.Combine(folderName, imageFileName));
                        APICFrame.ResizePicture(img, maxSize.Value).Save(ms, ImageFrame.GetImageFormatFromMimeType(mimeType));
                    }

                    ImageLoadedEventArgs e = new ImageLoadedEventArgs(imageFileName, ms);
                    //OnImageLoaded(e);
                    for (int count = 0; count < files.Length; count++)
                    {
                        InsertImage(files[count], pictureType, mimeType, description, imageFileName, deleteMode, ms);
                    }
                    ms.Dispose();
                }
            }

            if (recursive)
            {
                string[] folders = Directory.GetDirectories(folderName);
                foreach (string folder in folders)
                {
                    EmbedFolderImage(folder, imageFileName, searchFilter, pictureType, mimeType, description, recursive, maxSize, deleteMode);
                }
            }
        }

        public V1Tag GetV1Tag()
        {
            if (this.mp3Stream == null)
            {
                this.mp3Stream = this.OpenMp3(this.filePath);
            }
            long position = this.mp3Stream.Position;
            if (this.mp3Stream.Length > 0x80L)
            {
                this.mp3Stream.Position = this.mp3Stream.Length - 0x80L;
                byte[] tagBytes = new byte[0x80];
                this.mp3Stream.Read(tagBytes, 0, 0x80);
                if (Encoding.UTF8.GetString(tagBytes, 0, 3) == "TAG")
                {
                    this.mp3Stream.Position = position;
                    return new V1Tag(tagBytes);
                }
                this.mp3Stream.Position = position;
            }
            return null;
        }

        /// <summary>
        /// Retrieves the first image found matching pictureType.
        /// </summary>
        /// <param name="pictureType">byte value for the picture type.</param>
        /// <returns>System.Drawing.Image</returns>
        public Image GetEmbeddedImage(byte pictureType)
        {
            return GetFirstImage(pictureType).GetPicture();
        }

        /// <summary>
        /// Retrieves the first APIC frame matching pictureType.
        /// </summary>
        /// <param name="pictureType">byte value for the picture type.</param>
        /// <returns>APICFrame</returns>
        public APICFrame GetFirstImage(byte pictureType)
        {
            List<V2Tag> tags = this.GetV2Tags(true);

            foreach (V2Tag tag in tags)
            {
                foreach (Frame frame in tag.Frames)
                {
                    if (frame is APICFrame && ((APICFrame)frame).PictureType == pictureType)
                    {
                        return (APICFrame)frame;
                    }
                }
            }

            return null;
        }

        public List<APICFrame> GetAllImages(byte pictureType)
        {
            List<APICFrame> images = new List<APICFrame>();
            List<V2Tag> tags = this.GetV2Tags(true);

            foreach (V2Tag tag in tags)
            {
                foreach (Frame frame in tag.Frames)
                {
                    if (frame is APICFrame && ((APICFrame)frame).PictureType == pictureType)
                    {
                        images.Add((APICFrame)frame);
                    }
                }
            }

            return images;
        }

        public List<APICFrame> GetAllImages()
        {
            List<APICFrame> images = new List<APICFrame>();
            List<V2Tag> tags = this.GetV2Tags(true);

            foreach (V2Tag tag in tags)
            {
                foreach (Frame frame in tag.Frames)
                {
                    if (frame is APICFrame)
                    {
                        images.Add((APICFrame)frame);
                    }
                }
            }

            return images;
        }

        public V22Tag GetV22Tag()
        {
            V22Tag tag;
            if (this.mp3Stream == null)
            {
                this.mp3Stream = this.OpenMp3(this.filePath);
            }
            long position = this.mp3Stream.Position;
            byte[] headerBytes = new byte[10];
            if (((this.mp3Stream.Read(headerBytes, 0, 10) == 10) && (Encoding.UTF8.GetString(headerBytes, 0, 3) == "ID3")) && (headerBytes[3] == 2))
            {
                tag = new V22Tag(this.mp3Stream, headerBytes);
            }
            else
            {
                tag = null;
            }
            this.mp3Stream.Position = position;
            return tag;
        }

        private V2Tag GetV2Tag(FileStream fs, bool convertV22Tags)
        {
            long position = fs.Position;
            byte[] headerBytes = new byte[10];
            if ((fs.Read(headerBytes, 0, 10) != 10) || (Encoding.UTF8.GetString(headerBytes, 0, 3) != "ID3"))
            {
                fs.Position = position;
                return null;
            }
            if (headerBytes[3] == 2)
            {
                V22Tag v22Tag = new V22Tag(fs, headerBytes);
                if (convertV22Tags)
                {
                    return (V23Tag) v22Tag;
                }
                return null;
            }
            if (headerBytes[3] == 3)
            {
                return new V23Tag(fs, headerBytes);
            }
            fs.Position = position;
            return null;
        }

        public List<V2Tag> GetV2Tags()
        {
            return this.GetV2Tags(true);
        }

        public List<V2Tag> GetV2Tags(bool convertV22Tags)
        {
            V2Tag tag;
            List<V2Tag> v2Tags = new List<V2Tag>();
            if (this.mp3Stream == null)
            {
                this.mp3Stream = this.OpenMp3(this.filePath);
            }

            while ((tag = this.GetV2Tag(this.mp3Stream, convertV22Tags)) != null)
            {
                v2Tags.Add(tag);
            }
            this.Close();
            return v2Tags;
        }

        private static string GetValidFolderName(string pathName)
        {
            char[] chars = Path.GetInvalidPathChars();
            foreach (char ch in chars)
            {
                pathName = pathName.Replace(Convert.ToString(ch), "");
            }
            chars = Path.GetInvalidFileNameChars();
            foreach (char ch in chars)
            {
                pathName = pathName.Replace(Convert.ToString(ch), "");
            }
            return pathName;
        }

        public static void InsertImage(string mp3FilePath, byte pictureType, string mimeType, string description, string imagePath)
        {
            if (File.Exists(mp3FilePath) && File.Exists(imagePath))
            {
                MemoryStream ms = LoadPicureFromFile(imagePath);
                InsertImage(mp3FilePath, pictureType, mimeType, description, imagePath, DeletePictures.None, ms);
            }
        }

        public static void InsertImage(string mp3FilePath, byte pictureType, string mimeType, string description, string imagePath, DeletePictures deleteMode)
        {
            if (File.Exists(mp3FilePath) && File.Exists(imagePath))
            {
                MemoryStream ms = LoadPicureFromFile(imagePath);
                InsertImage(mp3FilePath, pictureType, mimeType, description, imagePath, deleteMode, ms);
            }
        }


        public static void InsertImage(string mp3FilePath, byte pictureType, string mimeType, string description, string imagePath, DeletePictures deleteMode, MemoryStream pictureData)
        {
            if (File.Exists(mp3FilePath))
            {
                Mp3File mp3File = new Mp3File(mp3FilePath);
                List<V2Tag> originalTags = mp3File.GetV2Tags();
                if (originalTags.Count != 0)
                {
                    V1Tag v1Tag = mp3File.GetV1Tag();
                    List<V2Tag> newTags = new List<V2Tag>();
                    foreach (V2Tag tag in originalTags)
                    {
                        V2Tag newV2Tag = null;
                        if (tag is V23Tag)
                        {
                            newV2Tag = new V23Tag();
                        }
                        foreach (V2Frame frame in tag.Frames)
                        {
                            if (!(frame is APICFrame))
                            {
                                newV2Tag.Frames.Add(frame);
                            }
                            else
                            {
                                APICFrame picFrame = (APICFrame)frame;
                                if (PictureIsKeeper(pictureType, description, deleteMode, picFrame))
                                {
                                    newV2Tag.Frames.Add(frame);
                                }
                            }
                        }
                        newTags.Add(newV2Tag);
                    }
                    if (newTags.Count == 0)
                    {
                        newTags.Add(new V23Tag());
                    }
                    foreach (V2Tag tag in newTags)
                    {
                        APICFrame frame = new APICFrame();
                        frame.PictureType = pictureType;
                        frame.PictureData = pictureData;
                        frame.MimeType = mimeType;
                        frame.Description = description;
                        tag.Frames.Add(frame);
                    }
                    mp3File.SaveTags(newTags, v1Tag);
                    //OnImageEmbedded(new ImageEmbeddedEventArgs(mp3File.filePath, imagePath, pictureData, ID3PictureTypes.GetPictureType(pictureType), mimeType, description));
                }
            }
        }

        private static bool PictureIsKeeper(byte pictureType, string description, DeletePictures deleteMode, APICFrame picFrame)
        {
            if (deleteMode == DeletePictures.None)
            {

                if ((picFrame.PictureType == 1 && pictureType == 1)
                    || (picFrame.PictureType == 2 && pictureType == 2)
                    || picFrame.IsMatch(pictureType, description))
                {
                    return false;
                }
            }

            else if (deleteMode == DeletePictures.SameTypePictures && picFrame.PictureType == pictureType)
            {
                return false;
            }

            else if (deleteMode == DeletePictures.AllPictures)
            {
                return false;
            }

            return true;
        }

        private bool IsV2Header(byte[] header)
        {
            return ((((((header.Length == 10) && (header[0] == 0x49)) && ((header[1] == 0x44) && (header[2] == 0x33))) && (((header[3] < 0x10) && (header[4] < 0x10)) && ((header[6] < 0x80) && (header[7] < 0x80)))) && (header[8] < 0x80)) && (header[9] < 0x80));
        }

        public static MemoryStream LoadPicureFromFile(string path)
        {
            int numBytes;
            FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            MemoryStream ms = new MemoryStream((int) fs.Length);
            byte[] bytes = new byte[0x1000];
            while ((numBytes = fs.Read(bytes, 0, 0x1000)) > 0)
            {
                ms.Write(bytes, 0, numBytes);
            }
            fs.Close();
            fs.Dispose();
            return ms;
        }

        private void MoveToMusicStart(FileStream mp3Stream)
        {
            mp3Stream.Position = 0L;
            long pos = 0L;
            byte[] header = new byte[10];
        Label_0015:
            if (!((mp3Stream.Read(header, 0, 10) == 10) && this.IsV2Header(header)))
            {
                mp3Stream.Position = pos;
            }
            else
            {
                int tagSize = V2Tag.GetTagSize(header);
                mp3Stream.Position = pos + tagSize;
                pos = mp3Stream.Position;
                goto Label_0015;
            }
        }

        public static void OnImageEmbedded(ImageEmbeddedEventArgs e)
        {
            try
            {
                if (ImageEmbedded != null)
                {
                    ImageEmbedded(null, e);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void OnImageLoaded(ImageLoadedEventArgs e)
        {
            try
            {
                if (ImageLoaded != null)
                {
                    ImageLoaded(null, e);
                }
            }
            catch
            {
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

        public FileStream OpenMp3(string path)
        {
            return File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        public static void RemoveTrackTags(string filePath)
        {
            Mp3File file = new Mp3File(filePath);
            file.SaveTags(null, null);
            file.Dispose();
        }

        public long RestoreTagsFromBackup(string backupFullFilePath)
        {
            Mp3File file = new Mp3File(backupFullFilePath);
            if (!File.Exists(this.filePath))
            {
                return 0L;
            }
            Mp3File backup = new Mp3File(CreateTagBackupFileName(file));
            if (!File.Exists(backup.filePath))
            {
                return 0L;
            }
            List<V2Tag> v2Tags = backup.GetV2Tags();
            V1Tag v1Tag = backup.GetV1Tag();
            if ((v2Tags.Count == 0) && (v1Tag == null))
            {
                return 0L;
            }
            return this.SaveTags(v2Tags, v1Tag);
        }

        private static void RoundRobinRename(string folderName, string newFolderName)
        {
            string temp = newFolderName + "_";
            DirectoryInfo di = new DirectoryInfo(folderName);
            di.Attributes &= ~FileAttributes.ReadOnly;
            try
            {
                di.MoveTo(temp);
                Directory.Move(folderName, temp);
                Directory.Move(temp, newFolderName);
            }
            catch
            {
            }
        }

        public long SaveTags(List<V2Tag> v2Tags, V1Tag v1Tag)
        {
            return this.SaveTags(v2Tags, v1Tag, Path.GetDirectoryName(this.filePath), Path.GetFileName(this.filePath));
        }

        public long SaveTags(List<V2Tag> v2Tags, V1Tag v1Tag, string destinationPath, string newFIleName)
        {
            if (this.mp3Stream == null)
            {
                this.OpenMp3(this.filePath);
            }
            V1Tag v1Old = this.GetV1Tag();
            this.mp3Stream.Position = 0L;
            long lastByte = this.mp3Stream.Length;
            if (v1Old != null)
            {
                lastByte = this.mp3Stream.Length - 0x80L;
            }
            this.MoveToMusicStart(this.mp3Stream);
            long firstByte = this.mp3Stream.Position;
            long lengthToCopy = lastByte - firstByte;
            int copyCount = (int) (lengthToCopy / 0x1000L);
            int lastCopy = (int) (lengthToCopy % 0x1000L);
            this.mp3Stream.Position = firstByte;
            byte[] bytes = new byte[0x1000];
            string destinationFile = Path.Combine(destinationPath, newFIleName);
            FileStream fs = File.Open(destinationFile + ".tmp", FileMode.Create);
            if ((v2Tags != null) && (v2Tags.Count > 0))
            {
                foreach (V2Tag tag in v2Tags)
                {
                    this.WriteV2Tag(tag, fs);
                }
            }
            for (int count = 0; count < copyCount; count++)
            {
                if (this.mp3Stream.Read(bytes, 0, 0x1000) > 0)
                {
                    fs.Write(bytes, 0, 0x1000);
                }
            }
            if (lastCopy > 0)
            {
                int numBytes = this.mp3Stream.Read(bytes, 0, lastCopy);
                if (numBytes > 0)
                {
                    fs.Write(bytes, 0, numBytes);
                }
            }
            this.mp3Stream.Close();
            this.mp3Stream.Dispose();
            if (v1Tag != null)
            {
                byte[] tagBytes = v1Tag.ToByteArray();
                fs.Write(tagBytes, 0, 0x80);
            }
            fs.Flush();
            long fileSize = fs.Length;
            fs.Close();
            fs.Dispose();
            for (int tryCount = 0; tryCount < 10; tryCount++)
            {
                File.Delete(destinationFile);
                if (File.Exists(destinationFile))
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
            File.Move(destinationFile + ".tmp", destinationFile);
            return fileSize;
        }

        public static long SaveTagsAsBackup(string fullFilePath)
        {
            Mp3File file = new Mp3File(fullFilePath);
            List<V2Tag> v2Tags = file.GetV2Tags();
            V1Tag v1Tag = file.GetV1Tag();
            file.Close();
            if ((v2Tags.Count == 0) && (v1Tag == null))
            {
                return 0L;
            }
            FileStream tagFile = new FileStream(CreateTagBackupFileName(file), FileMode.Create, FileAccess.Write);
            foreach (V2Tag tag in v2Tags)
            {
                tag.Write(tagFile);
            }
            if (v1Tag != null)
            {
                byte[] tagBytes = v1Tag.ToByteArray();
                tagFile.Write(tagBytes, 0, 0x80);
            }
            tagFile.Flush();
            long fileSize = tagFile.Length;
            tagFile.Close();
            tagFile.Dispose();
            return fileSize;
        }

        public static void SetAlbumFolderName(string album)
        {
            if (Directory.Exists(album))
            {
                string albumName = string.Empty;
                string[] mp3Files = Directory.GetFiles(album, "*.mp3");
                if (mp3Files.Length != 0)
                {
                    foreach (string file in mp3Files)
                    {
                        Mp3File mp3File = new Mp3File(file);
                        List<V2Tag> v2Tags = mp3File.GetV2Tags();
                        if (v2Tags.Count != 0)
                        {
                            V2Tag tag = v2Tags[0];
                            IEnumerator<V2Frame> ie = null;
                            ie = tag.Frames["TALB"];
                            while (ie.MoveNext())
                            {
                                TALBTextFrame tf = (TALBTextFrame) ie.Current;
                                albumName = tf.Text;
                                break;
                            }
                            if (albumName.Length > 0)
                            {
                                break;
                            }
                            mp3File.Close();
                            mp3File.Dispose();
                        }
                    }
                    if (albumName.Length > 0)
                    {
                        string newAlbumFolderName = CapitalizeFirstLetters(GetValidFolderName(albumName));
                        try
                        {
                            string newAlbum = Path.Combine(Path.GetDirectoryName(album), newAlbumFolderName);
                            if (album != newAlbum)
                            {
                                RoundRobinRename(album, newAlbum);
                            }
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }
            }
        }

        public static void SetArtistsAlbumFolderName(string artistPath)
        {
            if (Directory.Exists(artistPath))
            {
                string[] albums = Directory.GetDirectories(artistPath);
                if (albums.Length != 0)
                {
                    foreach (string album in albums)
                    {
                        SetAlbumFolderName(album);
                    }
                }
            }
        }

        public void SplitFileAndTag()
        {
            SaveTagsAsBackup(this.filePath);
            this.Close();
            this.SaveTags(null, null);
        }

        public static void UpdateFileTagFormatting(string mp3FilePath, string albumArtistName)
        {
            if (File.Exists(mp3FilePath))
            {
                string mp3FileFolder = Path.GetDirectoryName(mp3FilePath);
                Mp3File mp3File = new Mp3File(mp3FilePath);
                List<V2Tag> originalTags = mp3File.GetV2Tags();
                V1Tag v1Tag = mp3File.GetV1Tag();
                if ((originalTags.Count != 0) || (v1Tag != null))
                {
                    string newFileName = mp3FilePath;
                    string trackNumber = "00";
                    IEnumerator<V2Frame> ie = null;
                    foreach (V2Tag tag in originalTags)
                    {
                        ie = tag.Frames["TIT2"];
                        while (ie.MoveNext())
                        {
                            TIT2TextFrame tf = (TIT2TextFrame) ie.Current;
                            tf.Text = CapitalizeFirstLetters(tf.Text);
                            newFileName = tf.Text + ".mp3";
                        }
                        ie = tag.Frames["TALB"];
                        while (ie.MoveNext())
                        {
                            TALBTextFrame tf = (TALBTextFrame) ie.Current;
                            tf.Text = CapitalizeFirstLetters(tf.Text);
                        }
                        ie = tag.Frames["TPE1"];
                        while (ie.MoveNext())
                        {
                            TPE1TextFrame tf = (TPE1TextFrame) ie.Current;
                            tf.Text = CapitalizeFirstLetters(tf.Text);
                        }
                        ie = tag.Frames["TPE2"];
                        TPE2TextFrame tpf = null;
                        while (ie.MoveNext())
                        {
                            tpf = (TPE2TextFrame) ie.Current;
                            tpf.Text = CapitalizeFirstLetters(albumArtistName);
                        }
                        if (tpf == null)
                        {
                            tpf = new TPE2TextFrame(albumArtistName);
                            tag.Frames.Add(tpf);
                        }
                        trackNumber = Frame.GetTrackNumber(tag).ToString("0#");
                        if (v1Tag != null)
                        {
                            v1Tag.Album = CapitalizeFirstLetters(v1Tag.Album);
                            v1Tag.Artist = CapitalizeFirstLetters(v1Tag.Artist);
                            v1Tag.Title = CapitalizeFirstLetters(v1Tag.Title);
                        }
                        mp3File.SaveTags(originalTags, v1Tag);
                        mp3File.Close();
                        mp3File.Dispose();
                    }
                    newFileName = trackNumber + " " + newFileName;
                    char[] chars = Path.GetInvalidFileNameChars();
                    foreach (char ch in chars)
                    {
                        newFileName = newFileName.Replace(Convert.ToString(ch), "");
                    }
                    try
                    {
                        File.Move(mp3FilePath, Path.Combine(mp3FileFolder, newFileName));
                    }
                    catch
                    {
                    }
                }
            }
        }

        public static void UpdateFolderTagFormatting(string folderName, string searchFilter, bool recursive, bool isRecursiveCall)
        {
            if (!(Directory.Exists(folderName) || isRecursiveCall))
            {
                throw new DirectoryNotFoundException("Directory " + folderName + " could not be found.");
            }
            string newFolderPath = Path.GetDirectoryName(folderName);
            string newFileName = CapitalizeFirstLetters(Path.GetFileName(folderName));
            string newFolderName = Path.Combine(newFolderPath, newFileName);
            if (folderName != newFolderName)
            {
                RoundRobinRename(folderName, newFolderName);
            }
            string[] files = Directory.GetFiles(folderName, searchFilter);
            string albumArtistName = CapitalizeFirstLetters(Directory.GetParent(folderName).Name);
            if (files.Length > 0)
            {
                for (int count = 0; count < files.Length; count++)
                {
                    UpdateFileTagFormatting(files[count], albumArtistName);
                }
            }
            if (recursive)
            {
                string[] folders = Directory.GetDirectories(folderName);
                foreach (string folder in folders)
                {
                    UpdateFolderTagFormatting(folder, searchFilter, true, true);
                }
            }
        }

        private void WriteV2Tag(V2Tag tag, FileStream fs)
        {
            if (tag is V23Tag)
            {
                ((V23Tag) tag).Write(fs);
            }
        }

        public string FileDirectory
        {
            get
            {
                return this.fileDirectory;
            }
        }

        public string FileExtension
        {
            get
            {
                return this.fileExension;
            }
        }

        public string FileName
        {
            get
            {
                return this.fileName;
            }
        }

        public string FileNameWithoutExtension
        {
            get
            {
                return this.fileNameWithoutExtension;
            }
        }

        public enum TagTypes
        {
            All,
            ID3V1,
            ID3V2
        }
    }
}

