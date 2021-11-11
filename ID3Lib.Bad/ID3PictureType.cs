using System;
using System.Collections.Generic;
using System.Text;

namespace ID3Lib
{
    public class ID3PictureType : IComparable
    {
        private byte pictureTypeId = 255;
        private string pictureType;

        public ID3PictureType(byte pictureTypeId, string pictureType)
        {
            this.pictureTypeId = pictureTypeId;
            this.pictureType = pictureType;
        }

        public override string ToString()
        {
            return pictureType;
        }

        public override bool Equals(object obj)
        {
            if (obj is int)
            {
                return pictureTypeId == (int)obj;
            }
            else if (obj is ID3PictureType)
            {
                return pictureTypeId == ((ID3PictureType)obj).PictureTypeId;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return pictureType.GetHashCode();
        }

        public byte PictureTypeId
        {
            get { return pictureTypeId; }
            set { pictureTypeId = value; }
        }

        public string PictureType
        {
            get { return pictureType; }
            set { pictureType = value; }
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            if (obj is ID3PictureType)
            {
                return string.Compare(pictureType, ((ID3PictureType)obj).PictureType, true);
            }
            else
            {
                throw new ArgumentException("Only ID3PictureType objects can be compared using ID3PictureType.CompareTo().");
            }
        }

        #endregion
    }
}
