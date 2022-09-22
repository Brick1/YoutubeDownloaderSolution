using MediaToolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace YoutubeDownloader
{
    public class MediaConverter
    {
        /// <summary>
        /// Converts media file based on input path and output path
        /// </summary>
        /// <param name="inputPath">Full path of the input file</param>
        /// <param name="outputPath">Full path of the output file</param>
        public static void Convert(string inputPath, string outputPath)
        {
            var input = new MediaToolkit.Model.MediaFile(inputPath);
            var output = new MediaToolkit.Model.MediaFile(outputPath);
            using (var engine = new Engine())
            {
                engine.Convert(input, output);
            }
        }
    }
}
