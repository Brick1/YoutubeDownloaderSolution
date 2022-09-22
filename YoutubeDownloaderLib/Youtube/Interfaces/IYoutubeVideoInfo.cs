using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeDownloader.Youtube.Interfaces
{
    public interface IYoutubeVideoInfo
    {
        DateTime? PusblishedAt { get; }
        string Title { get; }
        string Author { get; }
        //string URL { get; }
        string ID { get; }
        TimeSpan? Duration { get; }

        string[] ThumbnailsUrls { get; }
    }
}
