using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ID3Lib
{
    public class V2Tag
    {
        protected const int MIN_FRAME_SIZE = 11;
        private byte majorVersion = 0;
        private byte minorVersion = 0;
        private byte flags = 0;
        private bool usesUnSynchronization = false;
        private bool hasExtendedHeader = false;
        private bool isExperimental = false;
        protected byte[] id3Bytes = new byte[] { 73, 68, 51 };

        private ID3V2FramesCollection<V2Frame> frames = new ID3V2FramesCollection<V2Frame>();

        public V2Tag()
        {
        }


        public V2Tag(FileStream fs, byte[] headerBytes)
        {
            this.MajorVersion = headerBytes[3];
            this.MinorVersion = headerBytes[4];
            int flags = headerBytes[5];

            int tagSize = GetTagSize(headerBytes);

            long nextPossibleTagPosition = fs.Position + tagSize;
            long tagEndPosition = nextPossibleTagPosition - MIN_FRAME_SIZE;

            ParseFlags(headerBytes[5]);

            if (HasExtendedHeader)
            {
                byte[] exHeader = PopExtendedHeader(fs);
            }

            bool frameAdded = true;
            while ((fs.Position < tagEndPosition) && frameAdded)
            {
                frameAdded = AddFrame(fs);
            }

            fs.Position = nextPossibleTagPosition;
        }

        public static int GetTagSize(byte[] headerBytes)
        {
            byte[] sizeBytes = new byte[4];
            Array.Copy(headerBytes, 6, sizeBytes, 0, 4);
            int tagSize = Utilities.ReadInt28(sizeBytes);
            return tagSize;
        }


        private void ParseFlags(byte flag)
        {

            this.hasExtendedHeader = ((flag & 64) == 1);
            this.isExperimental = ((flag & 32) == 1);
            this.usesUnSynchronization = ((flag & 128) == 1);
        }


        public ID3V2FramesCollection<V2Frame> Frames
        {
            get { return this.frames; }
        }


        public int GetLength()
        {
            // TODO:  Replace this with getting the sum of the length of each frame.  Add GetLength() to V2Frame
            MemoryStream ms = new MemoryStream();
            foreach (V2Frame frame in this.frames)
            {
                frame.Write(ms);
            }

            return (int)ms.Length;
        }


        protected static byte[] GetHeaderBytes(FileStream fs)
        {
            long position = fs.Position;
            byte[] bytes = new byte[10];
            fs.Read(bytes, 0, 10);

            if (!ID3Tag.IsId3Header(bytes))
            {
                fs.Position = position;
                return null;
            }

            return bytes;
        }

        public static byte[] GetTagsRaw(FileStream fs)
        {
            MemoryStream ms = new MemoryStream();

            fs.Position = 0;
            byte[] headerBytes = GetHeaderBytes(fs);

            while (headerBytes != null)
            {
                byte[] tagBytes;
                int tagLength;

                int length = Utilities.ReadInt28(GetSizeBytes(headerBytes));
                tagBytes = new byte[length];
                tagLength = fs.Read(tagBytes, 0, length);
                if (tagLength == length)
                {
                    ms.Write(headerBytes, 0, headerBytes.Length);
                    ms.Write(tagBytes, 0, tagLength);
                }

                headerBytes = GetHeaderBytes(fs);
            }

            return ms.ToArray();
        }

        private static byte[] GetSizeBytes(byte[] headerBytes)
        {
            return new byte[] {
                headerBytes[6],
                headerBytes[7],
                headerBytes[8],
                headerBytes[9]};
        }

        private bool AddFrame(FileStream fs)
        {
            V2Frame frame = V2Frame.FrameFactory(fs, majorVersion);
            if (frame != null)
            {
                this.Frames.Add(frame);
                return true;
            }

            return false;
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


        internal virtual int Write(Stream fs)
        {
            MemoryStream ms = new MemoryStream();
            foreach(V2Frame frame in this.frames)
            {
                frame.Write(ms);
            }

            WriteHeader(fs, (int)ms.Length);

            ms.Position = 0;
            const int size = 4096;
            byte[] bytes = new byte[4096];
            int numBytes;
            while ((numBytes = ms.Read(bytes, 0, size)) > 0)
                fs.Write(bytes, 0, numBytes);

            return (int)ms.Length + 10;
        }


        protected virtual void WriteHeader(Stream fs, int length)
        {
            fs.Write(this.id3Bytes, 0, 3);
            fs.WriteByte(majorVersion);
            fs.WriteByte(minorVersion);
            fs.WriteByte(GetFlags());
            fs.Write(Utilities.Int28ToByteArray(length), 0, 4);
        }


        protected virtual byte GetFlags()
        {
            this.flags = 0;
            if (this.isExperimental)
                flags |= 32;
            if (this.hasExtendedHeader)
                flags |= 64;
            if (this.usesUnSynchronization)
                flags |= 128;

            return flags;
        }

        
        public byte MajorVersion
        {
            get { return majorVersion; }
            set { majorVersion = value; }
        }


        public byte MinorVersion
        {
            get { return minorVersion; }
            set { minorVersion = value; }
        }


        public byte Flags
        {
            get { return flags; }
            set { flags = value; }
        }


        public bool UsesUnSynchronization
        {
            get { return usesUnSynchronization; }
            set { usesUnSynchronization = value; }
        }


        public bool HasExtendedHeader
        {
            get { return hasExtendedHeader; }
            set { hasExtendedHeader = value; }
        }


        public bool IsExperimental
        {
            get { return isExperimental; }
            set { isExperimental = value; }
        }
    }
}
