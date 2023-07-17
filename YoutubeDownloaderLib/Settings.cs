using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using YoutubeExplode.Videos.Streams;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace YoutubeDownloader
{
    internal class Settings
    {

        private static Settings instance;

        public const string MAIN_FOLDER_NAME = "BrickSoft";

        public string  ProgramFolderName { get; set; }
        public string VideoFolderPath { get; set; }
        public string AudioFolderPath { get; set; }
        public string AppFolder { get; set; }
        public string WindowTitle { get; set; }
        public string LastUrl { get; set; }
        public string ApiKey { get; private set; }
        private Settings()
        {
            ProgramFolderName = "";
            VideoFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
            AudioFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            AppFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            WindowTitle = "Youtube Video Downloader";
            LastUrl=string.Empty;
        }

        public static Settings Instance { get => instance ?? (instance = new Settings()); }


        public void LoadSettings()
        {
            if (!File.Exists(Path.Combine(AppFolder, "sett.bin")))
                return;
            Settings set = DeserializableFromFile<Settings>("sett.bin", AppFolder);
            this.AudioFolderPath = set.AudioFolderPath;
            this.VideoFolderPath = set.VideoFolderPath;
            this.LastUrl =set.LastUrl;
        }

        public void SaveSettings()
        {
            SerializableToFile("sett.bin", AppFolder, this);
        }

        private void SerializableToFile(string fileName, string dirPath, object obj)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream file = File.Create(Path.Combine(dirPath, fileName)))
            {
                formatter.Serialize(file, obj);
            }
        }

        private  T DeserializableFromFile<T>(string filename, string dirPath)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream file = File.OpenRead(Path.Combine(dirPath, filename)))
            {
                return (T)formatter.Deserialize(file);
            }
        }


    }
}
