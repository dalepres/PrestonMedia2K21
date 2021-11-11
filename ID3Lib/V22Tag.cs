namespace ID3Lib
{
    using System;
    using System.IO;
    using System.Text;

    public class V22Tag
    {
        private byte flags;
        private ID3V22FramesCollection<V22Frame> frames;
        protected byte[] id3Bytes;
        private byte majorVersion;
        protected const int MIN_FRAME_SIZE = 11;
        private byte minorVersion;
        private bool usesUnSynchronization;

        public V22Tag()
        {
            this.majorVersion = 2;
            this.minorVersion = 0;
            this.flags = 0;
            this.usesUnSynchronization = false;
            this.id3Bytes = new byte[] { 0x49, 0x44, 0x33 };
            this.frames = new ID3V22FramesCollection<V22Frame>();
        }

        public V22Tag(FileStream fs, byte[] headerBytes)
        {
            this.majorVersion = 2;
            this.minorVersion = 0;
            this.flags = 0;
            this.usesUnSynchronization = false;
            this.id3Bytes = new byte[] { 0x49, 0x44, 0x33 };
            this.frames = new ID3V22FramesCollection<V22Frame>();
            this.MajorVersion = headerBytes[3];
            this.MinorVersion = headerBytes[4];
            int flags = headerBytes[5];
            byte[] sizeBytes = new byte[4];
            Array.Copy(headerBytes, 6, sizeBytes, 0, 4);
            int tagSize = Utilities.ReadInt28(sizeBytes);
            long nextPossibleTagPosition = fs.Position + tagSize;
            long tagEndPosition = nextPossibleTagPosition - 11L;
            this.ParseFlags(headerBytes[5]);
            for (bool frameAdded = true; (fs.Position < tagEndPosition) && frameAdded; frameAdded = this.AddFrame(fs))
            {
            }
            fs.Position = nextPossibleTagPosition;
        }

        private bool AddFrame(FileStream fs)
        {
            byte[] headerBytes = new byte[6];
            if ((fs.Read(headerBytes, 0, 6) == 6) && (headerBytes[0] > 0))
            {
                string frameId = Encoding.UTF8.GetString(headerBytes, 0, 3);
                V22Frame frame = V22FrameFactory.CreateFrame(fs, this.majorVersion, headerBytes);
                if (frame != null)
                {
                    this.Frames.Add(frame);
                }
                return true;
            }
            return false;
        }

        private byte GetFlags()
        {
            this.flags = 0;
            if (this.usesUnSynchronization)
            {
                this.flags = (byte) (this.flags | 0x80);
            }
            return this.flags;
        }

        protected static byte[] GetHeaderBytes(FileStream fs)
        {
            long position = fs.Position;
            byte[] bytes = new byte[10];
            fs.Read(bytes, 0, 10);
            if (Encoding.UTF8.GetString(bytes, 0, 3) != "ID3")
            {
                fs.Position = position;
                return null;
            }
            return bytes;
        }

        public static explicit operator V23Tag(V22Tag v22Tag)
        {
            return new V23Tag((ID3V2FramesCollection<V2Frame>) v22Tag.Frames);
        }

        private void ParseFlags(byte flag)
        {
            this.usesUnSynchronization = (flag & 0x80) == 1;
        }

        public override string ToString()
        {
            return "ID3 Version 2.2 Tag";
        }

        internal virtual int Write(FileStream fs)
        {
            int numBytes;
            MemoryStream ms = new MemoryStream();
            foreach (V22Frame frame in this.frames)
            {
                frame.Write(ms);
            }
            this.WriteHeader(fs, (int) ms.Length);
            ms.Position = 0L;
            byte[] bytes = new byte[0x1000];
            while ((numBytes = ms.Read(bytes, 0, 0x1000)) > 0)
            {
                fs.Write(bytes, 0, numBytes);
            }
            return (((int) ms.Length) + 10);
        }

        private void WriteHeader(FileStream fs, int length)
        {
            fs.Write(this.id3Bytes, 0, 3);
            fs.WriteByte(this.majorVersion);
            fs.WriteByte(this.minorVersion);
            fs.WriteByte(this.GetFlags());
            fs.Write(Utilities.Int28ToByteArray(length), 0, 4);
        }

        public byte Flags
        {
            get
            {
                return this.flags;
            }
            set
            {
                this.flags = value;
            }
        }

        public ID3V22FramesCollection<V22Frame> Frames
        {
            get
            {
                return this.frames;
            }
        }

        public byte MajorVersion
        {
            get
            {
                return this.majorVersion;
            }
            set
            {
                this.majorVersion = value;
            }
        }

        public byte MinorVersion
        {
            get
            {
                return this.minorVersion;
            }
            set
            {
                this.minorVersion = value;
            }
        }

        public bool UsesUnSynchronization
        {
            get
            {
                return this.usesUnSynchronization;
            }
            set
            {
                this.usesUnSynchronization = value;
            }
        }
    }
}

