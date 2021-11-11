using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ID3Lib
{
    public class TCOTextFrame : V22TextFrame
    {
        //private ID3Genres genreList = new ID3Genres();

        public TCOTextFrame(byte[] frameValue)
            : base("TCO", frameValue)
        {
        }
        
        public TCOTextFrame(string genre, EncodingTypes encodingType)
            : this(genre)
        {
            SetEncoding(encodingType);
        }

        public TCOTextFrame(string genre)
        {
            FrameId = "TCO";
            base.Text = ParseGenreText(genre);
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

        private string ParseGenreText(string value)
        {
            return "(" + ID3Genres.GetGenreItem(value).GenreId + ")"; 
        }

        private string ParseGenreCodes(string p)
        {
            int genreId;
            Regex r = new Regex(@"\(\d{1,3}\)");
            Match m = r.Match(p);

            if (m == Match.Empty)
            {
                return string.Empty;
            }

            p = p.Replace("(", "").Replace(")", "");

            if (!int.TryParse(p, out genreId))
            {
                genreId = 255;
            }

            return ID3Genres.GetGenreItem(genreId).Genre;
        }


        public static explicit operator TCONTextFrame(TCOTextFrame frame)
        {
            if (frame != null)
            {
                return new TCONTextFrame(frame.FrameValue);
            }

            return null;
        }
    }
}
