using System;
using System.Collections;
using System.Collections.Generic;


namespace Preston.Media
{
    /// <summary>
    /// A collection to handle MediaAttribute objects.  This is required to 
    /// include implementations of IndexOf and the indexer that will avoid
    /// the problem described in Microsoft KnowledgeBase article 823194.
    /// </summary>
    [Serializable]
    public class MediaAttributeCollection : CollectionBase
    {
        public MediaAttributeCollection()
        {
        }

        public MediaAttributeCollection(IList list)
        {
            this.AddRange(list);
        }


        public MediaAttribute this[int index]
        {
            get { return (MediaAttribute)this.List[index]; }
            set
            {
                if (value is MediaAttribute)
                {
                    this.List[index] = value;
                }
                else
                {
                    throw new ArgumentException("Only MediaAttribute objects can be added to MediaAttributeCollection.");
                }
            }
        }

        /// <summary>
        /// An indexer that returns a MediaAttribute object matching the 
        /// integer index or Name provided.
        /// </summary>
        public MediaAttribute this[object obj]
        {
            get
            {
                return (MediaAttribute)this.List[IndexOf(obj)];
            }
            set
            {
                this.List[IndexOf(obj)] = value;
            }
        }


        /// <summary>
        /// Adds a MediaAttribute to the end of the MediaAttributeCollection.
        /// </summary>
        /// <param name="mediaAttribute">The MediaAttribute to add to the MediaAttributeCollection.</param>
        public void Add(MediaAttribute mediaAttribute)
        {
            //mediaAttribute.Site=mediaAttribute.Parent.Site;
            this.List.Add(mediaAttribute);
        }


        /// <summary>
        /// Inserts a MediaAttribute object at the specified index within the MediaAttributeCollection.
        /// </summary>
        /// <param name="index">The location within the MediaAttributeCollection to insert the MediaAttribute.</param>
        /// <param name="mediaAttribute">The MediaAttribute object to insert.</param>
        public void Insert(int index, MediaAttribute mediaAttribute)
        {
            this.List.Insert(index, mediaAttribute);
        }


        /// <summary>
        /// Removes the mediaAttribute passed as a parameter from the MediaAttributeCollection.
        /// </summary>
        /// <param name="mediaAttribute">The MediaAttribute mediaAttribute object to remove.</param>
        public void Remove(MediaAttribute mediaAttribute)
        {
            List.Remove(mediaAttribute);
        }


        /// <summary>
        /// Removes the first mediaAttribute with name matching the provided keyName value.
        /// </summary>
        /// <param name="keyName">The name of the mediaAttribute(s) to remove.</param>
        public void Remove(string keyName)
        {
            IEnumerator enumerator = this.GetEnumerator();
            while (enumerator.MoveNext())
            {
                string name = ((MediaAttribute)enumerator.Current).AttributeName;
                if (string.Compare(keyName, name, true) == 0)
                    this.Remove((MediaAttribute)enumerator.Current);
                break;
            }
        }


        /// <summary>
        /// Determines whether the MediaAttributeCollection contains the mediaAttribute passed
        /// in the mediaAttribute parameter.
        /// </summary>
        /// <param name="mediaAttribute">The name of the mediaAttribute to search for.</param>
        /// <returns>True if found, otherwise false.</returns>
        public bool Contains(MediaAttribute mediaAttribute)
        {
            return this.List.Contains(mediaAttribute);
        }

        /// <summary>
        /// Gets the index of the mediaAttribute matching the integer index or string
        /// name provided.
        /// </summary>
        /// <param name="obj">Must be either an integer or string</param>
        /// <returns>True if a matching mediaAttribute is found in the collection, 
        /// otherwise false.
        /// </returns>
        public int IndexOf(object obj)
        {
            // Return the integer value if obj if it is within the range of 
            // the MediaAttributeCollection count.  Otherwise, return -1 as defined
            // in the IList.IndexOf documentation.
            if (obj is int)
            {
                int index = (int)obj;
                if ((index > -1) && (index < this.Count))
                {
                    return (int)obj;
                }
                else
                {
                    return -1;
                }
            }

            // If the object is a string, search for a MediaAttribute object with 
            // a matching name within the MediaAttributeCollection
            if (obj is string)
            {
                for (int i = 0; i < List.Count; i++)
                    if (string.Compare((string)obj, ((MediaAttribute)List[i]).AttributeName, true) == 0)
                        return i;
                return -1;
            }
            else
            {
                throw new ArgumentException("Only a string or an integer is permitted for the indexer.");
            }
        }

        /// <summary>
        /// Copies the MediaAttributeCollection to a MediaAttribute array.
        /// </summary>
        /// <param name="array">The array to populate with the contents of the MediaAttributeCollection.</param>
        /// <param name="index">Where in the array to begin copying at.</param>
        public void CopyTo(MediaAttribute[] array, int index)
        {
            List.CopyTo(array, index);
        }


        /// <summary>
        /// Determines if there is a mediaAttribute in the MediaAttributeCollection with a name 
        /// matching the value provided.
        /// </summary>
        /// <param name="keyName">The name of the mediaAttribute to search for.</param>
        /// <returns>True if found, otherwise false.</returns>
        public bool Contains(string keyName)
        {
            IEnumerator enumerator = this.GetEnumerator();
            while (enumerator.MoveNext())
            {
                string name = ((MediaAttribute)enumerator.Current).AttributeName;
                if (string.Compare(keyName, name, true) == 0)
                    return true;
            }
            return false;
        }

        internal void AddRange(IList list)
        {
            if (list != null)
            {
                for (int count = 0; count < list.Count; count++)
                {
                    if (list[count] is MediaAttribute)
                    {
                        this.Add((MediaAttribute)list[count]);
                    }
                }
            }
        }

        internal string[] ToStringArray()
        {
            List<string> aList = new List<string>();
            foreach (MediaAttribute attribute in this.List)
            {
                aList.Add(attribute.AttributeName);
            }

            return aList.ToArray();
        }


        internal MediaAttribute[] ToArray()
        {
            List<MediaAttribute> aList = new List<MediaAttribute>();
            foreach (MediaAttribute attribute in this.List)
            {
                aList.Add(attribute);
            }

            return aList.ToArray();
        }
    }
}

