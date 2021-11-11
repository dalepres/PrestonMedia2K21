using System;
using System.Collections.Generic;
using System.Text;

namespace ID3Lib
{
    public class ID3GenreItem : IComparable
    {
        private byte genreId = 255;
        private string genre;

        public ID3GenreItem(byte genreId, string genre)
        {
            this.genreId = genreId;
            this.genre = genre;
        }

        public override string ToString()
        {
            return genre;
        }

        public override bool Equals(object obj)
        {
            if (obj is int)
            {
                return genreId == (int)obj;
            }
            else if (obj is ID3GenreItem)
            {
                return genreId == ((ID3GenreItem)obj).GenreId;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return genre.GetHashCode();
        }

        public byte GenreId
        {
            get { return genreId; }
            set { genreId = value; }
        }

        public string Genre
        {
            get { return genre; }
            set { genre = value; }
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            if (obj is ID3GenreItem)
            {
                return string.Compare(genre, ((ID3GenreItem)obj).Genre, true);
            }
            else
            {
                throw new ArgumentException("Only ID3GenreItem objects can be compared using ID3GenreItem.CompareTo().");
            }
        }

        #endregion
    }
}
