namespace ID3Lib
{
    using System;
    using System.IO;

    public class V23Tag : V2Tag, ICloneable
    {
        public V23Tag()
        {
            base.MajorVersion = 3;
            base.MinorVersion = 0;
        }

        public V23Tag(ID3V2FramesCollection<V2Frame> frames) : this()
        {
            foreach (V2Frame frame in frames)
            {
                base.Frames.Add(frame);
            }
        }

        public V23Tag(FileStream fs, byte[] headerBytes) : base(fs, headerBytes)
        {
        }

        private static void AddFrameFromText(V23Tag tag, string frameId, string value, int id3Encoding)
        {
            if (value.Trim().Length > 0)
            {
                tag.Frames.Add(V2FrameFactory.CreateTextFrame(frameId, value, id3Encoding));
            }
        }

        public object Clone()
        {
            V23Tag tag = (V23Tag) base.MemberwiseClone();
            for (int count = 0; count < base.Frames.Count; count++)
            {
                tag.Frames[count] = V2FrameFactory.CreateFrame(base.Frames[count].FrameId, (byte[]) base.Frames[count].FrameValue.Clone());
            }
            tag.id3Bytes = (byte[]) base.id3Bytes.Clone();
            return tag;
        }

        public static V23Tag FromTag(object tag, int id3Encoding)
        {
            if (tag is V1Tag)
            {
                return FromV1Tag(tag, id3Encoding);
            }
            if (tag is V22Tag)
            {
                return (V23Tag) ((V22Tag) tag);
            }
            if (tag is V23Tag)
            {
                return (V23Tag) ((V23Tag) tag).Clone();
            }
            return new V23Tag();
        }

        private static V23Tag FromV1Tag(object tag, int id3Encoding)
        {
            V1Tag v1 = (V1Tag) tag;
            V23Tag v23 = new V23Tag();
            AddFrameFromText(v23, "TALB", v1.Album, id3Encoding);
            AddFrameFromText(v23, "TPE1", v1.Artist, id3Encoding);
            AddFrameFromText(v23, "TYER", v1.Year, id3Encoding);
            AddFrameFromText(v23, "TIT2", v1.Title, id3Encoding);
            if (v1.Comment.Trim().Length > 0)
            {
                COMMFrame commFrame = new COMMFrame("eng", "Copied from ID3V1 frame.", v1.Comment.Trim());
                v23.Frames.Add(commFrame);
            }
            if (v1.MajorVersion == 1)
            {
                v23.Frames.Add(V2FrameFactory.CreateTextFrame("TRCK", v1.TrackNumber.ToString(), id3Encoding));
            }
            v23.Frames.Add(V2FrameFactory.CreateTextFrame("TCON", "(" + v1.Genre.ToString() + ")", id3Encoding));
            return v23;
        }

        public static V23Tag GetV23Tag(FileStream fs)
        {
            long position = fs.Position;
            byte[] headerBytes = V2Tag.GetHeaderBytes(fs);
            if (headerBytes == null)
            {
                fs.Position = position;
                return null;
            }
            return new V23Tag(fs, headerBytes);
        }

        public override string ToString()
        {
            return "ID3 Version 2.3 Tag";
        }
    }
}

