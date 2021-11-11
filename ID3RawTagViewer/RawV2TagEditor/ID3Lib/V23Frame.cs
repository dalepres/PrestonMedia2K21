using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ID3Lib
{
    public class V23Frame : V2Frame
    {
        public V23Frame(Stream stream)
        {
            //byte[] headerBytes;
            if ((HeaderBytes = GetHeaderBytes(stream)) == null)
            {
                throw new Exception("Invalid V23Frame.  Header could not be read.");
            }

            this.FrameId = Encoding.UTF8.GetString(HeaderBytes, 0, 4);
            this.Flags = new byte[] { HeaderBytes[8], HeaderBytes[9] };
            byte[] sizeBytes = new byte[4];
            Array.Copy(HeaderBytes, 4, sizeBytes, 0, 4);
            int frameLength = Utilities.ReadInt32(sizeBytes);

            if (frameLength > (stream.Length - stream.Position))
            {
                throw new Exception("Invalid frame length: " + frameLength.ToString());
            }

            byte[] valueBytes = new byte[frameLength];
            int bytesRead = stream.Read(valueBytes, 0, frameLength);

            if (bytesRead == frameLength)
            {
                this.FrameValue = valueBytes;
            }
            else
            {
                throw new Exception("Invalid V23Frame.  Frame value could not be read.");
            }
        }

        public V23Frame(byte[] headerBytes, byte[] frameValue)
            : base(headerBytes, frameValue)
        {
        }

        internal byte[] GetHeaderBytes(Stream stream)
        {
            byte[] bytes = new byte[10];
            int headerLength = stream.Read(bytes, 0, 10);

            return headerLength == 10 ? bytes : null;
        }
    }
}
