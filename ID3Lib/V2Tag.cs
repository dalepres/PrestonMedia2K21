namespace ID3Lib
{
    using System;
    using System.IO;
    using System.Text;

    public class V2Tag
    {
        private byte flags;
        private ID3V2FramesCollection<V2Frame> frames;
        private bool hasExtendedHeader;
        protected byte[] id3Bytes;
        private bool isExperimental;
        private byte majorVersion;
        protected const int MIN_FRAME_SIZE = 11;
        private byte minorVersion;
        private bool usesUnSynchronization;

        public V2Tag()
        {
            this.majorVersion = 0;
            this.minorVersion = 0;
            this.flags = 0;
            this.usesUnSynchronization = false;
            this.hasExtendedHeader = false;
            this.isExperimental = false;
            this.id3Bytes = new byte[] { 0x49, 0x44, 0x33 };
            this.frames = new ID3V2FramesCollection<V2Frame>();
        }

        public V2Tag(FileStream fs, byte[] headerBytes)
        {
            this.majorVersion = 0;
            this.minorVersion = 0;
            this.flags = 0;
            this.usesUnSynchronization = false;
            this.hasExtendedHeader = false;
            this.isExperimental = false;
            this.id3Bytes = new byte[] { 0x49, 0x44, 0x33 };
            this.frames = new ID3V2FramesCollection<V2Frame>();
            this.MajorVersion = headerBytes[3];
            this.MinorVersion = headerBytes[4];
            int flags = headerBytes[5];
            int tagSize = GetTagSize(headerBytes);
            long nextPossibleTagPosition = fs.Position + tagSize;
            long tagEndPosition = nextPossibleTagPosition - 11L;
            this.ParseFlags(headerBytes[5]);
            if (this.HasExtendedHeader)
            {
                byte[] exHeader = this.PopExtendedHeader(fs);
            }
            for (bool frameAdded = true; (fs.Position < tagEndPosition) && frameAdded; frameAdded = this.AddFrame(fs))
            {
            }
            fs.Position = nextPossibleTagPosition;
        }

        private bool AddFrame(FileStream fs)
        {
            byte[] headerBytes = new byte[10];
            if ((fs.Read(headerBytes, 0, 10) == 10) && (headerBytes[0] > 0))
            {
                if (Encoding.UTF8.GetString(headerBytes, 0, 4) == "COMM")
                {
                }
                V2Frame frame = V2FrameFactory.CreateFrame(fs, this.majorVersion, headerBytes);
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
            if (this.isExperimental)
            {
                this.flags = (byte) (this.flags | 0x20);
            }
            if (this.hasExtendedHeader)
            {
                this.flags = (byte) (this.flags | 0x40);
            }
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

        public int GetLength()
        {
            MemoryStream ms = new MemoryStream();
            foreach (V2Frame frame in this.frames)
            {
                frame.Write(ms);
            }
            return (int) ms.Length;
        }

        public static int GetTagSize(byte[] headerBytes)
        {
            byte[] sizeBytes = new byte[4];
            Array.Copy(headerBytes, 6, sizeBytes, 0, 4);
            return Utilities.ReadInt28(sizeBytes);
        }

        private void ParseFlags(byte flag)
        {
            this.hasExtendedHeader = (flag & 0x40) == 1;
            this.isExperimental = (flag & 0x20) == 1;
            this.usesUnSynchronization = (flag & 0x80) == 1;
        }

        private byte[] PopExtendedHeader(FileStream fs)
        {
            byte[] exSize = new byte[4];
            fs.Read(exSize, 0, 4);
            int exHeaderSize = Utilities.ReadInt32(exSize);
            byte[] exHeader = new byte[exSize.Length + exHeaderSize];
            exSize.CopyTo(exHeader, 0);
            fs.Read(exHeader, 4, exHeaderSize);
            return exHeader;
        }

        internal virtual int Write(FileStream fs)
        {
            int numBytes;
            MemoryStream ms = new MemoryStream();
            foreach (V2Frame frame in this.frames)
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

        public ID3V2FramesCollection<V2Frame> Frames
        {
            get
            {
                return this.frames;
            }
        }

        public bool HasExtendedHeader
        {
            get
            {
                return this.hasExtendedHeader;
            }
            set
            {
                this.hasExtendedHeader = value;
            }
        }

        public bool IsExperimental
        {
            get
            {
                return this.isExperimental;
            }
            set
            {
                this.isExperimental = value;
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

