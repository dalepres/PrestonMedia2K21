namespace ID3Lib
{
    using System;
    using System.Collections.Generic;

    public static class ID3Genres
    {
        private static List<ID3GenreItem> genres = new List<ID3GenreItem>();

        static ID3Genres()
        {
            ID3GenreItem[] genresList = new ID3GenreItem[] { 
                new ID3GenreItem(0xff, ""), new ID3GenreItem(0, "Blues"), new ID3GenreItem(1, "Classic Rock"), new ID3GenreItem(2, "Country"), new ID3GenreItem(3, "Dance"), new ID3GenreItem(4, "Disco"), new ID3GenreItem(5, "Funk"), new ID3GenreItem(6, "Grunge"), new ID3GenreItem(7, "Hip-Hop"), new ID3GenreItem(8, "Jazz"), new ID3GenreItem(9, "Metal"), new ID3GenreItem(10, "New Age"), new ID3GenreItem(11, "Oldies"), new ID3GenreItem(12, "Other"), new ID3GenreItem(13, "Pop"), new ID3GenreItem(14, "R&B"), 
                new ID3GenreItem(15, "Rap"), new ID3GenreItem(0x10, "Reggae"), new ID3GenreItem(0x11, "Rock"), new ID3GenreItem(0x12, "Techno"), new ID3GenreItem(0x13, "Industrial"), new ID3GenreItem(20, "Alternative"), new ID3GenreItem(0x15, "Ska"), new ID3GenreItem(0x16, "Death Metal"), new ID3GenreItem(0x17, "Pranks"), new ID3GenreItem(0x18, "Soundtrack"), new ID3GenreItem(0x19, "Euro-Techno"), new ID3GenreItem(0x1a, "Ambient"), new ID3GenreItem(0x1b, "Trip-Hop"), new ID3GenreItem(0x1c, "Vocal"), new ID3GenreItem(0x1d, "Jazz+Funk"), new ID3GenreItem(30, "Fusion"), 
                new ID3GenreItem(0x1f, "Trance"), new ID3GenreItem(0x20, "Classical"), new ID3GenreItem(0x21, "Instrumental"), new ID3GenreItem(0x22, "Acid"), new ID3GenreItem(0x23, "House"), new ID3GenreItem(0x24, "Game"), new ID3GenreItem(0x25, "Sound Clip"), new ID3GenreItem(0x26, "Gospel"), new ID3GenreItem(0x27, "Noise"), new ID3GenreItem(40, "AlternRock"), new ID3GenreItem(0x29, "Bass"), new ID3GenreItem(0x2a, "Soul"), new ID3GenreItem(0x2b, "Punk"), new ID3GenreItem(0x2c, "Space"), new ID3GenreItem(0x2d, "Meditative"), new ID3GenreItem(0x2e, "Instrumental Pop"), 
                new ID3GenreItem(0x2f, "Instrumental Rock"), new ID3GenreItem(0x30, "Ethnic"), new ID3GenreItem(0x31, "Gothic"), new ID3GenreItem(50, "Darkwave"), new ID3GenreItem(0x33, "Techno-Industrial"), new ID3GenreItem(0x34, "Electronic"), new ID3GenreItem(0x35, "Pop-Folk"), new ID3GenreItem(0x36, "Eurodance"), new ID3GenreItem(0x37, "Dream"), new ID3GenreItem(0x38, "Southern Rock"), new ID3GenreItem(0x39, "Comedy"), new ID3GenreItem(0x3a, "Cult"), new ID3GenreItem(0x3b, "Gangsta"), new ID3GenreItem(60, "Top 40"), new ID3GenreItem(0x3d, "Christian Rap"), new ID3GenreItem(0x3e, "Pop/Funk"), 
                new ID3GenreItem(0x3f, "Jungle"), new ID3GenreItem(0x40, "Native American"), new ID3GenreItem(0x41, "Cabaret"), new ID3GenreItem(0x42, "New Wave"), new ID3GenreItem(0x43, "Psychadelic"), new ID3GenreItem(0x44, "Rave"), new ID3GenreItem(0x45, "Showtunes"), new ID3GenreItem(70, "Trailer"), new ID3GenreItem(0x47, "Lo-Fi"), new ID3GenreItem(0x48, "Tribal"), new ID3GenreItem(0x49, "Acid Punk"), new ID3GenreItem(0x4a, "Acid Jazz"), new ID3GenreItem(0x4b, "Polka"), new ID3GenreItem(0x4c, "Retro"), new ID3GenreItem(0x4d, "Musical"), new ID3GenreItem(0x4e, "Rock & Roll"), 
                new ID3GenreItem(0x4f, "Hard Rock"), new ID3GenreItem(80, "Folk"), new ID3GenreItem(0x51, "Folk-Rock"), new ID3GenreItem(0x52, "National Folk"), new ID3GenreItem(0x53, "Swing"), new ID3GenreItem(0x54, "Fast Fusion"), new ID3GenreItem(0x55, "Bebob"), new ID3GenreItem(0x56, "Latin"), new ID3GenreItem(0x57, "Revival"), new ID3GenreItem(0x58, "Celtic"), new ID3GenreItem(0x59, "Bluegrass"), new ID3GenreItem(90, "Avantgarde"), new ID3GenreItem(0x5b, "Gothic Rock"), new ID3GenreItem(0x5c, "Progressive Rock"), new ID3GenreItem(0x5d, "Psychedelic Rock"), new ID3GenreItem(0x5e, "Symphonic Rock"), 
                new ID3GenreItem(0x5f, "Slow Rock"), new ID3GenreItem(0x60, "Big Band"), new ID3GenreItem(0x61, "Chorus"), new ID3GenreItem(0x62, "Easy Listening"), new ID3GenreItem(0x63, "Acoustic"), new ID3GenreItem(100, "Humour"), new ID3GenreItem(0x65, "Speech"), new ID3GenreItem(0x66, "Chanson"), new ID3GenreItem(0x67, "Opera"), new ID3GenreItem(0x68, "Chamber Music"), new ID3GenreItem(0x69, "Sonata"), new ID3GenreItem(0x6a, "Symphony"), new ID3GenreItem(0x6b, "Booty Bass"), new ID3GenreItem(0x6c, "Primus"), new ID3GenreItem(0x6d, "Porn Groove"), new ID3GenreItem(110, "Satire"), 
                new ID3GenreItem(0x6f, "Slow Jam"), new ID3GenreItem(0x70, "Club"), new ID3GenreItem(0x71, "Tango"), new ID3GenreItem(0x72, "Samba"), new ID3GenreItem(0x73, "Folklore"), new ID3GenreItem(0x74, "Ballad"), new ID3GenreItem(0x75, "Power Ballad"), new ID3GenreItem(0x76, "Rhythmic Soul"), new ID3GenreItem(0x77, "Freestyle"), new ID3GenreItem(120, "Duet"), new ID3GenreItem(0x79, "Punk Rock"), new ID3GenreItem(0x7a, "Drum Solo"), new ID3GenreItem(0x7b, "A capella"), new ID3GenreItem(0x7c, "Euro-House"), new ID3GenreItem(0x7d, "Dance Hall")
             };
            genres.AddRange(genresList);
            genres.Sort();
        }

        public static ID3GenreItem GetGenreItem(int genreId)
        {
            for (int count = 0; count < Genres.Length; count++)
            {
                if (Genres[count].GenreId == genreId)
                {
                    return Genres[count];
                }
            }
            return new ID3GenreItem(0xff, "");
        }

        public static ID3GenreItem GetGenreItem(string genre)
        {
            for (int count = 0; count < Genres.Length; count++)
            {
                if (Genres[count].Genre == genre)
                {
                    return Genres[count];
                }
            }
            return new ID3GenreItem(0xff, "");
        }

        public static ID3GenreItem[] GetGenreList()
        {
            return Genres;
        }

        public static ID3GenreItem[] Genres
        {
            get
            {
                return genres.ToArray();
            }
        }
    }
}

