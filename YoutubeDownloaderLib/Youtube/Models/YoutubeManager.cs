using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YoutubeDownloader.Youtube.Interfaces;

namespace YoutubeDownloader.Youtube.Models
{

    /// <summary>
    /// Public class for downloading from YT
    /// </summary>
    public class YoutubeManager
    {
        const string YOUTUBE_URL = "https://www.youtube.com/";
        const string YOUTUBE_VID_TOKEN = "watch?v=";
        Downloader downloader;

        /// <summary>
        /// Youtube service from Youtube API
        /// </summary>
        YouTubeService ytService;

        public YoutubeManager()
        {
            //TODO: GET API KEY LOGIC HERE
            downloader = new Downloader();
            ytService = CreateYoutubeService("AIzaSyABcRj1TumeuD-LKu8NvsQQJmoCOljRbWA");

        }

        /// <summary>
        /// Creates youtube service from Google API
        /// </summary>
        /// <param name="apikey">API key for Youtube API</param>
        /// <returns><see cref="YouTubeService"/></returns>
        private YouTubeService CreateYoutubeService(string apikey)
        {
            return new YouTubeService(new Google.Apis.Services.BaseClientService.Initializer
            {
                ApiKey = apikey,
                ApplicationName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name
            });
        }

        public async Task DownloadAudioFromUrlAsync(IYoutubeVideoInfo video, IProgress<double> progress)
        {
            CancellationToken cancellationToken = new CancellationToken();
            var extension = ".mp3";
            var downloadInfo = await downloader.DownloadAudioAsync(video, progress);
            if(downloadInfo == null) return;
            var output = Path.Combine(Settings.AudioFolderPath, downloadInfo.Item2 + extension);
            MediaConverter.ConvertAsync(downloadInfo.Item1, output, cancellationToken).Wait();
            File.Delete(downloadInfo.Item1);
        }

        public async Task DownloadVideoFromUrlAsync(IYoutubeVideoInfo video, IProgress<double> progress)
        {
            CancellationToken cancellationToken = new CancellationToken();
            var extension = ".mp4";
            var downloadInfo = await downloader.DownloadVideoAsync(video, progress);
            var output = Path.Combine(Settings.VideoFolderPath, downloadInfo.Item2 + extension);
            MediaConverter.ConvertAsync(downloadInfo.Item1, output, cancellationToken).Wait();
            File.Delete(downloadInfo.Item1);
        }

        /// <summary>
        /// Gets an array with videos urls
        /// </summary>
        /// <param name="playlistID">Playlist ID</param>
        /// <returns>An array of urls as string</returns>
        public async Task<YoutubePlaylistInfo?> GetPlaylistVideosUrlsAsync(string playlistID)
        {
            List<YoutubeVideoInfo> youtubeVideos = new List<YoutubeVideoInfo>();

            var playlistListRequest = ytService.Playlists.List("snippet");
            playlistListRequest.Id = playlistID;
            var playlistListResponse = await playlistListRequest.ExecuteAsync(); // getting the playlist
            var result = playlistListResponse.Items.FirstOrDefault(); // picking the first playlist, it's usually just one in the array
            if (result == null)
                return null;

            // cycling through the pages with videos until the token is null
            string nextPageToken = "";
            while (nextPageToken != null)
            {
                var playlistItemsListRequest = ytService.PlaylistItems.List("snippet");
                playlistItemsListRequest.PlaylistId = playlistID;
                playlistItemsListRequest.MaxResults = 50;
                playlistItemsListRequest.PageToken = nextPageToken;
                try
                {
                    var playlistItemsListResponse = await playlistItemsListRequest.ExecuteAsync().ConfigureAwait(false);

                    foreach (var item in playlistItemsListResponse.Items)
                    {
                        // if the video is private there's no info about it, nor the URL, so let's just skip it
                        if (item.Snippet?.Title.ToLower() == "private video") continue;

                        // creating the videoInfo and adding it to the list
                        var youtubevideoInfo = CreateVideoInfo(item);
                        youtubeVideos.Add(youtubevideoInfo);
                    }
                    nextPageToken = playlistItemsListResponse.NextPageToken;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            YoutubePlaylistInfo info = new YoutubePlaylistInfo(result.Snippet.Title, result.Snippet.Description, youtubeVideos.ToArray(), GetThumbnailURL(result.Snippet.Thumbnails)[0]);
            // TODO: make a simplier way to get the thumbnail for the playlist
            return info;
        }

        /// <summary>
        /// Creates video info from <see cref="PlaylistItem"/>
        /// </summary>
        /// <returns>Newly created <see cref="YoutubeVideoInfo"/></returns>
        private YoutubeVideoInfo CreateVideoInfo(PlaylistItem item)
        {
            var id = item.Snippet?.ResourceId.Kind == "youtube#video" ? item.Snippet.ResourceId.VideoId : "";
            var title = item.Snippet?.Title;
            var author = item.Snippet?.VideoOwnerChannelTitle;
            var thumbnails = GetThumnbails(item.Snippet?.Thumbnails);
            var addedToPlaylist = item.Snippet?.PublishedAt;
            var url = item.Snippet;
            return new YoutubeVideoInfo(title, author, id, thumbnails, addedToPlaylist, null);
        }

        private Thumbnails GetThumnbails(ThumbnailDetails? thumbnails)
        {
            if (thumbnails != null) return new Thumbnails();

            Thumbnails thumbs = new Thumbnails();
            thumbs.Default = new Thumbnail(thumbnails.Default__.Url, thumbnails?.Default__?.Width, thumbnails?.Default__?.Height);
            thumbs.Standard = new Thumbnail(thumbnails.Standard.Url, thumbnails.Standard.Width, thumbnails.Standard.Height);
            thumbs.MaxRes = new Thumbnail(thumbnails.Maxres.Url, thumbnails.Maxres.Width, thumbnails.Maxres.Height);
            thumbs.Medium = new Thumbnail(thumbnails.Medium.Url, thumbnails.Medium.Width, thumbnails.Medium.Height);
            thumbs.High = new Thumbnail(thumbnails.High.Url, thumbnails.High.Width, thumbnails.High.Height);
            return thumbs;
        }

        private string[] GetThumbnailURL(ThumbnailDetails thumbnailDetails)
        {
            List<string> urls = new List<string>();
            urls.Add(thumbnailDetails?.Maxres?.Url);
            urls.Add(thumbnailDetails?.High?.Url);
            urls.Add(thumbnailDetails?.Standard?.Url);
            urls.Add(thumbnailDetails?.Default__?.Url);
            return urls.ToArray();
        }
    }
}
