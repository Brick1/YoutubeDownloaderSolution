using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeDownloader.Youtube.Interfaces;

namespace YoutubeDownloader.Youtube
{
    public class YoutubeVideoInfo : IYoutubeVideoInfo
    {
        /// <summary>
        /// Duration of the video
        /// Will be obselete and removed
        /// </summary>
        public TimeSpan? Duration { get; }
        /// <summary>
        /// Title of the video
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Author of the video
        /// </summary>
        public string Author { get; }

        ///// <summary>
        ///// Video URL
        ///// </summary>
        //public string URL { get; }

        /// <summary>
        /// Video ID from Youtube
        /// </summary>
        public string ID { get; }

        /// <summary>
        /// URLS for thumbnails
        /// </summary>
        public string[] ThumbnailsUrls { get; }

        public DateTime? PusblishedAt { get; }

        public YoutubeVideoInfo(string title, string author, string id, string[] thumbnails, DateTime? publishedAt, TimeSpan? duration)
        {
            Title = title;
            PusblishedAt = publishedAt;
            Author = author;
            ID = id;
            ThumbnailsUrls = thumbnails;
            Duration = duration;
        }
    }
}
