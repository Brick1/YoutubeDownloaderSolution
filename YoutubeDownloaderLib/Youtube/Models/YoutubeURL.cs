using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace YoutubeDownloader.Youtube.Models
{
    public class YoutubeURL
    {
        Regex youtubeReg = new Regex(@"^(https?\:\/\/)?(www\.youtube\.com|youtu\.be)\/.+$");
        Regex youtubePlaylistReg = new Regex("");
        Regex youtubeVideoReg = new Regex("");

        int httpsGroupNumber;
        public YoutubeURL(string url)
        {
            if (!youtubeReg.IsMatch(url)) return;

            youtubeUrl = url;
            httpsGroupNumber = youtubeReg.GroupNumberFromName("https://");
            isHttps = httpsGroupNumber > 0;

            playlistID = Helper.ExtractPlaylistID(url);
            hasPlaylistID = playlistID.Length > 0;

            videoID = Helper.ExtractVideoID(url);
            hasVideoID = videoID.Length > 0;            
        }

        private bool isHttps;
        public bool IsHttps => isHttps;


        private string youtubeUrl;
        public string YoutubeUrl => youtubeUrl;


        private bool hasVideoID;
        public bool HasVideoID => hasVideoID;


        private bool hasPlaylistID;
        public bool HasPlaylistID => hasPlaylistID;


        private string videoID;
        public string VideoId
        {
            get
            {
                if (hasVideoID)
                    return videoID;
                else return "";
            }
        }


        private string playlistID;
        public string PlaylistID
        {
            get
            {
                if (hasPlaylistID)
                    return playlistID;
                else return "";
            }
        }
    }
}
