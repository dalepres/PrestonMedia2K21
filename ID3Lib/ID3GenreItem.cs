namespace ID3Lib
{
    using System;

    public class ID3GenreItem : IComparable
    {
        private string genre;
        private byte genreId = 0xff;

        public ID3GenreItem(byte genreId, string genre)
        {
            this.genreId = genreId;
            this.genre = genre;
        }

        public int CompareTo(object obj)
        {
            if (!(obj is ID3GenreItem))
            {
                throw new ArgumentException("Only ID3GenreItem objects can be compared using ID3GenreItem.CompareTo().");
            }
            return string.Compare(this.genre, ((ID3GenreItem) obj).Genre, true);
        }

        public override bool Equals(object obj)
        {
            if (obj is int)
            {
                return (this.genreId == ((int) obj));
            }
            return ((obj is ID3GenreItem) && (this.genreId == ((ID3GenreItem) obj).GenreId));
        }

        public override int GetHashCode()
        {
            return this.genre.GetHashCode();
        }

        public override string ToString()
        {
            return this.genre;
        }

        public string Genre
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

        public byte GenreId
        {
            get
            {
                return this.genreId;
            }
            set
            {
                this.genreId = value;
            }
        }
    }
}

