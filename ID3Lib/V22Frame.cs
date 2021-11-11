namespace ID3Lib
{
    using System;
    using System.IO;
    using System.Text;

    public abstract class V22Frame : Frame
    {
        public V22Frame()
        {
        }

        public V22Frame(FileStream fs, byte[] headerBytes)
        {
            base.FrameId = Encoding.UTF8.GetString(headerBytes, 0, 3);
            byte[] sizeBytes = new byte[3];
            Array.Copy(headerBytes, 3, sizeBytes, 0, 3);
            int frameLength = Utilities.ReadInt32(sizeBytes);
            byte[] valueBytes = new byte[frameLength];
            fs.Read(valueBytes, 0, frameLength);
            base.FrameValue = valueBytes;
        }

        public V22Frame(string frameId, byte[] frameValue)
        {
            if ((frameValue == null) || (frameValue.Length == 0))
            {
                throw new ArgumentException("frameValue length must be at least 1.", "frameValue");
            }
            if (frameId.Length != 3)
            {
                throw new ArgumentException("frameId length must be 3 characters long.", "frameId");
            }
            base.FrameValue = frameValue;
            base.FrameId = frameId;
        }

        public V2Frame ConvertToV23Frame()
        {
            return ConvertToV23Frame(this);
        }

        public static V2Frame ConvertToV23Frame(V22Frame frame)
        {
            switch (frame.FrameId)
            {
                case "TAL":
                    return (TALBTextFrame) ((TALTextFrame) frame);

                case "TT2":
                    return (TIT2TextFrame) ((TT2TextFrame) frame);

                case "TP2":
                    return (TPE2TextFrame) ((TP2TextFrame) frame);

                case "TPA":
                    return (TPOSTextFrame) ((TPATextFrame) frame);

                case "PIC":
                    return (APICFrame) ((PICFrame) frame);

                case "TP1":
                    return (TPE1TextFrame) ((TP1TextFrame) frame);

                case "TRK":
                    return (TRCKTextFrame) ((TRKTextFrame) frame);

                case "TYE":
                    return (TYERTextFrame) ((TYETextFrame) frame);

                case "TCO":
                    return (TCONTextFrame) ((TCOTextFrame) frame);

                case "COM":
                    return (COMMFrame) ((COMFrame) frame);

                case "MCI":
                    return new UnknownV2Frame("MCDI", frame.FrameValue);

                case "CNT":
                    return new UnknownV2Frame("PCNT", frame.FrameValue);

                case "TCM":
                    return new UnknownV2Frame("TCOM", frame.FrameValue);

                case "TIM":
                    return new UnknownV2Frame("TIME", frame.FrameValue);

                case "TLE":
                    return new UnknownV2Frame("TLEN", frame.FrameValue);

                case "TOF":
                    return new UnknownV2Frame("TOFN", frame.FrameValue);

                case "TP3":
                    return new UnknownV2Frame("TPE3", frame.FrameValue);

                case "CRA":
                    return new UnknownV2Frame("AENC", frame.FrameValue);

                case "EQU":
                    return new UnknownV2Frame("EQUA", frame.FrameValue);

                case "ETC":
                    return new UnknownV2Frame("ETCO", frame.FrameValue);

                case "GEO":
                    return new UnknownV2Frame("GEOB", frame.FrameValue);

                case "IPL":
                    return new UnknownV2Frame("IPLS", frame.FrameValue);

                case "LNK":
                    return new UnknownV2Frame("LINK", frame.FrameValue);

                case "MLL":
                    return new UnknownV2Frame("MLLT", frame.FrameValue);

                case "POP":
                    return new UnknownV2Frame("POPM", frame.FrameValue);

                case "BUF":
                    return new UnknownV2Frame("RBUF", frame.FrameValue);

                case "RVA":
                    return new UnknownV2Frame("RVAD", frame.FrameValue);

                case "REV":
                    return new UnknownV2Frame("RVRB", frame.FrameValue);

                case "SLT":
                    return new UnknownV2Frame("SYLT", frame.FrameValue);

                case "STC":
                    return new UnknownV2Frame("SYTC", frame.FrameValue);

                case "TBP":
                    return new UnknownV2Frame("TBPM", frame.FrameValue);

                case "TCR":
                    return new UnknownV2Frame("TCOP", frame.FrameValue);

                case "TDA":
                    return new UnknownV2Frame("TDAT", frame.FrameValue);

                case "TDY":
                    return new UnknownV2Frame("TDLY", frame.FrameValue);

                case "TEN":
                    return new UnknownV2Frame("TENC", frame.FrameValue);

                case "TXT":
                    return new UnknownV2Frame("TEXT", frame.FrameValue);

                case "TFT":
                    return new UnknownV2Frame("TFLT", frame.FrameValue);

                case "TT1":
                    return new UnknownV2Frame("TIT1", frame.FrameValue);

                case "TT3":
                    return new UnknownV2Frame("TIT3", frame.FrameValue);

                case "TKE":
                    return new UnknownV2Frame("TKEY", frame.FrameValue);

                case "TLA":
                    return new UnknownV2Frame("TLAN", frame.FrameValue);

                case "TMT":
                    return new UnknownV2Frame("TMED", frame.FrameValue);

                case "TOT":
                    return new UnknownV2Frame("TOAL", frame.FrameValue);

                case "TOL":
                    return new UnknownV2Frame("TOLY", frame.FrameValue);

                case "TOA":
                    return new UnknownV2Frame("TOPE", frame.FrameValue);

                case "TOR":
                    return new UnknownV2Frame("TORY", frame.FrameValue);

                case "TP4":
                    return new UnknownV2Frame("TPE4", frame.FrameValue);

                case "TPB":
                    return new UnknownV2Frame("TPUB", frame.FrameValue);

                case "TRD":
                    return new UnknownV2Frame("TRDA", frame.FrameValue);

                case "TSI":
                    return new UnknownV2Frame("TSIZ", frame.FrameValue);

                case "TRC":
                    return new UnknownV2Frame("TSRC", frame.FrameValue);

                case "TSS":
                    return new UnknownV2Frame("TSSE", frame.FrameValue);

                case "TXX":
                    return new UnknownV2Frame("TXXX", frame.FrameValue);

                case "UFI":
                    return new UnknownV2Frame("UFID", frame.FrameValue);

                case "ULT":
                    return new UnknownV2Frame("USLT", frame.FrameValue);

                case "WCM":
                    return new UnknownV2Frame("WCOM", frame.FrameValue);

                case "WCP":
                    return new UnknownV2Frame("WCOP", frame.FrameValue);

                case "WAF":
                    return new UnknownV2Frame("WOAF", frame.FrameValue);

                case "WAR":
                    return new UnknownV2Frame("WOAR", frame.FrameValue);

                case "WAS":
                    return new UnknownV2Frame("WOAS", frame.FrameValue);

                case "WPB":
                    return new UnknownV2Frame("WPUB", frame.FrameValue);

                case "WXX":
                    return new UnknownV2Frame("WXXX", frame.FrameValue);
            }
            return null;
        }

        public override string ToString()
        {
            return base.FrameId;
        }

        protected override void WriteHeader(MemoryStream ms)
        {
            ms.Write(base.Encoding.GetBytes(base.FrameId), 0, 3);
            ms.Write(Utilities.Int24ToByteArray(base.FrameValue.Length), 0, 3);
        }
    }
}

