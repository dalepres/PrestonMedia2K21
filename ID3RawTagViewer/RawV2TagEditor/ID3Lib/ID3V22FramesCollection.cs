using System;
using System.Collections.Generic;
using System.Text;

namespace ID3Lib
{
    public class ID3V22FramesCollection<T> : IEnumerable<T> where T : V22Frame
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


        public V22Frame this[int index]
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
        }


        public IEnumerator<V22Frame> this[string frameId]
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

        public V22Frame GetFrame(string frameId, int index)
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


        public V22Frame GetFrame(string frameId)
        {
            return GetFrame(frameId, 0);
        }

        public void Add(V22Frame frame)
        {
            framesList.Add((T)frame);
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
