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
    /// <summary>
    /// This is main class for downloading from YT
    /// </summary>
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
        /// Downloads audio async to a local drive
        /// </summary>
        /// <param name="videoInfo"><see cref="IYoutubeVideoInfo"/></param>
        /// <param name="progress"><see cref="IProgress{T}>"/></param>
        /// <returns>Tuple<string, string> First one is complete path to the file, second is name of the locally stored file</returns>
        public async Task<Tuple<string, string>?> DownloadAudioAsync(IYoutubeVideoInfo videoInfo, IProgress<double> progress)
        {
            var streamInfo = GetAudioManifest(videoInfo.ID);
            if (streamInfo == null)
                return null;
            return await DownloadMedia(videoInfo, progress, streamInfo);
        }

        /// <summary>
        /// Downloads whole playlist, this can take a while and I really mean it.
        /// </summary>
        /// <returns>Returns list of tuples, where first string is path to the file and second is filename</returns>
        public async Task<List<Tuple<string, string>>?> DownloadAudiosAsync(IYoutubePlaylistInfo playlistInfo, IProgress<double> progress, string saveFilePath)
        {
            List<Tuple<string, string>>? downloadedAudios = new List<Tuple<string, string>>();
            foreach (IYoutubeVideoInfo videoInfo in playlistInfo)
            {
                var itemToAdd = await DownloadAudioAsync(videoInfo, progress);
                if (itemToAdd != null)
                    downloadedAudios.Add(itemToAdd);
            }
            return downloadedAudios;
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
            var videoTitle = Helper.ValidateTitle(videoInfo.Title);
            var path = Path.Combine(Path.GetTempPath(), videoTitle + "." + stream.Container.Name);
            await client.Videos.Streams.DownloadAsync(stream, path, progress);
            return new Tuple<string, string>(path, videoTitle);
        }


    }
}
