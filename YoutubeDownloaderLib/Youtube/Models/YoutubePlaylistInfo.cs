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
        public YoutubePlaylistInfo(string title, string description, YoutubeVideoInfo[] youtubeVideoInfos, string thumbnailURL)
        {
            Title = title;
            Description = description;
            VideosInfos = youtubeVideoInfos;
            ThumbnailURL = thumbnailURL;
        }


        public IYoutubeVideoInfo this[int index] => VideosInfos[index];

        /// <summary>
        /// Title of the playlist
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Description of the playlist
        /// </summary>
        public string Description { get; }

        public YoutubeVideoInfo[] VideosInfos { get; }

        public string ThumbnailURL { get; }

        IYoutubeVideoInfo[] IYoutubePlaylistInfo.VideosInfos => VideosInfos;

        public IEnumerator GetEnumerator() => VideosInfos.GetEnumerator();

    }
}
