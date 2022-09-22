using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeDownloader.Youtube.Interfaces
{
    public interface IPlaylistVideoItem
    {
        string[] Url { get; }
        string Title { get; }
        string Description { get; }
        string[] ThumbnailsUrls { get; }

    }
}
