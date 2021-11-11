using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ID3Lib
{
    public class TCONTextFrame : V2TextFrame
    {
        //private ID3Genres genreList = new ID3Genres();

        public TCONTextFrame(byte[] frameValue)
            : base("TCON", frameValue)
        {
        }
        
        public TCONTextFrame(string genre, EncodingTypes encodingType)
            : this(genre)
        {
            SetEncoding(encodingType);
        }

        public TCONTextFrame(string genre)
        {
            FrameId = "TCON";
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

        public static string ParseGenreText(string value)
        {
            return "(" + ID3Genres.GetGenreItem(value).GenreId + ")"; 
        }

        public static string ParseGenreCodes(string p)
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
    }
}
