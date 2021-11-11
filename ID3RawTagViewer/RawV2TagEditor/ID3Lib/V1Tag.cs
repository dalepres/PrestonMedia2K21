using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ID3Lib
{
    public class V1Tag : ID3Tag, ICloneable
    {
        /*
         * "TAG":        3 bytes
         * title:       30 bytes
         * artist:      30 bytes
         * album:       30 bytes
         * year:         4 bytes
         * comments:    30 bytes
         * genre:        1 byte
         * TOTAL:      128 bytes 
         * 
         * // If comments 28 = 0 and comments 29 > 0 then 
         * // tag is v1.1 tag and comments 29 is track number.
        */

        private const int TAG_START = 0;
        private const int TAG_MAX_LENGTH = 3;
        private const int TITLE_START = 3;
        private const int TITLE_MAX_LENGTH = 30;
        private const int ARTIST_START = 33;
        private const int ARTIST_MAX_LENGTH = 30;
        private const int ALBUM_START = 63;
        private const int ALBUM_MAX_LENGTH = 30;
        private const int YEAR_START = 93;
        private const int YEAR_MAX_LENGTH = 4;
        private const int COMMENT_START = 97;
        private const int V1_0_COMMENT_MAX_LENGTH = 30;
        private const int V1_1_COMMENT_MAX_LENGTH = 28;
        private const int GENRE_START = 127;
        private const int GENRE_MAX_LENGTH = 1;
        private const int COMMENT_TRACK_SEPERATOR_BYTE_POSITION = 125;
        private const int TRACK_NUMBER_BYTE_POSITION = 126;
        private const int GENRE_BYTE_POSITION = 127;
        private const int TAG_LENGTH = 128;

        private string tag;
        private string title;
        private string artist;
        private string album;
        private string year;
        private string comment;
        private byte genre = 255;
        private byte trackNumber;
        private int majorVersion = 0;

        byte[] v1TagBytes = new byte[TAG_LENGTH];

        List<V1Frame> frames;

        public V1Tag(byte[] tagBytes)
        {
            this.v1TagBytes = tagBytes;
            ParseTagBytes(tagBytes);

            frames = CreateFrames();
        }


        public V1Tag(int majorVersion)
        {
            this.majorVersion = majorVersion;
        }


        public V1Tag()
        {
        }

        public static byte[] GetTagRaw(FileStream fs)
        {
            if (fs.Length > TAG_LENGTH)
            {
                long position = fs.Position;
                fs.Position = fs.Length - TAG_LENGTH;
                byte[] tagBytes = new byte[TAG_LENGTH];
                int tagLength = fs.Read(tagBytes, 0, TAG_LENGTH);
                fs.Position = position;

                if (tagLength == TAG_LENGTH && IsId3Tag(tagBytes))
                {
                    return tagBytes;
                }
            }

            return new byte[0];
        }

        private static bool IsId3Tag(byte[] tagBytes)
        {
            return tagBytes.Length == 128 && ID3Tag.StartsWithID3(tagBytes);
        }

        public List<V1Frame> CreateFrames()
        {
            List<V1Frame> f = new List<V1Frame>();
            f.Add(new V1Frame("Title", GetTitleBytes(v1TagBytes)));
            f.Add(new V1Frame("Artist", GetArtistBytes(v1TagBytes)));
            f.Add(new V1Frame("Album", GetAlbumBytes(v1TagBytes)));
            f.Add(new V1Frame("Year", GetYearBytes(v1TagBytes)));
            f.Add(new V1Frame("Comment", GetCommentBytes(v1TagBytes)));
            f.Add(new V1ByteFrame("Genre", new byte[] { GetGenre(v1TagBytes) }));


            UTF8Encoding e = new UTF8Encoding();
            //f.Add(new V1TextFrame("Title", e.GetBytes(title)));
            //f.Add(new V1TextFrame("Artist", e.GetBytes(artist)));
            //f.Add(new V1TextFrame("Album", e.GetBytes(album)));
            //f.Add(new V1TextFrame("Year", e.GetBytes(year)));
            //f.Add(new V1TextFrame("Comment", e.GetBytes(comment)));

            //f.Add(new V1ByteFrame("Genre", new byte[] { genre }));

            if (majorVersion == 1)
            {
                f.Add(new V1ByteFrame("Track", new byte[] { TrackNumber }));
            }

            return f;
        }

        private byte[] GetCommentBytes(byte[] v1TagBytes)
        {
            return GetFrameBytes(v1TagBytes, COMMENT_START, 
                this.majorVersion == 0 ? V1_0_COMMENT_MAX_LENGTH : V1_1_COMMENT_MAX_LENGTH);
        }

        private byte[] GetYearBytes(byte[] v1TagBytes)
        {
            return GetFrameBytes(v1TagBytes, YEAR_START, YEAR_MAX_LENGTH);
        }

        private byte[] GetAlbumBytes(byte[] v1TagBytes)
        {
            return GetFrameBytes(v1TagBytes, ALBUM_START, ALBUM_MAX_LENGTH);
        }

        private byte[] GetArtistBytes(byte[] v1TagBytes)
        {
            return GetFrameBytes(v1TagBytes, ARTIST_START, ARTIST_MAX_LENGTH);
        }

        private byte[] GetTitleBytes(byte[] v1TagBytes)
        {
            return GetFrameBytes(v1TagBytes, TITLE_START, TITLE_MAX_LENGTH);
        }


        private byte[] GetFrameBytes(byte[] tagBytes, int start, int length)
        {
            byte[] bytes = new byte[length];
            Array.Copy(tagBytes, start, bytes, 0, length);
            return bytes;
        }


        public void ParseTagBytes(byte[] tagBytes)
        {
            tag = Encoding.UTF8.GetString(tagBytes, TAG_START, TAG_MAX_LENGTH);
            if (tag != "TAG")
            {
                throw new ArgumentException("Tag byte array does not represent an ID3V1 tag.");
            }

            title = GetStringFromByteSection(tagBytes, TITLE_START, TITLE_MAX_LENGTH);
            artist = GetStringFromByteSection(tagBytes, ARTIST_START, ARTIST_MAX_LENGTH);
            album = GetStringFromByteSection(tagBytes, ALBUM_START, ALBUM_MAX_LENGTH);
            year = GetStringFromByteSection(tagBytes, YEAR_START, YEAR_MAX_LENGTH);
            comment = GetComment(tagBytes);
            genre = GetGenre(tagBytes);

            if (tagBytes[COMMENT_TRACK_SEPERATOR_BYTE_POSITION] == 0 && tagBytes[TRACK_NUMBER_BYTE_POSITION] > 0)
            {
                trackNumber = GetTrackNumber(tagBytes);
                majorVersion = 1;
            }
        }


        private byte GetTrackNumber(byte[] tagBytes)
        {
            return tagBytes[TRACK_NUMBER_BYTE_POSITION];
        }


        protected virtual string GetComment(byte[] tagBytes)
        {
            return GetStringFromByteSection(tagBytes, COMMENT_START, V1_1_COMMENT_MAX_LENGTH);
        }


        private byte GetGenre(byte[] tagBytes)
        {
            return tagBytes[GENRE_START];
        }

        private string GetLengthLimitedString(string value, int start, int maxLength)
        {
            return value.Substring(start,
                value.Length > maxLength ? maxLength : value.Length);
        }


        private string GetStringFromByteSection(byte[] bytes, int start, int maxLength)
        {
            if (bytes[start] == 0)
            {
                return string.Empty;
            }

            string s = Encoding.UTF8.GetString(bytes, start, maxLength);
            if (s.IndexOf((char)0) > 0)
            {
                s = s.Substring(0, s.IndexOf((char)0));
            }

            return s.Trim();
        }

        
        public string Title
        {
            get { return title; }
            set { title = GetLengthLimitedString(value, 0, TITLE_MAX_LENGTH); }
        }


        public string Artist
        {
            get { return artist; }
            set { artist = GetLengthLimitedString(value, 0, ARTIST_MAX_LENGTH); }
        }


        public string Album
        {
            get { return album; }
            set { album = GetLengthLimitedString(value, 0, ALBUM_MAX_LENGTH); }
        }


        public string Year
        {
            get { return year; }
            set { year = GetLengthLimitedString(value, 0, YEAR_MAX_LENGTH); }
        }


        public string Comment
        {
            get { return comment; }
            set
            {
                if (majorVersion == 0)
                {
                    comment = GetLengthLimitedString(value, 0, V1_0_COMMENT_MAX_LENGTH);
                }
                else
                {
                    comment = GetLengthLimitedString(value, 0, V1_1_COMMENT_MAX_LENGTH);
                }
            }
        }


        public override string ToString()
        {
            return "ID3 Version 1." + majorVersion.ToString() + " Tag";
        }


        public byte Genre
        {
            get { return genre; }
            set { genre = value; }
        }


        public byte TrackNumber
        {
            get
            {
                if (majorVersion == 0)
                {
                    throw new NotImplementedException("TrackNumber not available for ID3 V1.0 tags.");
                }
                else
                {
                    return trackNumber;
                }
            }

            set
            {
                if (majorVersion == 0)
                {
                    throw new NotImplementedException("TrackNumber not available for ID3 V1.0 tags.");
                }
                else
                {
                    trackNumber = value;
                }
            }
        }

        public int MajorVersion
        {
            get { return majorVersion; }
            set { majorVersion = value; }
        }


        public List<V1Frame> Frames
        {
            get { return frames; }
        }

        public byte[] TagBytes
        {
            get { return v1TagBytes; }
        }

        public byte[] ToByteArray()
        {
            byte[] bytes = new byte[128];
            Array.Copy(StringToByteArray(tag, TAG_MAX_LENGTH), 0, bytes, TAG_START, TAG_MAX_LENGTH);
            Array.Copy(StringToByteArray(title, TITLE_MAX_LENGTH), 0, bytes, TITLE_START, TITLE_MAX_LENGTH);
            Array.Copy(StringToByteArray(artist, ARTIST_MAX_LENGTH), 0, bytes, ARTIST_START, ARTIST_MAX_LENGTH);
            Array.Copy(StringToByteArray(album, ALBUM_MAX_LENGTH), 0, bytes, ALBUM_START, ALBUM_MAX_LENGTH);
            Array.Copy(StringToByteArray(year, YEAR_MAX_LENGTH), 0, bytes, YEAR_START, YEAR_MAX_LENGTH);

            if (majorVersion == 0)
            {
                Array.Copy(CommentToByteArray(), 0, bytes, COMMENT_START, V1_0_COMMENT_MAX_LENGTH);
            }
            else
            {
                Array.Copy(CommentToByteArray(), 0, bytes, COMMENT_START, V1_1_COMMENT_MAX_LENGTH);
                bytes[COMMENT_TRACK_SEPERATOR_BYTE_POSITION] = 0;
                bytes[TRACK_NUMBER_BYTE_POSITION] = trackNumber;
            }

            bytes[GENRE_BYTE_POSITION] = (byte)Math.Min(genre, (byte)255);
            return bytes;
        }


        internal virtual byte[] CommentToByteArray()
        {
            if (majorVersion == 0)
            {
                return StringToByteArray(comment, V1_0_COMMENT_MAX_LENGTH);
            }
            else
            {
                return StringToByteArray(comment, V1_1_COMMENT_MAX_LENGTH);
            }
        }


        private byte[] StringToByteArray(string value, int size)
        {
            byte[] bytes = new byte[size];
            Array.Copy(Encoding.UTF8.GetBytes(value),bytes,Math.Min(value.Length,size));
            return bytes;
        }

        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}
