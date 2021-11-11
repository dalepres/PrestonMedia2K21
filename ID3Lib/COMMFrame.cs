namespace ID3Lib
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class COMMFrame : V2Frame
    {
        private string comment;
        private string description;
        private string language;

        private COMMFrame()
        {
            this.language = "eng";
            base.FrameId = "COMM";
        }

        public COMMFrame(byte[] frameValue)
        {
            this.language = "eng";
            base.FrameId = "COMM";
            base.FrameValue = frameValue;
            this.ParseFrameValue();
        }

        public COMMFrame(string language, string description, string comment)
        {
            this.language = "eng";
            this.language = language;
            this.description = description;
            this.comment = comment;
            this.SetFrameValue();
        }

        internal override int GetLength()
        {
            this.SetFrameValue();
            return (base.HeaderLength + base.FrameValue.Length);
        }

        private void ParseFrameValue()
        {
            base.SetEncoding((EncodingTypes) base.FrameValue[0]);
            byte[] languageBytes = new byte[3];
            Array.Copy(base.FrameValue, 1, languageBytes, 0, 3);
            this.language = Encoding.ASCII.GetString(languageBytes);
            byte[] stringBytes = new byte[base.FrameValue.Length - 4];
            Array.Copy(base.FrameValue, 4, stringBytes, 0, stringBytes.Length);
            string[] strings = Frame.ParseNullTerminatedStringList(stringBytes, base.FrameValue[0]);

            if (LooksLikeAppleCommFrame(strings))
            {
                strings = FixAppleCommFrameStrings(strings);
            }

            if (strings.Length != 2)
            {
                throw new ArgumentException("FrameValue does not represent a valid COMMFrame.");
            }
            this.description = strings[0];
            this.comment = strings[1];
        }

        private string[] FixAppleCommFrameStrings(string[] strings)
        {
            return new string[] { strings[0], strings[1] };
        }

        private static bool LooksLikeAppleCommFrame(string[] strings)
        {
            // Apple appends an extra terminating byte (0) at the end of the frame
            // causing the strings length to be three with the last string an empty string.
            // This is in violation of the ID3 V2.3 standard but it's Apple and too widespread
            // to fix in any other way except to handle it.
            return strings.Length == 3 && strings[2].Length == 0;
        }

        private void SetFrameValue()
        {
            List<byte> bytes = new List<byte>();
            bytes.Add(base.GetEncodingByte());
            bytes.AddRange(Encoding.ASCII.GetBytes(this.language));
            bytes.AddRange(base.BOM);
            bytes.AddRange(base.Encoding.GetBytes(this.description));
            bytes.AddRange(base.StringTerminator);
            bytes.AddRange(base.Encoding.GetBytes(this.comment));
            base.FrameValue = bytes.ToArray();
        }

        internal override int Write(MemoryStream ms)
        {
            this.SetFrameValue();
            return base.Write(ms);
        }

        protected override void WriteValue(MemoryStream ms)
        {
            this.SetFrameValue();
            base.WriteValue(ms);
        }

        public string Comment
        {
            get
            {
                return this.comment;
            }
            set
            {
                this.comment = value.Replace("\r\n", "\n");
                this.SetFrameValue();
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
                this.SetFrameValue();
            }
        }

        public string Language
        {
            get
            {
                return this.language;
            }
            set
            {
                this.language = value;
                this.SetFrameValue();
            }
        }
    }
}

