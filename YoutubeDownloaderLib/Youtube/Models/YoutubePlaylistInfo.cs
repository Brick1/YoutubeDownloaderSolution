using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeDownloader.Youtube.Interfaces;

namespace YoutubeDownloader.Youtube.Models
{
    public class YoutubePlaylistInfo : IYoutubePlaylistInfo
    {
        public YoutubePlaylistInfo(string title, string description, YoutubeVideoInfo[] videosUrls, string thumbnailURL)
        {
            Title = title;
            Description = description;
            VideosInfos = videosUrls;
            ThumbnailURL = thumbnailURL;
        }

        public IYoutubeVideoInfo this[int index] => VideosInfos[index];

        public string Title { get; }

        public string Description { get; }

        public YoutubeVideoInfo[] VideosInfos { get; }

        public string ThumbnailURL { get; }

        IYoutubeVideoInfo[] IYoutubePlaylistInfo.VideosInfos => VideosInfos;

        public IEnumerator GetEnumerator()
        {
            return VideosInfos.GetEnumerator();
        }
    }
}
