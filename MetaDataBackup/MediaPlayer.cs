using System;
using System.Collections.Generic;
using System.Text;
using WMPLib;
using System.IO;
using System.Xml;

namespace Preston.Media
{
    public class MediaPlayer
    {
        public static System.Diagnostics.TraceSwitch loggingSwitch = new System.Diagnostics.TraceSwitch("LoggingSwitch", "Logging Switch");

        WindowsMediaPlayer player = new WindowsMediaPlayer();
        System.Diagnostics.TraceSwitch infoSwitch = new System.Diagnostics.TraceSwitch("InfoSwitch", "General use switch");

        public MediaPlayer()
        {
            player.MediaError +=
                new WMPLib._WMPOCXEvents_MediaErrorEventHandler(Player_MediaError);
        }

        public IWMPPlaylistArray GetPlayLists()
        {
            string titles = string.Empty;
            IWMPPlaylist list = player.mediaCollection.getByAttribute("SourceURL", @"D:\My Music\ABBA\Gold\12 Fernando.mp3");
            IWMPMedia media;
            List<KeyValuePair<string, string>> attributes = new List<KeyValuePair<string, string>>();
            for (int count = 0; count < list.count; count++)
            {
                media = list.get_Item(count);
                int attCount = media.attributeCount;
                for (int x = 0; x < attCount; x++)
                {
                    string attName = media.getAttributeName(x);
                    attributes.Add(new KeyValuePair<string, string>(
                        attName,
                        media.getItemInfo(attName)));
                }
                titles += media.name + "\r\n";
            }

            return player.playlistCollection.getAll();
        }

        private void Player_MediaError(object pMediaObject)
        {
            throw new Exception(pMediaObject.ToString());
        }

        public WMPLib.WindowsMediaPlayer Player
        {
            get { return player; }
        }

        internal void BackupLibraryData(IWMPMedia media, XmlWriter writer)
        {
            writer.WriteStartElement("MediaItem");

            writer.WriteAttributeString("SourceUrl", media.sourceURL);

            WriteAttributeValue(writer, media, "AcquisitionTime", true);
            WriteAttributeValue(writer, media, "AlbumID", true);
            WriteAttributeValue(writer, media, "AlbumIDAlbumArtist", true);
            WriteAttributeValue(writer, media, "Author", false);
            WriteAttributeValue(writer, media, "AverageLevel", false);
            WriteAttributeValue(writer, media, "Bitrate", true);
            WriteAttributeValue(writer, media, "BuyNow", true);
            WriteAttributeValue(writer, media, "BuyTickets", true);
            WriteAttributeValue(writer, media, "Copyright", true);
            WriteAttributeValue(writer, media, "Duration", true);
            WriteAttributeValue(writer, media, "FileSize", true);
            WriteAttributeValue(writer, media, "FileType", true);
            WriteAttributeValue(writer, media, "Is_Protected", true);
            WriteAttributeValue(writer, media, "MediaType", true);
            WriteAttributeValue(writer, media, "MoreInfo", true);
            WriteAttributeValue(writer, media, "PeakValue", false);
            WriteAttributeValue(writer, media, "ProviderLogoURL", true);
            WriteAttributeValue(writer, media, "ProviderURL", true);
            WriteAttributeValue(writer, media, "RecordingTime", true);
            WriteAttributeValue(writer, media, "ReleaseDate", false);
            WriteAttributeValue(writer, media, "RequestState", false);
            WriteAttributeValue(writer, media, "SourceURL", true);
            WriteAttributeValue(writer, media, "SyncState", true);
            WriteAttributeValue(writer, media, "Title", false);
            WriteAttributeValue(writer, media, "TrackingID", true);
            WriteAttributeValue(writer, media, "UserCustom1", false);
            WriteAttributeValue(writer, media, "UserCustom2", false);
            WriteAttributeValue(writer, media, "UserEffectiveRating", true);
            WriteAttributeValue(writer, media, "UserLastPlayedTime", true);
            WriteAttributeValue(writer, media, "UserPlayCount", false);
            WriteAttributeValue(writer, media, "UserPlaycountAfternoon", false);
            WriteAttributeValue(writer, media, "UserPlaycountEvening", false);
            WriteAttributeValue(writer, media, "UserPlaycountMorning", false);
            WriteAttributeValue(writer, media, "UserPlaycountNight", false);
            WriteAttributeValue(writer, media, "UserPlaycountWeekday", false);
            WriteAttributeValue(writer, media, "UserPlaycountWeekend", false);
            WriteAttributeValue(writer, media, "UserRating", false);
            WriteAttributeValue(writer, media, "UserServiceRating", false);
            WriteAttributeValue(writer, media, "WM/AlbumArtist", false);
            WriteAttributeValue(writer, media, "WM/AlbumTitle", false);
            WriteAttributeValue(writer, media, "WM/Category", false);
            WriteAttributeValue(writer, media, "WM/Composer", false);
            WriteAttributeValue(writer, media, "WM/Conductor", false);
            WriteAttributeValue(writer, media, "WM/ContentDistributor", false);
            WriteAttributeValue(writer, media, "WM/ContentGroupDescription", false);
            WriteAttributeValue(writer, media, "WM/EncodingTime", true);
            WriteAttributeValue(writer, media, "WM/Genre", false);
            WriteAttributeValue(writer, media, "WM/InitialKey", false);
            WriteAttributeValue(writer, media, "WM/Language", false);
            WriteAttributeValue(writer, media, "WM/Lyrics", false);
            WriteAttributeValue(writer, media, "WM/MCDI", true);
            WriteAttributeValue(writer, media, "WM/MediaClassPrimaryID", false);
            WriteAttributeValue(writer, media, "WM/MediaClassSecondaryID", false);
            WriteAttributeValue(writer, media, "WM/Mood", false);
            WriteAttributeValue(writer, media, "WM/ParentalRating", false);
            WriteAttributeValue(writer, media, "WM/Period", false);
            WriteAttributeValue(writer, media, "WM/ProtectionType", false);
            WriteAttributeValue(writer, media, "WM/Provider", true);
            WriteAttributeValue(writer, media, "WM/ProviderRating", true);
            WriteAttributeValue(writer, media, "WM/ProviderStyle", true);
            WriteAttributeValue(writer, media, "WM/Publisher", false);
            WriteAttributeValue(writer, media, "WM/SubscriptionContentID", false);
            WriteAttributeValue(writer, media, "WM/SubTitle", false);
            WriteAttributeValue(writer, media, "WM/TrackNumber", false);
            WriteAttributeValue(writer, media, "WM/UniqueFileIdentifier", false);
            WriteAttributeValue(writer, media, "WM/WMCollectionGroupID", true);
            WriteAttributeValue(writer, media, "WM/WMCollectionID", true);
            WriteAttributeValue(writer, media, "WM/WMContentID", true);
            WriteAttributeValue(writer, media, "WM/Writer", false);

            WriteMediaSearString(writer, media);
            writer.WriteEndElement();
        }

