using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ID3Lib
{
    public class V22Tag
    {
        protected const int MIN_FRAME_SIZE = 11;
        private byte majorVersion = 2;
        private byte minorVersion = 0;
        private byte flags = 0;
        private bool usesUnSynchronization = false;
        protected byte[] id3Bytes = new byte[] { 73, 68, 51 };

        private ID3V22FramesCollection<V22Frame> frames = new ID3V22FramesCollection<V22Frame>();

        public V22Tag()
        {
        }


        public V22Tag(FileStream fs, byte[] headerBytes)
        {
            this.MajorVersion = headerBytes[3];
            this.MinorVersion = headerBytes[4];
            int flags = headerBytes[5];

            byte[] sizeBytes = new byte[4];
            Array.Copy(headerBytes, 6, sizeBytes, 0, 4);
            int tagSize = Utilities.ReadInt28(sizeBytes);

            long nextPossibleTagPosition = fs.Position + tagSize;
            long tagEndPosition = nextPossibleTagPosition - MIN_FRAME_SIZE;

            ParseFlags(headerBytes[5]);

            bool frameAdded = true;
            while ((fs.Position < tagEndPosition) && frameAdded)
            {
                frameAdded = AddFrame(fs);
            }

            fs.Position = nextPossibleTagPosition;
        }


        private void ParseFlags(byte flag)
        {
            this.usesUnSynchronization = ((flag & 128) == 1);
        }


        public override string ToString()
        {
            return "ID3 Version 2.2 Tag";
        }

        public ID3V22FramesCollection<V22Frame> Frames
        {
            get { return this.frames; }
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


        private bool AddFrame(FileStream fs)
        {
            byte[] headerBytes = new byte[6];
            int bytesRead = fs.Read(headerBytes, 0, 6);
            //byte[] sizeBytes;
			
            //headerBytes.CopyTo(sizeBytes, 3);

            if (bytesRead == 6 && headerBytes[0] > 0)
            {
                string frameId = Encoding.UTF8.GetString(headerBytes, 0, 3);
                //int size = ByteArrayToInt(sizeBytes);

                V22Frame frame = null;
                try
                {
                    frame = V22FrameFactory.CreateFrame(fs, majorVersion, headerBytes);
                }
                catch (InvalidCommFrameException e) { }

                if (frame != null)
                    this.Frames.Add(frame);
                return true;
            }

            return false;
        }

        //private int ByteArrayToInt(byte[] bytes)
        //{
        //    if (buf.length != 4)
        //        throw new ArgumentException("Bad Length");
        //    return ((b[0] & 0xff) << 24) | ((b[1] & 0xff) << 16) |
        //    ((b[2] & 0xff) << 8) | (b[3]);
        //}

        internal virtual int Write(FileStream fs)
        {
            MemoryStream ms = new MemoryStream();
            foreach(V22Frame frame in this.frames)
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


        private void WriteHeader(FileStream fs, int length)
        {
            fs.Write(this.id3Bytes, 0, 3);
            fs.WriteByte(majorVersion);
            fs.WriteByte(minorVersion);
            fs.WriteByte(GetFlags());
            fs.Write(Utilities.Int28ToByteArray(length), 0, 4);
        }


        private byte GetFlags()
        {
            this.flags = 0;
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


        public static explicit operator V23Tag(V22Tag v22Tag)
        {
            return new V23Tag(((ID3V2FramesCollection<V2Frame>)v22Tag.Frames));
        }
    }
}
