using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeDownloader
{
    [Serializable]
    internal class Settings
    {
        public const string MAIN_FOLDER_NAME = "BrickSoft";
        public static string ProgramFolderName = "";
        public static string VideoFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
        public static string AudioFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
        public static string AppFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string WindowTitle { get; set; } = "Youtube Video Downloader";
        public static string LastUrl { get; set; } = "";



    }
}