        private void WriteMediaSearString(XmlWriter writer, IWMPMedia media)
        {
            writer.WriteStartElement("MediaSearchString");
            writer.WriteAttributeString("IsReadOnly", "False");
            writer.WriteValue(MakeMediaSearchString(media));
            writer.WriteEndElement();

        }

        private void WriteAttributeValue(XmlWriter writer, IWMPMedia media, string attribute, bool isReadOnly)
        {
            try
            {
                if (loggingSwitch.TraceVerbose)
                {
                    string testValue = RemoveNullChars(media.getItemInfo(attribute));
                    Logger.WriteLine(media.sourceURL + "::" + attribute + "::" + testValue);
                }
            }
            catch
            {
               
            }
            writer.WriteStartElement(attribute.Replace("WM/", "WM_"));

            writer.WriteAttributeString("IsReadOnly", isReadOnly.ToString());

            writer.WriteValue(RemoveNullChars(media.getItemInfo(attribute)));
            writer.WriteEndElement();
        }

        
        public string MakeMediaSearchString(IWMPMedia media)
        {
            string mediaAuthor = RemoveNullChars(media.getItemInfo("Author"))
                .ToLower()
                .Replace("&", "and")
                .Replace("in\'", "ing")
                .Replace(" ", "")
                .Trim()
                ;
            string mediaTitle = RemoveNullChars(media.getItemInfo("Title"))
                .ToLower()
                .Replace("&", "and")
                .Replace("in\'", "ing")
                .Replace(" ", "")
                .Trim()
                ;

            return mediaAuthor + "::" + mediaTitle;
        }

