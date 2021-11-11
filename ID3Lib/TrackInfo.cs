namespace ID3Lib
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class TrackInfo
    {
        private string albumArtist = string.Empty;
        private string albumName = string.Empty;
        private string artistName = string.Empty;
        private string filePath;
        private Mp3File mp3File = null;
        private string trackName = string.Empty;
        private int trackNumber = 0;
        private ID3Lib.V1Tag v1Tag = null;
        private ID3Lib.V2Tag v2Tag = null;
        private List<ID3Lib.V2Tag> v2Tags = null;

        public TrackInfo(string filePath)
        {
            this.filePath = filePath;
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File " + filePath + " could not be found.");
            }
            this.mp3File = new Mp3File(filePath);
            this.GetTrackInfo();
            this.mp3File.Close();
            this.mp3File.Dispose();
        }

        private void GetAlbumArtist()
        {
            this.albumArtist = this.GetAlbumArtistFromV2Tag();
        }

        private string GetAlbumArtistFromV2Tag()
        {
            if (this.V2Tag != null)
            {
                IEnumerator<V2Frame> ie = null;
                ie = this.v2Tag.Frames["TPE2"];
                while (ie.MoveNext())
                {
                    TPE2TextFrame tf = (TPE2TextFrame) ie.Current;
                    return tf.Text;
                }
            }
            return string.Empty;
        }

        private string GetAlbumFromV1Tag()
        {
            if (this.V1Tag != null)
            {
                return this.V1Tag.Album;
            }
            return string.Empty;
        }

        private string GetAlbumFromV2Tag()
        {
            if (this.V2Tag != null)
            {
                IEnumerator<V2Frame> ie = null;
                ie = this.v2Tag.Frames["TALB"];
                while (ie.MoveNext())
                {
                    TALBTextFrame tf = (TALBTextFrame) ie.Current;
                    return tf.Text;
                }
            }
            return string.Empty;
        }

        private void GetAlbumName()
        {
            this.albumName = this.GetAlbumFromV2Tag();
            if (this.albumName.Trim().Length == 0)
            {
                this.albumName = this.GetAlbumFromV1Tag();
            }
        }

        private string GetArtistFromV1Tag()
        {
            if (this.V1Tag != null)
            {
                return this.V1Tag.Artist;
            }
            return string.Empty;
        }

        private string GetArtistFromV2Tag()
        {
            if (this.V2Tag != null)
            {
                IEnumerator<V2Frame> ie = null;
                ie = this.v2Tag.Frames["TPE1"];
                while (ie.MoveNext())
                {
                    TPE1TextFrame tf = (TPE1TextFrame) ie.Current;
                    return tf.Text;
                }
            }
            return string.Empty;
        }

        private void GetArtistName()
        {
            this.artistName = this.GetArtistFromV2Tag();
            if (this.artistName.Trim().Length == 0)
            {
                this.artistName = this.GetArtistFromV1Tag();
            }
        }

        private string GetTrackFromV1Tag()
        {
            if (this.V1Tag != null)
            {
                return this.V1Tag.Title;
            }
            return string.Empty;
        }

        private string GetTrackFromV2Tag()
        {
            if (this.V2Tag != null)
            {
                IEnumerator<V2Frame> ie = null;
                ie = this.v2Tag.Frames["TIT2"];
                while (ie.MoveNext())
                {
                    TIT2TextFrame tf = (TIT2TextFrame) ie.Current;
                    return tf.Text;
                }
            }
            return string.Empty;
        }

        private void GetTrackInfo()
        {
            this.GetAlbumName();
            this.GetArtistName();
            this.GetTrackName();
            this.GetAlbumArtist();
            this.GetTrackNumber();
        }

        private void GetTrackName()
        {
            this.trackName = this.GetTrackFromV2Tag();
            if (this.trackName.Trim().Length == 0)
            {
                this.trackName = this.GetTrackFromV1Tag();
            }
        }

        private void GetTrackNumber()
        {
            this.trackNumber = this.GetTrackNumberFromV2Tag();
        }

        private int GetTrackNumberFromV2Tag()
        {
            int num;
            string text = "0";
            if (this.V2Tag != null)
            {
                IEnumerator<V2Frame> ie = null;
                ie = this.v2Tag.Frames["TRCK"];
                while (ie.MoveNext())
                {
                    TRCKTextFrame tf = (TRCKTextFrame) ie.Current;
                    text = tf.Text;
                    break;
                }
            }
            if (text.IndexOf("/") > 0)
            {
                text = text.Split(new char[] { '/' })[0];
            }
            int.TryParse(text, out num);
            return num;
        }

        private void GetV1Tag()
        {
            this.v1Tag = this.mp3File.GetV1Tag();
        }

        private void GetV2Tag()
        {
            this.v2Tags = this.mp3File.GetV2Tags();
            if (this.v2Tags.Count > 0)
            {
                this.v2Tag = this.v2Tags[0];
            }
        }

        public string AlbumArtist
        {
            get
            {
                return this.albumArtist;
            }
            set
            {
                this.albumArtist = value;
            }
        }

        public string AlbumName
        {
            get
            {
                return this.albumName;
            }
        }

        public string ArtistName
        {
            get
            {
                return this.artistName;
            }
        }

        public string FilePath
        {
            get
            {
                return this.filePath;
            }
        }

        public string TrackName
        {
            get
            {
                return this.trackName;
            }
        }

        public int TrackNumber
        {
            get
            {
                return this.trackNumber;
            }
        }

        private ID3Lib.V1Tag V1Tag
        {
            get
            {
                if (this.v1Tag == null)
                {
                    this.GetV1Tag();
                }
                return this.v1Tag;
            }
        }

        private ID3Lib.V2Tag V2Tag
        {
            get
            {
                if (this.v2Tag == null)
                {
                    this.GetV2Tag();
                }
                return this.v2Tag;
            }
        }
    }
}

