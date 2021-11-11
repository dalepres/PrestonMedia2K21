namespace ID3Lib
{
    using System;
    using System.Text.RegularExpressions;

    public class TCONTextFrame : V2TextFrame
    {
        public TCONTextFrame(byte[] frameValue) : base("TCON", frameValue)
        {
        }

        public TCONTextFrame(string genre)
        {
            base.FrameId = "TCON";
            base.Text = ParseGenreText(genre);
        }

        public TCONTextFrame(string genre, EncodingTypes encodingType) : this(genre)
        {
            base.SetEncoding(encodingType);
        }

        public static string ParseGenreCodes(string p)
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

        public static string ParseGenreText(string value)
        {
            return ("(" + ID3Genres.GetGenreItem(value).GenreId + ")");
        }

        public override string Text
        {
            get
            {
                return ParseGenreCodes(base.Text);
            }
            set
            {
                base.Text = ParseGenreText(value);
            }
        }
    }
}

