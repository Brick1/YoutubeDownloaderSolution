using YoutubeDownloader.Youtube.Interfaces;
using YoutubeDownloader.Youtube.Models;

namespace YoutubeDownloaderConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            YoutubeManager manager = new YoutubeManager();

            Console.WriteLine("Insert youtube url for playlist:");
            string? url = Console.ReadLine();

            if (url == null)
                return;

            Console.WriteLine("Getting playlist info");
            var playlistInfo = Task.Run(() => manager.GetPlaylistVideosUrlsAsync(Helper.ExtractPlaylistID(url))).Result;
            Console.WriteLine("Playlist info has been set");

            if (playlistInfo == null)
                return;

            int currentItemIndex = 0;
            List<IYoutubeVideoInfo> list = new List<IYoutubeVideoInfo>();
            foreach (IYoutubeVideoInfo item in playlistInfo)
            {
                IProgress<double> progress = new Progress<double>();

                Console.WriteLine($"Downloading audio: {item.Title}");
                Console.WriteLine($"{currentItemIndex + 1} of {playlistInfo.VideosInfos.Length + 1}");

                // Task.Run(() => manager.DownloadAudioFromUrlAsync(item, progress)).Wait();
                list.Add(item);
                currentItemIndex++;
                Console.WriteLine("Audio has been downloaded");                    
            }
            var ordered = list.OrderBy(x => x.PusblishedAt);
            Console.WriteLine();
            Console.WriteLine("All audios has been downloaded...");
            Console.ReadLine();

        }
    }
}