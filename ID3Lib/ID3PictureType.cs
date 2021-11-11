namespace ID3Lib
{
    using System;
    using System.IO;
    using System.Collections.Generic;

    public class ID3PictureType : IComparable
    {
        private string pictureType;
        private byte pictureTypeId = 0xff;

        public ID3PictureType(byte pictureTypeId, string pictureType)
        {
            this.pictureTypeId = pictureTypeId;
            this.pictureType = pictureType;
        }

        public int CompareTo(object obj)
        {
            if (!(obj is ID3PictureType))
            {
                throw new ArgumentException("Only ID3PictureType objects can be compared using ID3PictureType.CompareTo().");
            }
            return string.Compare(this.pictureType, ((ID3PictureType) obj).PictureType, true);
        }

        public override bool Equals(object obj)
        {
            if (obj is int)
            {
                return (this.pictureTypeId == ((int) obj));
            }
            return ((obj is ID3PictureType) && (this.pictureTypeId == ((ID3PictureType) obj).PictureTypeId));
        }

        public override int GetHashCode()
        {
            return this.pictureType.GetHashCode();
        }

        public override string ToString()
        {
            return this.pictureType;
        }

        public string ToFileNameSafeString()
        {
            return this.ToString().Replace("'", "").Replace(".", "").Replace("/", "-");
        }

        public string PictureType
        {
            get
            {
                return this.pictureType;
            }
            set
            {
                this.pictureType = value;
            }
        }

        public byte PictureTypeId
        {
            get
            {
                return this.pictureTypeId;
            }
            set
            {
                this.pictureTypeId = value;
            }
        }
    }
}

