namespace ID3Lib
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Resources;
    using System.Text;

    public class V2Frame : Frame, ICloneable
    {
        public V2Frame()
        {
            base.HeaderLength = 10;
        }

        public V2Frame(FileStream fs, byte[] headerBytes) : this()
        {
            base.FrameId = Encoding.UTF8.GetString(headerBytes, 0, 4);
            base.Flags = new byte[] { headerBytes[8], headerBytes[9] };
            byte[] sizeBytes = new byte[4];
            Array.Copy(headerBytes, 4, sizeBytes, 0, 4);
            int frameLength = Utilities.ReadInt32(sizeBytes);
            byte[] valueBytes = new byte[frameLength];
            fs.Read(valueBytes, 0, frameLength);
            base.FrameValue = valueBytes;
        }

        public V2Frame(string frameId, byte[] frameValue) : this()
        {
            if ((frameValue == null) || (frameValue.Length == 0))
            {
                throw new ArgumentException("frameValue length must be at least 1.", "frameValue");
            }
            if (frameId.Length != 4)
            {
                throw new ArgumentException("frameId length must be 4 characters long.", "frameId");
            }
            base.FrameValue = frameValue;
            base.FrameId = frameId;
        }

        public virtual object Clone()
        {
            V2Frame frame = (V2Frame) base.MemberwiseClone();
            switch (base.GetEncodingByte())
            {
                case 0:
                    frame.SetEncoding(EncodingTypes.ISO8859);
                    break;

                case 1:
                    frame.SetEncoding(EncodingTypes.Unicode);
                    break;

                default:
                    throw new Exception("Invalid Encoding byte.");
            }
            this.UpdateFrameValue();
            frame.FrameValue = (byte[]) base.FrameValue.Clone();
            return frame;
        }

        protected string GetDescriptionForCulture()
        {
            ResourceManager rm = new ResourceManager("ID3Lib.Resources", Assembly.GetExecutingAssembly());
            return rm.GetString("FrameDesc" + base.FrameId, new CultureInfo("es-MX"));
        }

        protected override void WriteHeader(MemoryStream ms)
        {
            ms.Write(base.Encoding.GetBytes(base.FrameId), 0, 4);
            ms.Write(Utilities.Int32ToByteArray(base.FrameValue.Length), 0, 4);
            ms.Write(base.Flags, 0, 2);
        }
    }
}

