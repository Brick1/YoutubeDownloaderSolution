using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFmpeg.NET;

namespace YoutubeDownloader
{
    public class MediaConverter
    {
        /// <summary>
        /// Converts media file based on input path and output path
        /// </summary>
        /// <param name="inputPath">Full path of the input file</param>
        /// <param name="outputPath">Full path of the output file</param>
        public static async Task ConvertAsync(string inputPath, string outputPath, CancellationToken token)
        {
            var input = new InputFile(inputPath);
            var output = new OutputFile(outputPath);
            var ffmpegPath = Path.Combine(Environment.CurrentDirectory, "ffmpeg.exe");
            var ffmpeg = new Engine(ffmpegPath);
            await ffmpeg.ConvertAsync(input, output, token);
        }
    }
}
