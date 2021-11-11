using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ID3Lib
{
    public partial class V22Tag : V2Tag, ICloneable
    {
        public V22Tag()
        {
            MajorVersion = 2;
            MinorVersion = 0;
        }

        public V22Tag(FileStream fs, byte[] headerBytes)
            : base(fs, headerBytes)
        {
        }

        public V22Tag(ID3V22FramesCollection<V22Frame> frames)
            : this()
        {
            foreach (V22Frame frame in frames)
            {
                Frames.Add(frame);
            }
        }

        public override string ToString()
        {
            return "ID3 Version 2.2 Tag";
        }

        public static V22Tag GetV22Tag(FileStream fs)
        {
            long position = fs.Position;
            byte[] headerBytes = GetHeaderBytes(fs);

            if (headerBytes == null)
            {
                fs.Position = position;
                return null;
            }

            return new V22Tag(fs, headerBytes);
        }


        internal override int Write(Stream fs)
        {
            MemoryStream ms = new MemoryStream();
            foreach (V22Frame frame in this.Frames)
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


        protected override void WriteHeader(Stream fs, int length)
        {
            fs.Write(this.id3Bytes, 0, 3);
            fs.WriteByte(MajorVersion);
            fs.WriteByte(MinorVersion);
            fs.WriteByte(GetFlags());
            fs.Write(Utilities.Int28ToByteArray(length), 0, 4);
        }


        protected override byte GetFlags()
        {
            this.Flags = 0;
            if (this.IsExperimental)
                Flags |= 32;
            if (this.HasExtendedHeader)
                Flags |= 64;
            if (this.UsesUnSynchronization)
                Flags |= 128;

            return Flags;
        }

        #region ICloneable Members

        public object Clone()
        {
            V22Tag tag = (V22Tag)this.MemberwiseClone();

            for (int count = 0; count < this.Frames.Count; count++)
            {
                byte[] newValue = (byte[])((V22Frame)this.Frames[count]).FrameValue.Clone();
                byte[] newHeader = (byte[])((V22Frame)this.Frames[count]).HeaderBytes.Clone();
                tag.Frames[count] = new V2Frame(newHeader, newValue);
            }

            tag.id3Bytes = (byte[])this.id3Bytes.Clone();

            return tag;
        }

        #endregion
    }
}
