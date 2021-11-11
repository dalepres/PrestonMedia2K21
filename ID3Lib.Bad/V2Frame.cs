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
            HeaderLength = 10;
        }


        public V2Frame(string frameId)
            : this()
        {
            this.FrameId = frameId;
        }


        public V2Frame(string frameId, byte[] frameValue)
            : this()
        {
            if (frameValue == null || frameValue.Length == 0)
            {
                throw new ArgumentException("frameValue length must be at least 1.", "frameValue");
            }

            if (frameId.Length != 4)
            {
                throw new ArgumentException("frameId length must be 4 characters long.", "frameId");
            }

            this.FrameValue = frameValue;
            this.FrameId = frameId;
        }


        public V2Frame(FileStream fs, byte[] headerBytes)
            : this()
        {
            this.FrameId = Encoding.UTF8.GetString(headerBytes, 0, 4);
            this.Flags = new byte[] { headerBytes[8], headerBytes[9] };
            byte[] sizeBytes = new byte[4];
            Array.Copy(headerBytes, 4, sizeBytes, 0, 4);
            int frameLength = Utilities.ReadInt32(sizeBytes);

            byte[] valueBytes = new byte[frameLength];
            fs.Read(valueBytes, 0, frameLength);

            this.FrameValue = valueBytes;
        }


        protected string GetDescriptionForCulture()
        {
            ResourceManager rm = new ResourceManager("ID3Lib.Resources", 
                Assembly.GetExecutingAssembly());
            return rm.GetString("FrameDesc" + this.FrameId, new CultureInfo("es-MX"));
                //System.Threading.Thread.CurrentThread.CurrentUICulture);
        }

        protected override void WriteHeader(MemoryStream ms)
        { 
            ms.Write(this.Encoding.GetBytes(this.FrameId),0,4);
            ms.Write(Utilities.Int32ToByteArray(this.FrameValue.Length), 0, 4);
            ms.Write(Flags, 0, 2);
        }

        #region ICloneable Members

        public virtual object Clone()
        {
            V2Frame frame = (V2Frame)this.MemberwiseClone();

            byte encodingByte = this.GetEncodingByte();

            switch (encodingByte)
            {
                case 0 :
                    frame.SetEncoding(EncodingTypes.ISO8859);
                    break;
                case 1 :
                    frame.SetEncoding(EncodingTypes.Unicode);
                    break;
                default :
                    throw new Exception("Invalid Encoding byte.");
            }

            UpdateFrameValue();
            frame.FrameValue = (byte[])this.FrameValue.Clone();

            return frame;
        }

        #endregion
    }
}
