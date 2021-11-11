using System;
using System.Collections.Generic;
using System.Text;

namespace ID3Lib
{
    public class COMMFrame : V2Frame
    {
        private string description;
        private string comment;
        private string language = "eng";

        public COMMFrame(byte[] frameValue)
        {
            base.FrameId = "COMM";
            base.FrameValue = frameValue;
            ParseFrameValue();
        }

        public COMMFrame(string language, string description, string comment)
            : base("COMM")
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


        private COMMFrame()
        {
            base.FrameId = "COMM";
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

            string[] strings;
            if (FrameValue.Length == 5 || (FrameValue.Length == 6 && FrameValue[4] == 0 && FrameValue[5] == 0))
            {
                strings = new string[] { "", ""};
            }
            else
            {
                strings = Frame.ParseNullTerminatedStringList(stringBytes, FrameValue[0], 2);
            }

            if (LooksLikeAppleCommFrame(strings))
            {
                // to rollback the FixAppleCommFrameStrings test just
                //remove the try/catch and contents, leaving just the throw
                try
                {
                    FixAppleCommFrameStrings(strings);
                }
                catch
                {
                    throw new LooksLikeAppleCommFrameException();
                }
            }

            if (strings.Length != 2)
            {
                throw new InvalidCommFrameException("FrameValue does not represent a valid COMMFrame.");
            }

            description = strings[0];
            comment = strings[1];
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
    }
}
