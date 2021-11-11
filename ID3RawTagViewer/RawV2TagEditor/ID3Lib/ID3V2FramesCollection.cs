using System;
using System.Collections.Generic;
using System.Text;

namespace ID3Lib
{
    public class ID3V2FramesCollection<T> : IEnumerable<T> where T : V2Frame
    {
        List<T> framesList = new List<T>();

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return framesList.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return framesList.GetEnumerator();
        }

        #endregion

        public V2Frame this[int index]
        {
            get
            {
                if (index < framesList.Count)
                {
                    return framesList[index];
                }
                else
                {
                    return null;
                }
            }

            set
            {
                if (index < this.Count)
                {
                    framesList[index] = (T)value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        
        public IEnumerator<V2Frame> this[string frameId]
        {
            get
            {
                foreach (T frame in framesList)
                {
                    if (frame.FrameId == frameId)
                    {
                        yield return frame;
                    }
                }
            }
        }


        public V2Frame GetFrame(string frameId, int index)
        {
            int x = 0;
            foreach (T frame in framesList)
            {
                if (frame.FrameId == frameId)
                {
                    if (x++ == index)
                        return frame;
                }
            }

            return null;
        }

        public int Count
        {
            get { return this.framesList.Count; }
        }

        public V2Frame GetFrame(string frameId)
        {
            return GetFrame(frameId, 0);
        }


        public void Add(V2Frame frame)
        {
            if (ValidateFrame(frame))
            {
                framesList.Add((T)frame);
            }
        }

        private bool ValidateFrame(V2Frame frame)
        {
            return true;
        }

        public bool Remove(string frameId)
        {
            int count = 0;
            while (count < framesList.Count)
            //foreach (T frame in framesList)
            {
                T frame = framesList[count++];
                if (frame.FrameId == frameId)
                {
                    return framesList.Remove(frame);
                }
            }

            return false;
        }

        public void RemoveAt(int index)
        {
            framesList.RemoveAt(index);
        }
    }
}
