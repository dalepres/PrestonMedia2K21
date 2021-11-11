using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace ID3Lib
{
    public class V2Frame : Frame, ICloneable
    {
        public V2Frame()
        {
        }

        public V2Frame(byte[] headerBytes, byte[] frameValue)
        {
            InitializeFrame(headerBytes, frameValue);
        }

        public void InitializeFrame(byte[] headerBytes, byte[] frameValue)
        {
            this.HeaderLength = headerBytes.Length;
            this.HeaderBytes = headerBytes;
            this.FrameValue = frameValue;
        }

        #region ICloneable Members

        public virtual object Clone()
        {
            V2Frame frame = (V2Frame)this.MemberwiseClone();

            byte encodingByte = this.GetEncodingByte();

            switch (encodingByte)
            {
                case 0 :
                    frame.SetEncoding(EncodingType.ISO8859);
                    break;
                case 1 :
                    frame.SetEncoding(EncodingType.Unicode);
                    break;
                default :
                    throw new Exception("Invalid Encoding byte.");
            }

            UpdateFrameValue();
            frame.FrameValue = (byte[])this.FrameValue.Clone();

            return frame;
        }

        #endregion

        internal static V2Frame FrameFactory(Stream fs, byte majorVersion)
        {
            V2Frame frame = null;
            switch (majorVersion)
            {
                case 2 :
                    frame = new V22Frame(fs);
                    break;
                case 3:
                    frame = new V23Frame(fs);
                    break;
            }

            if (frame.FrameId.StartsWith("\0"))
            {
                return null;
            }

            return frame;
        }
    }
}
