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
        [Obsolete("Duration of the video")]
        public TimeSpan? Duration { get; }
        /// <summary>
        /// Title of the video
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Author of the video
        /// </summary>
        public string Author { get; }

        /// <summary>
        /// Video ID from Youtube
        /// </summary>
        public string ID { get; }

        /// <summary>
        /// URLS for thumbnails
        /// </summary>
        public Thumbnails Thumbnails { get; }

        /// <summary>
        /// The date when the video was published
        /// </summary>
        public DateTime? PusblishedAt { get; }

        public YoutubeVideoInfo(string title, string author, string id, Thumbnails thumbnails, DateTime? publishedAt, TimeSpan? duration)
        {
            Title = title;
            PusblishedAt = publishedAt;
            Author = author;
            ID = id;
            Thumbnails = thumbnails;
            Duration = duration;
        }
    }

    /// <summary>
    /// Represents thumbnails of a video
    /// </summary>
    public class Thumbnails
    {
        public Thumbnail? Default { get; set; }
        public Thumbnail? Medium { get; set; }
        public Thumbnail? High { get; set; }
        public Thumbnail? Standard { get; set; }
        public Thumbnail? MaxRes { get; set; }
    }

    /// <summary>
    /// Represents thumbnail of a video
    /// </summary>
    public class Thumbnail
    {
        public string URL { get; private set; }
        public long? Width { get; private set; }
        public long? Height { get; private set; }

        public Thumbnail(string uRL, long? width, long? height)
        {
            URL = uRL;
            Width = width;
            Height = height;
        }
    }
}
