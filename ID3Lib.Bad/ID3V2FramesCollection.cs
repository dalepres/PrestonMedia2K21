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
            switch (frame.FrameId)
            {
                case "APIC" :
                    return ValidateApicFrame((APICFrame)frame);
                case "COMM" :
                    return ValidateCommFrame((COMMFrame)frame);
            }

            if (frame is V2TextFrame)
            {
                return ValidateTextFrame(frame);
            }

            return true;
        }

        private bool ValidateCommFrame(COMMFrame frame)
        {
            // TODO: Add COMMFrame validation code
            return true;
        }

        private bool ValidateTextFrame(V2Frame frame)
        {
            // TODO: Add TextFrame validation code
            return true;
        }

        private bool ValidateApicFrame(APICFrame frame)
        {
            foreach (V2Frame f in framesList)
            {
                if (f is APICFrame)
                {
                    APICFrame aFrame = (APICFrame)f;
                    if(aFrame.Equals(frame))
                    {
                        throw new ArgumentException(
                            "Frame already exists: " 
                            + ID3PictureTypes.GetPictureType(frame.PictureType).PictureType 
                            + ", Description: " 
                            + frame.Description);
                    }

                    if ((frame.PictureType == 1 || frame.PictureType == 2)
                        && frame.PictureType == aFrame.PictureType)
                    {
                        throw new ArgumentException(
                            "Image type "
                            + ID3PictureTypes.GetPictureType(frame.PictureType)
                            + " can only be included once and already exists in the current tag.");
                    }

                    if (frame.PictureType == 1 && frame.MimeType != "image/png")
                    {
                        throw new ArgumentException(
                            "Image type "
                            + ID3PictureTypes.GetPictureType(frame.PictureType)
                            + " must be image type Portable Network Graphic (PNG).");
                    }
                }
            }

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


        public void SetTextFrame(string frameId, string frameText, int id3Encoding)
        {
            if (frameText.Length == 0)
            {
                Remove(frameId);
                return;
            }

            V2Frame frame = GetFrame(frameId);
            if (frame == null)
            {
                this.Add(V2FrameFactory.CreateTextFrame(frameId, frameText, id3Encoding));
            }
            else
            {
                ((V2TextFrame)frame).Text = frameText;
            }
        }
    }
}
