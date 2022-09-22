using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeDownloader.Youtube.Interfaces
{
    public interface IYoutubePlaylistInfo : IEnumerable
    {
        /// <summary>
        /// Title of the playlist
        /// </summary>
        string Title { get; }
        /// <summary>
        /// Description for the playlist
        /// </summary>
        string Description { get; }

        IYoutubeVideoInfo[] VideosInfos { get; }

        int Count => VideosInfos.Length;

        string ThumbnailURL { get; }

        IYoutubeVideoInfo this[int index] { get; }
    }
}
