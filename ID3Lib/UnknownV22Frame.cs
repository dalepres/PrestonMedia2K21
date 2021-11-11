namespace ID3Lib
{
    using System;

    public class UnknownV22Frame : V22Frame
    {
        public UnknownV22Frame(string frameId, byte[] frameValue) : base(frameId, frameValue)
        {
        }
    }
}

