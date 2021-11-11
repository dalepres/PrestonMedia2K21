using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace ID3Lib
{
    public static class ID3PictureTypes
    {
        private static List<ID3PictureType> pictureTypes = new List<ID3PictureType>();

        public static ID3PictureType[] GetPictureTypes()
        {
            return PictureTypes;
        }

        static ID3PictureTypes()
        {
            ID3PictureType[] pictureTypeList = new ID3PictureType[] {
                new ID3PictureType(0, "Other"),
                new ID3PictureType(1, "32x32 pixels 'file icon' (PNG only)"),
                new ID3PictureType(2, "Other file icon"),
                new ID3PictureType(3, "Cover (front)"),
                new ID3PictureType(4, "Cover (back)"),
                new ID3PictureType(5, "Leaflet page"),
                new ID3PictureType(6, "Media (e.g. lable side of CD)"),
                new ID3PictureType(7, "Lead artist/lead performer/soloist"),
                new ID3PictureType(8, "Artist/performer"),
                new ID3PictureType(9, "Conductor"),
                new ID3PictureType(10, "Band/Orchestra"),
                new ID3PictureType(11, "Composer"),
                new ID3PictureType(12, "Lyricist/text writer"),
                new ID3PictureType(13, "Recording Location"),
                new ID3PictureType(14, "During recording"),
                new ID3PictureType(15, "During performance"),
                new ID3PictureType(16, "Movie/video screen capture"),
                new ID3PictureType(17, "A bright coloured fish"),
                new ID3PictureType(18, "Illustration"),
                new ID3PictureType(19, "Band/artist logotype"),
                new ID3PictureType(20, "Publisher/Studio logotype")
            };

            pictureTypes.AddRange(pictureTypeList);
            pictureTypes.Sort();
        }

        public static ID3PictureType[] PictureTypes
        {
            get
            {
                return pictureTypes.ToArray();
            }
        }

        
        public static ID3PictureType GetPictureType(int pictureTypeId)
        {
            if (!IsValidPictureTypeId(pictureTypeId))
            {
                throw new FormatException("Invalid pictureTypeId value.  Value must be between 0 and 20.");
            }


            for (int count = 0; count < PictureTypes.Length; count++)
            {
                if (PictureTypes[count].PictureTypeId == pictureTypeId)
                {
                    return PictureTypes[count];
                }
            }

            return null;
        }

        private static bool IsValidPictureTypeId(int pictureTypeId)
        {
            return pictureTypeId >= 0 && pictureTypeId <= 20;
        }


        public static ID3PictureType GetPictureType(string pictureType)
        {
            for (int count = 0; count < PictureTypes.Length; count++)
            {
                if (PictureTypes[count].PictureType == pictureType)
                {
                    return PictureTypes[count];
                }
            }

            return GetPictureType(0);
        }
    }
}
