namespace ID3Lib
{
    using System;

    public class UnknownV2Frame : V2Frame
    {
        public UnknownV2Frame(string frameId, byte[] frameValue) : base(frameId, frameValue)
        {
        }
    }
}

