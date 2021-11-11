using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace ID3Lib
{
    public class V22Frame : V2Frame
    {
        const int HEADER_LENGTH = 6;
        //private byte[] flags = new byte[] { 0, 0 };

        public V22Frame()
        {
        }

        public V22Frame(Stream stream)
        {
            byte[] headerBytes = GetHeaderBytes(stream);
            if (headerBytes != null)
            {
                InitializeFrame(stream, headerBytes);
            }
            else
            {
                throw new Exception("Invalid V22Frame.  Could not read header.");
            }
        }

        private byte[] GetHeaderBytes(Stream stream)
        {
            byte[] headerBytes = new byte[HEADER_LENGTH];
            int bytesRead = stream.Read(headerBytes, 0, HEADER_LENGTH);
            if (bytesRead == HEADER_LENGTH)
            {
                return headerBytes;
            }
            else
            {
                return null;
            }
        }

        public V22Frame(Stream stream, byte[] headerBytes)
        {
            InitializeFrame(stream, headerBytes);
        }

        private void InitializeFrame(Stream stream, byte[] headerBytes)
        {
            byte[] sizeBytes = new byte[3];
            Array.Copy(headerBytes, 3, sizeBytes, 0, 3);
            int frameLength = Utilities.ReadInt24(sizeBytes);

            if (frameLength > (stream.Length - stream.Position))
            {
                throw new Exception("Invalid frame length: " + frameLength.ToString());
            }

            byte[] valueBytes = new byte[frameLength];
            stream.Read(valueBytes, 0, frameLength);

            this.FrameValue = valueBytes;
            this.FrameId = Encoding.UTF8.GetString(headerBytes, 0, 3);
        }


        public override string ToString()
        {
            return this.FrameId;
        }


        protected override void WriteHeader(MemoryStream ms)
        {
            ms.Write(this.Encoding.GetBytes(this.FrameId),0,3);
            ms.Write(Utilities.Int24ToByteArray(FrameValue.Length), 0, 3);
        }
    }
}
