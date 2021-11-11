using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.IO;

namespace ID3Lib
{

	public delegate void ImageLoadedEventHandler(object sender, ImageLoadedEventArgs e);
    public delegate void TrackTagBackedUpEventHandler(object sender, TagBackedUpEventArgs e);
    public delegate void ImageEmbeddedEventHandler(object sender, ImageEmbeddedEventArgs e);


    public class Mp3File : IDisposable
    {
        string filePath = null;
        FileStream mp3Stream = null;

        string fileDirectory;
        string fileName;
        string fileNameWithoutExtension;
        string fileExension;

		public static event ImageLoadedEventHandler ImageLoaded;
        public static event TrackTagBackedUpEventHandler TrackTagBackedUp;
        public static event ImageEmbeddedEventHandler ImageEmbedded;
        
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
            return GetV2Tags(true);
        }


        public List<V2Tag> GetV2Tags(bool convertV22Tags)
        {
            List<V2Tag> v2Tags = new List<V2Tag>();
            if (this.mp3Stream == null)
            {
                mp3Stream = this.OpenMp3(this.filePath);
            }

            V2Tag tag;
            while ((tag = GetV2Tag(mp3Stream, convertV22Tags)) != null)
            {
                v2Tags.Add(tag);
            }

            this.Close();

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
                catch {}
            }
        }


        public FileStream OpenMp3(string path)
        {
            return File.Open(path, FileMode.Open,FileAccess.Read,FileShare.Read);
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


        private V2Tag GetV2Tag(FileStream fs, bool convertV22Tags)
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
                //return null;
                V22Tag v22Tag = new V22Tag(fs, headerBytes);
                if (convertV22Tags)
                {
                    return (V23Tag)v22Tag;
                }
                else
                {
                    return null;
                }
                //return ConvertV22TagToV23(fs, headerBytes);
				//throw new Exception("MusicDiskMaker cannot process ID3V2.2 Tags.  Convert your files to ID3V2.3 before running MusicDiskMaker.");
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
                mp3Stream = this.OpenMp3(this.filePath);
            }

            long position = mp3Stream.Position;

            if (mp3Stream.Length <= 128)
            {
                return null;
            }

            mp3Stream.Position = mp3Stream.Length - 128;
            byte[] tagBytes = new byte[128];
            mp3Stream.Read(tagBytes, 0, 128);
            string tagId = Encoding.UTF8.GetString(tagBytes, 0, 3);

            if (tagId == "TAG")
            {
                mp3Stream.Position = position;
                return new V1Tag(tagBytes);
            }
            else
            {
                mp3Stream.Position = position;
                return null;
            }
        }


        public V22Tag GetV22Tag()
        {
            V22Tag tag;
            if (this.mp3Stream == null)
            {
                mp3Stream = this.OpenMp3(this.filePath);
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

        public long RestoreTagsFromBackup(string backupFullFilePath)
        {
            Mp3File file = new Mp3File(backupFullFilePath);
            if (!File.Exists(this.filePath))
            {
                return 0;
            }

            Mp3File backup = new Mp3File(CreateTagBackupFileName(file));
            if (!File.Exists(backup.filePath))
            {
                return 0;
            }

            List<V2Tag> v2Tags = backup.GetV2Tags();
            V1Tag v1Tag = backup.GetV1Tag();

            if (v2Tags.Count == 0 && v1Tag == null)
            {
                return 0;
            }

            return this.SaveTags(v2Tags, v1Tag);
        }

        public static long SaveTagsAsBackup(string fullFilePath)
        {
            Mp3File file = new Mp3File(fullFilePath);

            List<V2Tag> v2Tags = file.GetV2Tags();
            V1Tag v1Tag = file.GetV1Tag();
            file.Close();

            if (v2Tags.Count == 0 && v1Tag == null)
            {
                return 0;
            }

            FileStream tagFile = new FileStream(CreateTagBackupFileName(file), FileMode.Create, FileAccess.Write);

            foreach(V2Tag tag in v2Tags)
            {
                tag.Write(tagFile);
            }

            if (v1Tag != null)
            {
                byte[] tagBytes = v1Tag.ToByteArray();
                tagFile.Write(tagBytes, 0, 128);
            }

            tagFile.Flush();

            long fileSize = tagFile.Length;

            tagFile.Close();
            tagFile.Dispose();

            return fileSize;
        }


        public void SplitFileAndTag()
        {
            SaveTagsAsBackup(this.filePath);
            this.Close();
            this.SaveTags(null, null);
        }

        private static string CreateMp3FileNameFromBackup(Mp3File file)
        {
            return Path.Combine(file.FileDirectory, file.FileNameWithoutExtension + ".mp3");
        }

        private static string CreateTagBackupFileName(Mp3File file)
        {
            return Path.Combine(file.FileDirectory, file.FileNameWithoutExtension + ".id3");
        }


        public static void RemoveTrackTags(string filePath)
        {
            Mp3File file = new Mp3File(filePath);
            file.SaveTags(null, null);
            file.Dispose();
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
                this.OpenMp3(filePath);
            }

            V1Tag v1Old = this.GetV1Tag();
            mp3Stream.Position = 0;
            long lastByte = mp3Stream.Length;
            if (v1Old != null)
                lastByte = mp3Stream.Length - 128;

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

            if (v2Tags != null && v2Tags.Count > 0)
            {
                foreach (V2Tag tag in v2Tags)
                {
                    WriteV2Tag(tag, fs);
                }
            }

            for (int count = 0; count < copyCount; count++)
            {
                numBytes = mp3Stream.Read(bytes,0,size);
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

            mp3Stream.Close();
            mp3Stream.Dispose();

            if (v1Tag != null)
            {
                byte[] tagBytes = v1Tag.ToByteArray();
                fs.Write(tagBytes, 0, 128);
            }

            fs.Flush();

			long fileSize = fs.Length;

            fs.Close();
            fs.Dispose();

            bool deleted = false;

            for (int tryCount = 0; tryCount < 10; tryCount++)
            {
                //try
                //{
                    File.Delete(destinationFile);
                    deleted = true;
                //}
                //catch(IOException)
                //{
                //}

                if (deleted)
                {
                    break;
                }
            }
            File.Move(destinationFile + ".tmp", destinationFile);

			return fileSize;
        }

        private void MoveToMusicStart(FileStream mp3Stream)
        {
            mp3Stream.Position = 0;
            long pos = 0;
            byte[] header = new byte[10];

            int tagSize;

            do
            {
                if ((mp3Stream.Read(header, 0, 10) != 10) || (!IsV2Header(header)))
                {
                    mp3Stream.Position = pos;
                    return;
                }

                tagSize = V2Tag.GetTagSize(header);
                mp3Stream.Position = pos + tagSize;
                pos = mp3Stream.Position;

            } while(true);
        }

        private bool IsV2Header(byte[] header)
        {
            return header.Length == 10
                && header[0] == 73
                && header[1] == 68
                && header[2] == 51
                && header[3] < 16
                && header[4] < 16
                && header[6] < 128
                && header[7] < 128
                && header[8] < 128
                && header[9] < 128;
        }

        private void WriteV2Tag(V2Tag tag, FileStream fs)
        {
            if (tag is V23Tag)
            {
                ((V23Tag)tag).Write(fs);
            }
        }

		public static void SetArtistsAlbumFolderName(string artistPath)
		{
			if (!Directory.Exists(artistPath))
			{
				return;
			}

			string[] albums = Directory.GetDirectories(artistPath);
			if (albums.Length == 0)
			{
				return;
			}

			foreach (string album in albums)
			{
				SetAlbumFolderName(album);
			}
		}

		public static void SetAlbumFolderName(string album)
		{
			if (!Directory.Exists(album))
			{
				return;
			}

			string albumName = string.Empty;
			string[] mp3Files = Directory.GetFiles(album, "*.mp3");

			if (mp3Files.Length == 0)
			{
				return;
			}

			foreach (string file in mp3Files)
			{
				Mp3File mp3File = new Mp3File(file);
				List<V2Tag> v2Tags = mp3File.GetV2Tags();
				if (v2Tags.Count == 0)
				{
					continue;
				}

				V2Tag tag = v2Tags[0];
				IEnumerator<V2Frame> ie = null;

                ie = tag.Frames["TALB"];
                while (ie.MoveNext())
                {
                    TALBTextFrame tf = (TALBTextFrame)ie.Current;
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

			if (albumName.Length > 0)
			{
				string newAlbumFolderName = CapitalizeFirstLetters(GetValidFolderName(albumName));
				try
				{
					string albumPath = Path.GetDirectoryName(album);
					string newAlbum = Path.Combine(albumPath, newAlbumFolderName);
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

		private static void RoundRobinRename(string folderName, string newFolderName)
		{
			string temp = newFolderName + "_";
			DirectoryInfo di = new DirectoryInfo(folderName);
			di.Attributes = di.Attributes & ~FileAttributes.ReadOnly;
			try
			{
				di.MoveTo(temp);


				Directory.Move(folderName, temp);
				Directory.Move(temp, newFolderName);
			}
			catch { }
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


        public static void UpdateFileTagFormatting(string mp3FilePath, string albumArtistName)
        {
            if (!File.Exists(mp3FilePath))
            {
                return;
            }

            string mp3FileFolder = Path.GetDirectoryName(mp3FilePath);

            Mp3File mp3File = new Mp3File(mp3FilePath);
            List<V2Tag> originalTags = mp3File.GetV2Tags();
            V1Tag v1Tag = mp3File.GetV1Tag();

            if (originalTags.Count == 0 && v1Tag == null)
            {
                return;
            }

            string newFileName = mp3FilePath;
            string trackNumber = "00";

            IEnumerator<V2Frame> ie = null;
            foreach (V2Tag tag in originalTags)
            {
                ie = tag.Frames["TIT2"];
                while (ie.MoveNext())
                {
                    TIT2TextFrame tf = (TIT2TextFrame)ie.Current;
                    tf.Text = CapitalizeFirstLetters(tf.Text);
                    newFileName = tf.Text + ".mp3";
                }

                ie = tag.Frames["TALB"];
                while (ie.MoveNext())
                {
                    TALBTextFrame tf = (TALBTextFrame)ie.Current;
                    tf.Text = CapitalizeFirstLetters(tf.Text);
                }

                ie = tag.Frames["TPE1"];
                while (ie.MoveNext())
                {
                    TPE1TextFrame tf = (TPE1TextFrame)ie.Current;
                    tf.Text = CapitalizeFirstLetters(tf.Text);
                }

                ie = tag.Frames["TPE2"];
				TPE2TextFrame tpf = null;
                while (ie.MoveNext())
                {
                    tpf = (TPE2TextFrame)ie.Current;
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


        public static void UpdateFolderTagFormatting(string folderName, string searchFilter, bool recursive, bool isRecursiveCall)
        {
            if (!Directory.Exists(folderName) && !isRecursiveCall)
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
        

        private static string CapitalizeFirstLetters(string text)
        {
			if (text.IndexOf(' ') > -1)
			{
				string[] words = text.Split(' ');
				for (int count = 0; count < words.Length; count++)
				{
					words[count] = CapitalizeWord(words[count]);
				}

				return string.Join(" ", words);
			}
			else
			{
				return CapitalizeWord(text);
			}
        }

		private static string CapitalizeWord(string word)
		{
			if (word.Length > 1)
			{
				return word.Substring(0, 1).ToUpper() + word.Substring(1);
			}
			else
			{
				return word.ToUpper();
			}
		}


        public static void InsertImage(string mp3FilePath, byte pictureType, string mimeType,
            string description, string imagePath)
        {
            if (File.Exists(mp3FilePath) && File.Exists(imagePath))
            {
                MemoryStream ms = Mp3File.LoadPicureFromFile(imagePath);
                InsertImage(mp3FilePath, pictureType, mimeType, description, ms, imagePath, DeletePictures.None);
            }
        }

        public static void InsertImage(string mp3FilePath, byte pictureType, string mimeType, 
            string description, MemoryStream pictureData, string imagePath, DeletePictures deletePictures)
        {
            if (!File.Exists(mp3FilePath))
            {
                return;
            }

            Mp3File mp3File = new Mp3File(mp3FilePath);
            List<V2Tag> originalTags = mp3File.GetV2Tags();

            if (originalTags.Count == 0)
            {
                return;
            }

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
                        continue;
                    }

                    if (deletePictures != DeletePictures.AllPictures)
                    {
                        APICFrame picFrame = (APICFrame)frame;

                        if (deletePictures == DeletePictures.SameTypePictures
                            && picFrame.PictureType == pictureType)
                        {
                            continue;
                        }

                        if (!picFrame.IsMatch(pictureType, description))
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

            //if (imagePath.Length > 0 && File.Exists(imagePath))
            //{
                foreach (V2Tag tag in newTags)
                {
                    APICFrame frame = new APICFrame();
                    frame.PictureType = pictureType;
                    frame.PictureData = pictureData;
                    frame.MimeType = mimeType;
                    frame.Description = description;
                    tag.Frames.Add(frame);
                }
            //  }

            mp3File.SaveTags(newTags, v1Tag);
            OnImageEmbedded(new ImageEmbeddedEventArgs(
                mp3File.filePath,
                imagePath,
                pictureData,
                ID3PictureTypes.GetPictureType(pictureType),
                mimeType,
                description));
        }

        public static MemoryStream LoadPicureFromFile(string path)
        {
            FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            MemoryStream ms = new MemoryStream((int)fs.Length);

            const int size = 4096;
            byte[] bytes = new byte[4096];
            int numBytes;
            while ((numBytes = fs.Read(bytes, 0, size)) > 0)
                ms.Write(bytes, 0, numBytes);

            fs.Close();
            fs.Dispose();

            return ms;
        }


        public static void DeleteAllImages(string mp3FilePath)
        {
            if (!File.Exists(mp3FilePath))
            {
                return;
            }

            Mp3File mp3File = new Mp3File(mp3FilePath);
            List<V2Tag> originalTags = mp3File.GetV2Tags();

            if (originalTags.Count == 0)
            {
                return;
            }

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
                        continue;
                    }
                }

                if (newV2Tag.Frames.Count > 0)
                {
                    newTags.Add(newV2Tag);
                }
            }

            mp3File.SaveTags(newTags, v1Tag);
        }


        public static void ExtractImages(string mp3FilePath, bool deletePictures)
        {
            if (!File.Exists(mp3FilePath))
            {
                return;
            }

            int fileCounter = 0;
            Mp3File mp3File = new Mp3File(mp3FilePath);
            List<V2Tag> originalTags = mp3File.GetV2Tags();

            if (originalTags.Count == 0)
            {
                return;
            }

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
                        continue;
                    }

                    APICFrame picFrame = (APICFrame)frame;
                    Image image = picFrame.GetPicture();
                    image.Save(Path.GetFileNameWithoutExtension(mp3FilePath + "_"
                        + (++fileCounter).ToString("00")));

                    if (!deletePictures)
                    {
                        newV2Tag.Frames.Add(frame);
                    }
                }

                newTags.Add(newV2Tag);
            }

            if (newTags.Count == 0)
            {
                newTags.Add(new V23Tag());
            }

            if (deletePictures)
            {
                mp3File.SaveTags(newTags, v1Tag);
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

        public static void EmbedFolderImage(string folderName, string imageFileName,
            string searchFilter, byte pictureType, string mimeType, string description)
        {
            EmbedFolderImage(folderName, imageFileName, searchFilter, pictureType, mimeType, description, true, DeletePictures.None);
        }

        public static void EmbedFolderImage(string folderName, string imageFileName,
            string searchFilter,byte pictureType, string mimeType, string description,
            bool recursive, ID3Lib.DeletePictures deletePictures)
        {
            if (!Directory.Exists(folderName))
            {
                throw new DirectoryNotFoundException("Directory " + folderName + " could not be found.");
            }

            string folderImage = Path.Combine(folderName, imageFileName);
            if (File.Exists(folderImage))
            {
                string[] files = Directory.GetFiles(folderName, searchFilter);
                if (files.Length > 0)
                {
                    MemoryStream ms = LoadPicureFromFile(Path.Combine(folderName, imageFileName));

                    ImageLoadedEventArgs e = new ImageLoadedEventArgs(imageFileName, ms);
                    OnImageLoaded(e);

                    for (int count = 0; count < files.Length; count++)
                    {
                        InsertImage(files[count], pictureType, mimeType, description, ms, imageFileName, deletePictures);
                    }
                }
            }

            if (recursive)
            {
                string[] folders = Directory.GetDirectories(folderName);
                foreach (string folder in folders)
                {
                    EmbedFolderImage(folder, imageFileName, searchFilter, pictureType, mimeType, description, recursive, deletePictures);
                }
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


        public static void OnImageEmbedded(ImageEmbeddedEventArgs e)
        {
            try
            {
                if (ImageEmbedded != null)
                {
                    ImageEmbedded(null, e);
                }
            }
            catch(Exception ex)
            {
                throw ex;
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
                catch {}
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