        // Remove the bool TESTMODE parameter below and have only one
        // overload after testing and resolving the null issue
        private string RemoveNullChars(string input)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in input)
            {
                if (c == 0)
                {
                    sb.Append("<NULL>");
                }
                else
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        internal void BackupLibraryData(IWMPMedia media, XmlDocument xDoc)
        {
            List<string> attributesToSave = new List<string>();
            XmlNode mediaNode =  xDoc.CreateNode(XmlNodeType.Element, "MediaItem", "");
            XmlNode filePath = xDoc.CreateNode(XmlNodeType.Attribute, "FilePath", "");
            filePath.Value = media.sourceURL;
            mediaNode.Attributes.SetNamedItem(filePath);
            //albumNode.AppendChild(mediaNode);

            attributesToSave.Add("AcquisitionTime"); // is Read-Only
            attributesToSave.Add("AlbumID"); // is Read-Only
            attributesToSave.Add("AlbumIDAlbumArtist"); // is Read-Only
            attributesToSave.Add("Author"); // is Read/Write
            attributesToSave.Add("AverageLevel"); // is Read/Write
            attributesToSave.Add("Bitrate"); // is Read-Only
            attributesToSave.Add("BuyNow"); // is Read-Only
            attributesToSave.Add("BuyTickets"); // is Read-Only
            attributesToSave.Add("Copyright"); // is Read-Only
            attributesToSave.Add("Duration"); // is Read-Only
            attributesToSave.Add("FileSize"); // is Read-Only
            attributesToSave.Add("FileType"); // is Read-Only
            attributesToSave.Add("Is_Protected"); // is Read-Only
            attributesToSave.Add("MediaType"); // is Read-Only
            attributesToSave.Add("MoreInfo"); // is Read-Only
            attributesToSave.Add("PeakValue"); // is Read/Write
            attributesToSave.Add("ProviderLogoURL"); // is Read-Only
            attributesToSave.Add("ProviderURL"); // is Read-Only
            attributesToSave.Add("RecordingTime"); // is Read-Only
            attributesToSave.Add("ReleaseDate"); // is Read/Write
            attributesToSave.Add("RequestState"); // is Read/Write
            attributesToSave.Add("SourceURL"); // is Read-Only
            attributesToSave.Add("SyncState"); // is Read-Only
            attributesToSave.Add("Title"); // is Read/Write
            attributesToSave.Add("TrackingID"); // is Read-Only
            attributesToSave.Add("UserCustom1"); // is Read/Write
            attributesToSave.Add("UserCustom2"); // is Read/Write
            attributesToSave.Add("UserEffectiveRating"); // is Read-Only
            attributesToSave.Add("UserLastPlayedTime"); // is Read-Only
            attributesToSave.Add("UserPlayCount"); // is Read/Write
            attributesToSave.Add("UserPlaycountAfternoon"); // is Read/Write
            attributesToSave.Add("UserPlaycountEvening"); // is Read/Write
            attributesToSave.Add("UserPlaycountMorning"); // is Read/Write
            attributesToSave.Add("UserPlaycountNight"); // is Read/Write
            attributesToSave.Add("UserPlaycountWeekday"); // is Read/Write
            attributesToSave.Add("UserPlaycountWeekend"); // is Read/Write
            attributesToSave.Add("UserRating"); // is Read/Write
            attributesToSave.Add("UserServiceRating"); // is Read/Write
            attributesToSave.Add("WM/AlbumArtist"); // is Read/Write
            attributesToSave.Add("WM/AlbumTitle"); // is Read/Write
            attributesToSave.Add("WM/Category"); // is Read/Write
            attributesToSave.Add("WM/Composer"); // is Read/Write
            attributesToSave.Add("WM/Conductor"); // is Read/Write
            attributesToSave.Add("WM/ContentDistributor"); // is Read/Write
            attributesToSave.Add("WM/ContentGroupDescription"); // is Read/Write
            attributesToSave.Add("WM/EncodingTime"); // is Read-Only
            attributesToSave.Add("WM/Genre"); // is Read/Write
            attributesToSave.Add("WM/InitialKey"); // is Read/Write
            attributesToSave.Add("WM/Language"); // is Read/Write
            attributesToSave.Add("WM/Lyrics"); // is Read/Write
            attributesToSave.Add("WM/MCDI"); // is Read-Only
            attributesToSave.Add("WM/MediaClassPrimaryID"); // is Read/Write
            attributesToSave.Add("WM/MediaClassSecondaryID"); // is Read/Write
            attributesToSave.Add("WM/Mood"); // is Read/Write
            attributesToSave.Add("WM/ParentalRating"); // is Read/Write
            attributesToSave.Add("WM/Period"); // is Read/Write
            attributesToSave.Add("WM/ProtectionType"); // is Read/Write
            attributesToSave.Add("WM/Provider"); // is Read-Only
            attributesToSave.Add("WM/ProviderRating"); // is Read-Only
            attributesToSave.Add("WM/ProviderStyle"); // is Read-Only
            attributesToSave.Add("WM/Publisher"); // is Read/Write
            attributesToSave.Add("WM/SubscriptionContentID"); // is Read/Write
            attributesToSave.Add("WM/SubTitle"); // is Read/Write
            attributesToSave.Add("WM/TrackNumber"); // is Read/Write
            attributesToSave.Add("WM/UniqueFileIdentifier"); // is Read/Write
            attributesToSave.Add("WM/WMCollectionGroupID"); // is Read-Only
            attributesToSave.Add("WM/WMCollectionID"); // is Read-Only
            attributesToSave.Add("WM/WMContentID"); // is Read-Only
            attributesToSave.Add("WM/Writer"); // is Read/Write


            for (int count = 0; count < attributesToSave.Count; count++)
            {
                XmlNode element = xDoc.CreateNode(XmlNodeType.Element, attributesToSave[count].Replace("WM/","WM_"), ""); // "Field", "");
                element.InnerText = media.getItemInfo(attributesToSave[count]);

                //XmlNode fieldName = xDoc.CreateNode(XmlNodeType.Attribute, "Name", "");
                //fieldName.Value = attributesToSave[count];
                //element.Attributes.SetNamedItem(fieldName);
                //XmlNode attr = xDoc.CreateNode(XmlNodeType.Attribute, "FieldValue", ""); //ns);
                //attr.Value = media.getItemInfo(attributesToSave[count]);
                //element.Attributes.SetNamedItem(attr);

                mediaNode.AppendChild(element);

            }

            xDoc.ChildNodes[0].AppendChild(mediaNode);

            return;
        }
    }
}
