using System;
using System.Collections.Generic;
using System.Text;

namespace ID3Lib
{
    public class COMFrame : V22Frame
    {
        private string description;
        private string comment;
        private string language = "eng";

        public COMFrame(byte[] frameValue)
        {
            base.FrameId = "COM";
            base.FrameValue = frameValue;
            ParseFrameValue();
        }

        public COMFrame(string language, string description, string comment)
            : base()
        {
            this.language = language;
            this.description = description;
            this.comment = comment;
            SetFrameValue();
        }

        internal override int Write(System.IO.MemoryStream ms)
        {
            SetFrameValue();
            return base.Write(ms);
        }

        private void SetFrameValue()
        {
            List<byte> bytes = new List<byte>();
            bytes.Add(this.GetEncodingByte());
            bytes.AddRange(System.Text.Encoding.ASCII.GetBytes(this.language));
            bytes.AddRange(this.BOM);
            bytes.AddRange(this.Encoding.GetBytes(description));
            bytes.AddRange(this.StringTerminator);
            bytes.AddRange(this.Encoding.GetBytes(comment));

            base.FrameValue = bytes.ToArray();
        }


        private COMFrame()
        {
            base.FrameId = "COM";
        }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                SetFrameValue();
            }
        }

        public string Comment
        {
            get { return comment; }
            set
            {
                comment = value.Replace("\r\n", "\n");
                SetFrameValue();
            }
        }

        public string Language
        {
            get { return language; }
            set
            {
                language = value;
                SetFrameValue();
            }
        }

        protected override void WriteValue(System.IO.MemoryStream ms)
        {
            SetFrameValue();
            base.WriteValue(ms);
        }

        internal override int GetLength()
        {
            SetFrameValue();
            return HeaderLength + FrameValue.Length;
        }

        private void ParseFrameValue()
        {
            base.SetEncoding((EncodingTypes)FrameValue[0]);
            byte[] languageBytes = new byte[3];
            Array.Copy(FrameValue, 1, languageBytes, 0, 3);
            language = System.Text.Encoding.ASCII.GetString(languageBytes);
            byte[] stringBytes = new byte[FrameValue.Length - 4];
            Array.Copy(FrameValue, 4, stringBytes, 0, stringBytes.Length);
            string[] strings = Frame.ParseNullTerminatedStringList(stringBytes, FrameValue[0], 2);

            if (strings.Length != 2)
            {
                throw new InvalidCommFrameException("FrameValue does not represent a valid COMFrame.");
            }

            description = strings[0];
            comment = strings[1];
        }


        public static explicit operator COMMFrame(COMFrame frame)
        {
            if (frame != null)
            {
                return new COMMFrame(frame.language, frame.description, frame.comment);
                //return new COMMFrame(frame.FrameValue);
            }

            return null;
        }
    }
}
