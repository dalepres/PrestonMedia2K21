using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace ID3Lib
{
    public static class ID3Genres
    {
        private static List<ID3GenreItem> genres = new List<ID3GenreItem>();

        public static ID3GenreItem[] GetGenreList()
        {
            return Genres;
        }

        static ID3Genres()
        {
            ID3GenreItem[] genresList = new ID3GenreItem[] {
                new ID3GenreItem(255, ""),
                new ID3GenreItem(0, "Blues"),
                new ID3GenreItem(1, "Classic Rock"),
                new ID3GenreItem(2, "Country"),
                new ID3GenreItem(3, "Dance"),
                new ID3GenreItem(4, "Disco"),
                new ID3GenreItem(5, "Funk"),
                new ID3GenreItem(6, "Grunge"),
                new ID3GenreItem(7, "Hip-Hop"),
                new ID3GenreItem(8, "Jazz"),
                new ID3GenreItem(9, "Metal"),
                new ID3GenreItem(10, "New Age"),
                new ID3GenreItem(11, "Oldies"),
                new ID3GenreItem(12, "Other"),
                new ID3GenreItem(13, "Pop"),
                new ID3GenreItem(14, "R&B"),
                new ID3GenreItem(15, "Rap"),
                new ID3GenreItem(16, "Reggae"),
                new ID3GenreItem(17, "Rock"),
                new ID3GenreItem(18, "Techno"),
                new ID3GenreItem(19, "Industrial"),
                new ID3GenreItem(20, "Alternative"),
                new ID3GenreItem(21, "Ska"),
                new ID3GenreItem(22, "Death Metal"),
                new ID3GenreItem(23, "Pranks"),
                new ID3GenreItem(24, "Soundtrack"),
                new ID3GenreItem(25, "Euro-Techno"),
                new ID3GenreItem(26, "Ambient"),
                new ID3GenreItem(27, "Trip-Hop"),
                new ID3GenreItem(28, "Vocal"),
                new ID3GenreItem(29, "Jazz+Funk"),
                new ID3GenreItem(30, "Fusion"),
                new ID3GenreItem(31, "Trance"),
                new ID3GenreItem(32, "Classical"),
                new ID3GenreItem(33, "Instrumental"),
                new ID3GenreItem(34, "Acid"),
                new ID3GenreItem(35, "House"),
                new ID3GenreItem(36, "Game"),
                new ID3GenreItem(37, "Sound Clip"),
                new ID3GenreItem(38, "Gospel"),
                new ID3GenreItem(39, "Noise"),
                new ID3GenreItem(40, "AlternRock"),
                new ID3GenreItem(41, "Bass"),
                new ID3GenreItem(42, "Soul"),
                new ID3GenreItem(43, "Punk"),
                new ID3GenreItem(44, "Space"),
                new ID3GenreItem(45, "Meditative"),
                new ID3GenreItem(46, "Instrumental Pop"),
                new ID3GenreItem(47, "Instrumental Rock"),
                new ID3GenreItem(48, "Ethnic"),
                new ID3GenreItem(49, "Gothic"),
                new ID3GenreItem(50, "Darkwave"),
                new ID3GenreItem(51, "Techno-Industrial"),
                new ID3GenreItem(52, "Electronic"),
                new ID3GenreItem(53, "Pop-Folk"),
                new ID3GenreItem(54, "Eurodance"),
                new ID3GenreItem(55, "Dream"),
                new ID3GenreItem(56, "Southern Rock"),
                new ID3GenreItem(57, "Comedy"),
                new ID3GenreItem(58, "Cult"),
                new ID3GenreItem(59, "Gangsta"),
                new ID3GenreItem(60, "Top 40"),
                new ID3GenreItem(61, "Christian Rap"),
                new ID3GenreItem(62, "Pop/Funk"),
                new ID3GenreItem(63, "Jungle"),
                new ID3GenreItem(64, "Native American"),
                new ID3GenreItem(65, "Cabaret"),
                new ID3GenreItem(66, "New Wave"),
                new ID3GenreItem(67, "Psychadelic"),
                new ID3GenreItem(68, "Rave"),
                new ID3GenreItem(69, "Showtunes"),
                new ID3GenreItem(70, "Trailer"),
                new ID3GenreItem(71, "Lo-Fi"),
                new ID3GenreItem(72, "Tribal"),
                new ID3GenreItem(73, "Acid Punk"),
                new ID3GenreItem(74, "Acid Jazz"),
                new ID3GenreItem(75, "Polka"),
                new ID3GenreItem(76, "Retro"),
                new ID3GenreItem(77, "Musical"),
                new ID3GenreItem(78, "Rock & Roll"),
                new ID3GenreItem(79, "Hard Rock"),
                new ID3GenreItem(80, "Folk"),
                new ID3GenreItem(81, "Folk-Rock"),
                new ID3GenreItem(82, "National Folk"),
                new ID3GenreItem(83, "Swing"),
                new ID3GenreItem(84, "Fast Fusion"),
                new ID3GenreItem(85, "Bebob"),
                new ID3GenreItem(86, "Latin"),
                new ID3GenreItem(87, "Revival"),
                new ID3GenreItem(88, "Celtic"),
                new ID3GenreItem(89, "Bluegrass"),
                new ID3GenreItem(90, "Avantgarde"),
                new ID3GenreItem(91, "Gothic Rock"),
                new ID3GenreItem(92, "Progressive Rock"),
                new ID3GenreItem(93, "Psychedelic Rock"),
                new ID3GenreItem(94, "Symphonic Rock"),
                new ID3GenreItem(95, "Slow Rock"),
                new ID3GenreItem(96, "Big Band"),
                new ID3GenreItem(97, "Chorus"),
                new ID3GenreItem(98, "Easy Listening"),
                new ID3GenreItem(99, "Acoustic"),
                new ID3GenreItem(100, "Humour"),
                new ID3GenreItem(101, "Speech"),
                new ID3GenreItem(102, "Chanson"),
                new ID3GenreItem(103, "Opera"),
                new ID3GenreItem(104, "Chamber Music"),
                new ID3GenreItem(105, "Sonata"),
                new ID3GenreItem(106, "Symphony"),
                new ID3GenreItem(107, "Booty Bass"),
                new ID3GenreItem(108, "Primus"),
                new ID3GenreItem(109, "Porn Groove"),
                new ID3GenreItem(110, "Satire"),
                new ID3GenreItem(111, "Slow Jam"),
                new ID3GenreItem(112, "Club"),
                new ID3GenreItem(113, "Tango"),
                new ID3GenreItem(114, "Samba"),
                new ID3GenreItem(115, "Folklore"),
                new ID3GenreItem(116, "Ballad"),
                new ID3GenreItem(117, "Power Ballad"),
                new ID3GenreItem(118, "Rhythmic Soul"),
                new ID3GenreItem(119, "Freestyle"),
                new ID3GenreItem(120, "Duet"),
                new ID3GenreItem(121, "Punk Rock"),
                new ID3GenreItem(122, "Drum Solo"),
                new ID3GenreItem(123, "A capella"),
                new ID3GenreItem(124, "Euro-House"),
                new ID3GenreItem(125, "Dance Hall")
            };

            genres.AddRange(genresList);
            genres.Sort();
        }

        public static ID3GenreItem[] Genres
        {
            get
            {
                return genres.ToArray();
            }
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

            return new ID3GenreItem(255, "");
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

            return new ID3GenreItem(255, "");
        }

    }
}
