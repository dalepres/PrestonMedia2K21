namespace ID3Lib
{
    using System;
    using System.Text.RegularExpressions;

    public class TCOTextFrame : V22TextFrame
    {
        public TCOTextFrame(byte[] frameValue) : base("TCO", frameValue)
        {
        }

        public TCOTextFrame(string genre)
        {
            base.FrameId = "TCO";
            base.Text = this.ParseGenreText(genre);
        }

        public TCOTextFrame(string genre, EncodingTypes encodingType) : this(genre)
        {
            base.SetEncoding(encodingType);
        }

        public static explicit operator TCONTextFrame(TCOTextFrame frame)
        {
            if (frame != null)
            {
                return new TCONTextFrame(frame.FrameValue);
            }
            return null;
        }

        private string ParseGenreCodes(string p)
        {
            int genreId;
            Regex r = new Regex(@"\(\d{1,3}\)");
            if (r.Match(p) == Match.Empty)
            {
                return string.Empty;
            }
            p = p.Replace("(", "").Replace(")", "");
            if (!int.TryParse(p, out genreId))
            {
                genreId = 0xff;
            }
            return ID3Genres.GetGenreItem(genreId).Genre;
        }

        private string ParseGenreText(string value)
        {
            return ("(" + ID3Genres.GetGenreItem(value).GenreId + ")");
        }

        public override string Text
        {
            get
            {
                return this.ParseGenreCodes(base.Text);
            }
            set
            {
                base.Text = this.ParseGenreText(value);
            }
        }
    }
}

