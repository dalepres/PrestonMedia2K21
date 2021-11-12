namespace DPMediaPlayer
{
    using System;
    using System.Security.Cryptography;

    public static class ReallyRandom
    {
        private static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

        private static int byteArrayToInt(byte[] bytes)
        {
            return ((((bytes[0] << 0x18) | (bytes[1] << 0x10)) | (bytes[2] << 8)) | bytes[3]);
        }

        private static long byteArrayToLong(byte[] bytes)
        {
            return ((((((((bytes[0] << 0x38) | ((bytes[1] & 0xffL) << 0x30)) | ((bytes[2] & 0xffL) << 40)) | ((bytes[3] & 0xffL) << 0x20)) | ((bytes[4] & 0xffL) << 0x18)) | ((bytes[5] & 0xffL) << 0x10)) | ((bytes[6] & 0xffL) << 8)) | (bytes[7] & 0xffL));
        }

        public static long Next(long maxValue)
        {
            byte[] bytes;
            long rnd;
            if ((maxValue < 0x7fffffffL) && (maxValue > -2147483648L))
            {
                bytes = new byte[4];
                rng.GetBytes(bytes);
                rnd = byteArrayToInt(bytes);
            }
            else
            {
                bytes = new byte[8];
                rng.GetBytes(bytes);
                rnd = byteArrayToLong(bytes);
            }

            return Math.Abs((long) (rnd % maxValue));
        }
    }
}

