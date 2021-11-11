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


        public static explicit operator ID3V2FramesCollection<V2Frame>(ID3V22FramesCollection<T> v22Frames)
        {
            ID3V2FramesCollection<V2Frame> v23frames = new ID3V2FramesCollection<V2Frame>();

            foreach (V22Frame frame in v22Frames)
            {
                try
                {
                    if (frame != null && frame.FrameId != "PIC")
                    {
                        V2Frame v23frame = V22Frame.ConvertToV23Frame(frame);
                        if (v23frame != null)
                        {
                            v23frames.Add(v23frame);
                        }
                    }
                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                }
            }


            // TODO:  fix the COMMFrame class and the following code
            //        so I can add a comment to 2.3 tags converted from 2.2.
            //COMMFrame f = new COMMFrame();
            //f.FrameId = "COMM";
            //f.Text = "Converted to V2.3 from V2.2 by ID3Lib on "
            //    + DateTime.Now.ToString();
            //v23frames.Add(f);

            return v23frames;
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

        public void SetTextFrame(string frameId, string frameText, int id3Encoding)
        {
            if (frameText.Length == 0)
            {
                Remove(frameId);
            }

            V22Frame frame = GetFrame(frameId);
            if (frame == null)
            {
                this.Add(V22FrameFactory.CreateTextFrame(frameId, frameText, id3Encoding));
            }
            else
            {
                ((V22TextFrame)frame).Text = frameText;
            }
        }
    }
}
