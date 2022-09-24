using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace YoutubeDownloader.Youtube.Models
{
    internal static class Helper
    {
        public static string ExtractVideoID(string url)
        {
            string listMark = "watch?v=";
            if (!url.Contains(listMark)) return "";
            int startListIndex = url.IndexOf(listMark);
            int endListIndex = url.Length;
            if (url.Contains("&"))
                endListIndex = url.IndexOf("&");
            int idLength = endListIndex - (startListIndex + listMark.Length);
            string id = url.Substring(startListIndex + listMark.Length, idLength);
            return id;
        }

        public static string ExtractPlaylistID(string url)
        {
            string listMark = "list=";
            if (!url.Contains(listMark)) return "";

            int startListIndex = url.IndexOf(listMark);
            int endListIndex = url.LastIndexOf("&") == -1 ? url.Length : url.LastIndexOf("&");
            int idLength = endListIndex - (startListIndex + listMark.Length);
            return url.Substring(startListIndex + listMark.Length, idLength);
        }

        public static bool IsYoutubeURL(string url)
        {
            return Regex.IsMatch(url, @"^((?:https?:)?\/\/)?((?:www|m)\.)?((?:youtube(-nocookie)?\.com|youtu.be))(\/(?:[\w\-]+\?v=|embed\/|v\/)?)([\w\-]+)(\S+)?$");
        }
    }
}
