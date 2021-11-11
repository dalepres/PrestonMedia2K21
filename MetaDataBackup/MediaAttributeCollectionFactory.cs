using System;
using System.Collections.Generic;
using System.Text;

namespace Preston.Media
{
    class MediaAttributeCollectionFactory
    {

        public static MediaAttributeCollection CreateDefaultCollection()
        {
            MediaAttributeCollection mac = new MediaAttributeCollection();
            mac.Add(new MediaAttribute("AcquisitionTime", true));
            mac.Add(new MediaAttribute("AlbumID", true));
            mac.Add(new MediaAttribute("AlbumIDAlbumArtist", true));
            mac.Add(new MediaAttribute("Author", false));
            mac.Add(new MediaAttribute("AverageLevel", false));
            mac.Add(new MediaAttribute("Bitrate", true));
            mac.Add(new MediaAttribute("BuyNow", true));
            mac.Add(new MediaAttribute("BuyTickets", true));
            mac.Add(new MediaAttribute("Copyright", true));
            mac.Add(new MediaAttribute("Duration", true));
            mac.Add(new MediaAttribute("FileSize", true));
            mac.Add(new MediaAttribute("FileType", true));
            mac.Add(new MediaAttribute("Is_Protected", true));
            mac.Add(new MediaAttribute("MediaType", true));
            mac.Add(new MediaAttribute("MoreInfo", true));
            mac.Add(new MediaAttribute("PeakValue", false));
            mac.Add(new MediaAttribute("ProviderLogoURL", true));
            mac.Add(new MediaAttribute("ProviderURL", true));
            mac.Add(new MediaAttribute("RecordingTime", true));
            mac.Add(new MediaAttribute("ReleaseDate", false));
            mac.Add(new MediaAttribute("RequestState", false));
            mac.Add(new MediaAttribute("SourceURL", true));
            mac.Add(new MediaAttribute("SyncState", true));
            mac.Add(new MediaAttribute("Title", false));
            mac.Add(new MediaAttribute("TrackingID", true));
            mac.Add(new MediaAttribute("UserCustom1", false));
            mac.Add(new MediaAttribute("UserCustom2", false));
            mac.Add(new MediaAttribute("UserEffectiveRating", true));
            mac.Add(new MediaAttribute("UserLastPlayedTime", true));
            mac.Add(new MediaAttribute("UserPlayCount", false));
            mac.Add(new MediaAttribute("UserPlaycountAfternoon", false));
            mac.Add(new MediaAttribute("UserPlaycountEvening", false));
            mac.Add(new MediaAttribute("UserPlaycountMorning", false));
            mac.Add(new MediaAttribute("UserPlaycountNight", false));
            mac.Add(new MediaAttribute("UserPlaycountWeekday", false));
            mac.Add(new MediaAttribute("UserPlaycountWeekend", false));
            mac.Add(new MediaAttribute("UserRating", false));
            mac.Add(new MediaAttribute("UserServiceRating", false));
            mac.Add(new MediaAttribute("WM/AlbumArtist", false));
            mac.Add(new MediaAttribute("WM/AlbumTitle", false));
            mac.Add(new MediaAttribute("WM/Category", false));
            mac.Add(new MediaAttribute("WM/Composer", false));
            mac.Add(new MediaAttribute("WM/Conductor", false));
            mac.Add(new MediaAttribute("WM/ContentDistributor", false));
            mac.Add(new MediaAttribute("WM/ContentGroupDescription", false));
            mac.Add(new MediaAttribute("WM/EncodingTime", true));
            mac.Add(new MediaAttribute("WM/Genre", false));
            mac.Add(new MediaAttribute("WM/InitialKey", false));
            mac.Add(new MediaAttribute("WM/Language", false));
            mac.Add(new MediaAttribute("WM/Lyrics", false));
            mac.Add(new MediaAttribute("WM/MCDI", true));
            mac.Add(new MediaAttribute("WM/MediaClassPrimaryID", false));
            mac.Add(new MediaAttribute("WM/MediaClassSecondaryID", false));
            mac.Add(new MediaAttribute("WM/Mood", false));
            mac.Add(new MediaAttribute("WM/ParentalRating", false));
            mac.Add(new MediaAttribute("WM/Period", false));
            mac.Add(new MediaAttribute("WM/ProtectionType", false));
            mac.Add(new MediaAttribute("WM/Provider", true));
            mac.Add(new MediaAttribute("WM/ProviderRating", true));
            mac.Add(new MediaAttribute("WM/ProviderStyle", true));
            mac.Add(new MediaAttribute("WM/Publisher", false));
            mac.Add(new MediaAttribute("WM/SubscriptionContentID", false));
            mac.Add(new MediaAttribute("WM/SubTitle", false));
            mac.Add(new MediaAttribute("WM/TrackNumber", false));
            mac.Add(new MediaAttribute("WM/UniqueFileIdentifier", false));
            mac.Add(new MediaAttribute("WM/WMCollectionGroupID", true));
            mac.Add(new MediaAttribute("WM/WMCollectionID", true));
            mac.Add(new MediaAttribute("WM/WMContentID", true));
            mac.Add(new MediaAttribute("WM/Writer", false));
            return mac;
        }

        //public static MediaAttributeCollection CreateFromStringArray(string[] array)
        //{

        //}
    }
}
