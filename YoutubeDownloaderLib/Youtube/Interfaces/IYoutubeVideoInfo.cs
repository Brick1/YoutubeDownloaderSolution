using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeDownloader.Youtube.Interfaces
{
    public interface IYoutubeVideoInfo
    {
        /// <summary>
        /// The date when the video was published
        /// </summary>
        DateTime? PusblishedAt { get; }

        /// <summary>
        /// Title of the video
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Author of the video
        /// </summary>
        string Author { get; }

        /// <summary>
        /// Video ID
        /// </summary>
        string ID { get; }

        /// <summary>
        /// The duration of the video
        /// </summary>
        TimeSpan? Duration { get; }

        /// <summary>
        /// Collection of the thumbnails for the video
        /// </summary>
        Thumbnails Thumbnails { get; }

    }
}
