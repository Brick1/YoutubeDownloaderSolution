using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeDownloader.Youtube.Interfaces;
using YoutubeDownloader.Youtube.Models;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace YoutubeDownloader.Youtube
{
    internal class Downloader
    {
        /// <summary>
        /// Client used for downloading videos from YoutubeExplode
        /// </summary>
        YoutubeClient client;

        public Downloader()
        {
            client = new YoutubeClient();
        }

        /// <summary>
        /// Gets the audio manifest for the given video ID
        /// </summary>
        /// <param name="videoID">Id from Url of video</param>
        /// <returns><see cref="IStreamInfo"/></returns>
        private async Task<IStreamInfo> GetAudioManifest(string videoID)
        {
            var manifest = await client.Videos.Streams.GetManifestAsync(videoID);
            return manifest.GetAudioOnlyStreams().GetWithHighestBitrate();
        }

        /// <summary>
        /// Gets the video manifest for the given video ID
        /// </summary>
        /// <param name="videoID">Id from Url of video</param>
        /// <returns><see cref="IStreamInfo"/></returns>
        private async Task<IStreamInfo> GetVideoManifest(string videoID)
        {
            var manifest = await client.Videos.Streams.GetManifestAsync(videoID);
            return manifest.GetMuxedStreams().GetWithHighestBitrate();
        }

        /// <summary>
        /// Gets video info
        /// </summary>
        /// <param name="videoID">Id from Url of video</param>
        /// <returns><see cref="YoutubeVideoInfo"/></returns>
        public async Task<YoutubeVideoInfo> GetVideoInfo(string videoID)
        {
            var videoInfo = await client.Videos.GetAsync(videoID).ConfigureAwait(false);
            return new YoutubeVideoInfo(videoInfo.Title, videoInfo.Author.Title, videoInfo.Id, videoInfo.Thumbnails.Select(q => q.Url).ToArray(), null, videoInfo.Duration);
        }

        /// <summary>
        /// Downloads audio async to a local drive
        /// </summary>
        /// <param name="videoInfo"><see cref="IYoutubeVideoInfo"/></param>
        /// <param name="progress"><see cref="IProgress{T}>"/></param>
        /// <returns>Tuple<string, string> First one is complete path to the file, second is name of the locally stored file</returns>
        public async Task<Tuple<string, string>> DownloadAudioAsync(IYoutubeVideoInfo videoInfo, IProgress<double> progress)
        {
            return await DownloadMedia(videoInfo, progress, GetAudioManifest(videoInfo.ID));
        }


        /// <summary>
        /// Downloads audio async to a local drive
        /// </summary>
        /// <param name="videoInfo"><see cref="IYoutubeVideoInfo"/></param>
        /// <param name="progress"><see cref="IProgress{T}>"/></param>
        /// <returns>Tuple<string, string> First one is complete path to the file, second is name of the locally stored file</returns>
        public async Task<Tuple<string, string>> DownloadVideoAsync(IYoutubeVideoInfo videoInfo, IProgress<double> progress)
        {
            return await DownloadMedia(videoInfo, progress, GetVideoManifest(videoInfo.ID));
        }

        /// <summary>
        /// Downloads media to a local file with given delegate
        /// </summary>
        /// <param name="videoInfo"><see cref="IYoutubeVideoInfo"/></param>
        /// <param name="progress"><see cref="IProgress{T}"></see>/></param>
        /// <param name="downloadMethod">Func with <see cref="IStreamInfo"></see> return type/></param>
        /// <returns></returns>
        private async Task<Tuple<string, string>> DownloadMedia(IYoutubeVideoInfo videoInfo, IProgress<double> progress, Task<IStreamInfo> downloadMethod)
        {
            var stream = await downloadMethod;
            var path = Path.Combine(Path.GetTempPath(), videoInfo.Title + "." + stream.Container.Name);
            await client.Videos.Streams.DownloadAsync(stream, path, progress);
            return new Tuple<string, string>(path, videoInfo.Title);
        }

        /// THIS WILL BE REMOVED IN THE FUTURE
        ///// <summary>
        ///// Gets the playlist info by given paylist ID
        ///// </summary>
        ///// <param name="playlistID">Playlist ID</param>
        ///// <returns><see cref="YoutubePlaylistInfo"></see>/></returns>
        //public async ValueTask<YoutubePlaylistInfo> GetPlaylistInfno(string playlistID, string[] videosURLs)
        //{
        //    var playlistInfo = await client.Playlists.GetAsync(playlistID);
        //    var playlistTitle = playlistInfo.Title;
        //    return new YoutubePlaylistInfo(playlistTitle, playlistInfo.Description, videosURLs);
        //}

        /// <summary>
        /// Gets the playlist by given playlist ID
        /// In a large playlist this method can take a few minutes, use foreach and GetVideoInfo instead
        /// </summary>
        /// <param name="playlistID">The playlist ID</param>
        /// <returns><see cref="YoutubeVideoInfo"></see>/></returns>
        [Obsolete("In a large playlist this method can take a few minutes, use foreach and GetVideoInfo instead")]
        public async Task<YoutubeVideoInfo[]> GetPlaylistVideosAsync(string[] playlistVideosUrls) // this seems to be critical, DO NOT USE interface as return type, idk why tho got to look into this
        {
            //var playlistInfo = await client.Playlists.GetAsync(playlistID);
            return await GetVideos(playlistVideosUrls).ConfigureAwait(false);
        }


        /// <summary>
        /// Gets videos infos as array od <see cref="IYoutubeVideoInfo"></see>/>
        /// </summary>
        /// <param name="playlistVideos">An array with playlist videos IDs</param>
        /// <returns><see cref="IYoutubeVideoInfo"></see> array/></returns>
        private async Task<YoutubeVideoInfo[]> GetVideos(string[] playlistVideos)
        {
            List<YoutubeVideoInfo> videos = new List<YoutubeVideoInfo>();
            foreach (var video in playlistVideos)
            {
                try
                {
                    var videoInfo = await GetVideoInfo(video).ConfigureAwait(false);
                    videos.Add(videoInfo);
                }
                catch (Exception ex)
                {
                    // TODO: Some kind of log system
                }
            }
            return videos.ToArray();
        }


    }
}
