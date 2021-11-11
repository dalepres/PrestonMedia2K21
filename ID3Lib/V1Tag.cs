namespace ID3Lib
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class V1Tag : ID3Tag, ICloneable
    {
        private string album;
        private const int ALBUM_MAX_LENGTH = 30;
        private const int ALBUM_START = 0x3f;
        private string artist;
        private const int ARTIST_MAX_LENGTH = 30;
        private const int ARTIST_START = 0x21;
        private string comment;
        private const int COMMENT_START = 0x61;
        private const int COMMENT_TRACK_SEPERATOR_BYTE_POSITION = 0x7d;
        private List<V1Frame> frames;
        private byte genre;
        private const int GENRE_BYTE_POSITION = 0x7f;
        private const int GENRE_MAX_LENGTH = 1;
        private const int GENRE_START = 0x7f;
        private int majorVersion;
        private string tag;
        private const int TAG_LENGTH = 0x80;
        private const int TAG_MAX_LENGTH = 3;
        private const int TAG_START = 0;
        private string title;
        private const int TITLE_MAX_LENGTH = 30;
        private const int TITLE_START = 3;
        private const int TRACK_NUMBER_BYTE_POSITION = 0x7e;
        private byte trackNumber;
        private const int V1_0_COMMENT_MAX_LENGTH = 30;
        private const int V1_1_COMMENT_MAX_LENGTH = 30;
        private string year;
        private const int YEAR_MAX_LENGTH = 4;
        private const int YEAR_START = 0x5d;

        public V1Tag()
        {
            this.genre = 0xff;
            this.majorVersion = 0;
        }

        public V1Tag(byte[] tagBytes)
        {
            this.genre = 0xff;
            this.majorVersion = 0;
            this.ParseTagBytes(tagBytes);
            this.frames = this.CreateFrames();
        }

        public V1Tag(int majorVersion)
        {
            this.genre = 0xff;
            this.majorVersion = 0;
            this.majorVersion = majorVersion;
        }

        public object Clone()
        {
            return base.MemberwiseClone();
        }

        internal virtual byte[] CommentToByteArray()
        {
            if (this.majorVersion == 0)
            {
                return this.StringToByteArray(this.comment, 30);
            }
            return this.StringToByteArray(this.comment, 30);
        }

        public List<V1Frame> CreateFrames()
        {
            List<V1Frame> f = new List<V1Frame>();
            UTF8Encoding e = new UTF8Encoding();
            f.Add(new V1TextFrame("Title", e.GetBytes(this.title)));
            f.Add(new V1TextFrame("Artist", e.GetBytes(this.artist)));
            f.Add(new V1TextFrame("Album", e.GetBytes(this.album)));
            f.Add(new V1TextFrame("Year", e.GetBytes(this.year)));
            f.Add(new V1TextFrame("Comment", e.GetBytes(this.comment)));
            f.Add(new V1ByteFrame("Genre", new byte[] { this.genre }));
            if (this.majorVersion == 1)
            {
                f.Add(new V1ByteFrame("Track", new byte[] { this.TrackNumber }));
            }
            return f;
        }

        public static V1Tag FromTag(object tag)
        {
            return FromTag(tag, 1);
        }

        public static V1Tag FromTag(object tag, int majorVersion)
        {
            if (tag is V22Tag)
            {
                return FromV22Tag((V22Tag) tag, majorVersion);
            }
            if (tag is V23Tag)
            {
                return FromV23Tag((V23Tag) tag, majorVersion);
            }
            if (tag is V1Tag)
            {
                V1Tag newTag = (V1Tag) ((V1Tag) tag).Clone();
                newTag.MajorVersion = majorVersion;
                return newTag;
            }
            return new V1Tag(majorVersion);
        }

        private static V1Tag FromV22Tag(V22Tag v22Tag, int majorVersion)
        {
            V1Tag newTag = new V1Tag(majorVersion);
            newTag.Album = Frame.GetV22TextFrameValue(v22Tag, "TAL");
            newTag.Artist = Frame.GetV22TextFrameValue(v22Tag, "TP1");
            newTag.Comment = GetFirstV22CommentFrameValue(v22Tag);
            newTag.Title = Frame.GetV22TextFrameValue(v22Tag, "TT2");
            newTag.Year = Frame.GetV22TextFrameValue(v22Tag, "TYE");
            if (newTag.MajorVersion == 1)
            {
                newTag.TrackNumber = Frame.GetTrackNumber(v22Tag);
            }
            newTag.Genre = ID3Genres.GetGenreItem(Frame.GetV22TextFrameValue(v22Tag, "TCO")).GenreId;
            return newTag;
        }

        private static V1Tag FromV23Tag(V23Tag v23Tag, int majorVersion)
        {
            V1Tag newTag = new V1Tag(majorVersion);
            newTag.Album = Frame.GetV23TextFrameValue(v23Tag, "TALB");
            newTag.Artist = Frame.GetV23TextFrameValue(v23Tag, "TPE1");
            newTag.Comment = GetFirstV23CommentFrameValue(v23Tag);
            newTag.Title = Frame.GetV23TextFrameValue(v23Tag, "TIT2");
            newTag.Year = Frame.GetV23TextFrameValue(v23Tag, "TYER");
            if (newTag.MajorVersion == 1)
            {
                newTag.TrackNumber = Frame.GetTrackNumber(v23Tag);
            }
            newTag.Genre = ID3Genres.GetGenreItem(Frame.GetV23TextFrameValue(v23Tag, "TCON")).GenreId;
            return newTag;
        }

        protected virtual string GetComment(byte[] tagBytes)
        {
            return this.GetStringFromByteSection(tagBytes, 0x61, 30);
        }

        private static string GetFirstV22CommentFrameValue(V22Tag v22Tag)
        {
            V22Frame frame = v22Tag.Frames.GetFrame("COM");
            return ((frame != null) ? ((COMFrame) frame).Comment : string.Empty);
        }

        private static string GetFirstV23CommentFrameValue(V23Tag v23Tag)
        {
            V2Frame frame = v23Tag.Frames.GetFrame("COMM");
            return ((frame != null) ? ((COMMFrame) frame).Comment : string.Empty);
        }

        private byte GetGenre(byte[] tagBytes)
        {
            return tagBytes[0x7f];
        }

        private string GetLengthLimitedString(string value, int start, int maxLength)
        {
            return value.Substring(start, (value.Length > maxLength) ? maxLength : value.Length);
        }

        private string GetStringFromByteSection(byte[] bytes, int start, int maxLength)
        {
            if (bytes[start] == 0)
            {
                return string.Empty;
            }
            string s = Encoding.UTF8.GetString(bytes, start, maxLength);
            if (s.IndexOf('\0') > 0)
            {
                s = s.Substring(0, s.IndexOf('\0'));
            }
            return s.Trim();
        }

        private byte GetTrackNumber(byte[] tagBytes)
        {
            return tagBytes[0x7e];
        }

        public void ParseTagBytes(byte[] tagBytes)
        {
            this.tag = Encoding.UTF8.GetString(tagBytes, 0, 3);
            if (this.tag != "TAG")
            {
                throw new ArgumentException("Tag byte array does not represent an ID3V1 tag.");
            }
            this.title = this.GetStringFromByteSection(tagBytes, 3, 30);
            this.artist = this.GetStringFromByteSection(tagBytes, 0x21, 30);
            this.album = this.GetStringFromByteSection(tagBytes, 0x3f, 30);
            this.year = this.GetStringFromByteSection(tagBytes, 0x5d, 4);
            this.comment = this.GetComment(tagBytes);
            this.genre = this.GetGenre(tagBytes);
            if ((tagBytes[0x7d] == 0) && (tagBytes[0x7e] > 0))
            {
                this.trackNumber = this.GetTrackNumber(tagBytes);
                this.majorVersion = 1;
            }
        }

        private byte[] StringToByteArray(string value, int size)
        {
            byte[] bytes = new byte[size];
            Array.Copy(Encoding.UTF8.GetBytes(value), bytes, Math.Min(value.Length, size));
            return bytes;
        }

        public byte[] ToByteArray()
        {
            byte[] bytes = new byte[0x80];
            Array.Copy(this.StringToByteArray(this.tag, 3), 0, bytes, 0, 3);
            Array.Copy(this.StringToByteArray(this.title, 30), 0, bytes, 3, 30);
            Array.Copy(this.StringToByteArray(this.artist, 30), 0, bytes, 0x21, 30);
            Array.Copy(this.StringToByteArray(this.album, 30), 0, bytes, 0x3f, 30);
            Array.Copy(this.StringToByteArray(this.year, 4), 0, bytes, 0x5d, 4);
            if (this.majorVersion == 0)
            {
                Array.Copy(this.CommentToByteArray(), 0, bytes, 0x61, 30);
            }
            else
            {
                Array.Copy(this.CommentToByteArray(), 0, bytes, 0x61, 30);
                bytes[0x7d] = 0;
                bytes[0x7e] = this.trackNumber;
            }
            bytes[0x7f] = Math.Min(this.genre, (byte)255);
            return bytes;
        }

        public override string ToString()
        {
            return ("ID3 Version 1." + this.majorVersion.ToString() + " Tag");
        }

        public string Album
        {
            get
            {
                return this.album;
            }
            set
            {
                this.album = this.GetLengthLimitedString(value, 0, 30);
            }
        }

        public string Artist
        {
            get
            {
                return this.artist;
            }
            set
            {
                this.artist = this.GetLengthLimitedString(value, 0, 30);
            }
        }

        public string Comment
        {
            get
            {
                return this.comment;
            }
            set
            {
                if (this.majorVersion == 0)
                {
                    this.comment = this.GetLengthLimitedString(value, 0, 30);
                }
                else
                {
                    this.comment = this.GetLengthLimitedString(value, 0, 30);
                }
            }
        }

        public List<V1Frame> Frames
        {
            get
            {
                return this.frames;
            }
        }

        public byte Genre
        {
            get
            {
                return this.genre;
            }
            set
            {
                this.genre = value;
            }
        }

        public int MajorVersion
        {
            get
            {
                return this.majorVersion;
            }
            set
            {
                this.majorVersion = value;
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = this.GetLengthLimitedString(value, 0, 30);
            }
        }

        public byte TrackNumber
        {
            get
            {
                if (this.majorVersion == 0)
                {
                    throw new NotImplementedException("TrackNumber not available for ID3 V1.0 tags.");
                }
                return this.trackNumber;
            }
            set
            {
                if (this.majorVersion == 0)
                {
                    throw new NotImplementedException("TrackNumber not available for ID3 V1.0 tags.");
                }
                this.trackNumber = value;
            }
        }

        public string Year
        {
            get
            {
                return this.year;
            }
            set
            {
                this.year = this.GetLengthLimitedString(value, 0, 4);
            }
        }
    }
}

