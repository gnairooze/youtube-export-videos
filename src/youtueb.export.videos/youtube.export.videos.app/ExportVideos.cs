using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Channels;
using YoutubeExplode.Common;
using YoutubeExplode.Playlists;
using Channel = YoutubeExplode.Channels.Channel;

namespace youtube.export.videos.app
{
    internal class ExportVideos
    {
        private static YoutubeClient _Youtube = new YoutubeClient();

        public static async Task ExportChannel()
        {
            try
            {
                #region Step 1
                Console.Write("Enter YouTube Channel URL: ");
                var url = Console.ReadLine() ?? "";

                var channel = await _Youtube.Channels.GetAsync(url);
                #endregion

                await Step2(channel);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            
        }

        private static async Task Step2(Channel channel)
        {
            var videos = await _Youtube.Channels.GetUploadsAsync(channel.Url);

            WriteMD(channel, videos);

            Console.WriteLine("Done");
        }

        public static async Task ExportUser()
        {
            try
            {
                #region Step 1
                Console.Write("Enter YouTube User URL: ");
                var url = Console.ReadLine() ?? "";

                var channel = await _Youtube.Channels.GetByUserAsync(url);
                #endregion

                await Step2(channel);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            
        }

        public static async Task ExportSlug()
        {
            try
            {
                #region Step 1
                Console.Write("Enter YouTube Slug URL: ");
                var url = Console.ReadLine() ?? "";

                var channel = await _Youtube.Channels.GetBySlugAsync(url);
                #endregion

                await Step2(channel);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }

        private static void WriteMD(Channel channel, IReadOnlyList<PlaylistVideo> videos)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            var title = rgx.Replace(channel.Title, "");
            var createdate = new DateTime(DateTime.Now.Ticks);
            var createtext = createdate.ToString("yyyy-MM-dd-HH-mm-fff");

            var filename = $"{title}-{createtext}.md";

            System.IO.File.AppendAllText(filename, $"# [{channel.Title}]({channel.Url}){Environment.NewLine}");

            System.IO.File.AppendAllText(filename, $"{videos.Count} videos exported on {createdate.ToString("yyyy-MM-dd HH:mm")}{Environment.NewLine}{Environment.NewLine}");

            int counter = 1;

            var counterPattern = $"D{Math.Floor(Math.Log10(videos.Count)) + 1}";

            foreach (var video in videos)
            {
                System.IO.File.AppendAllText(filename, $"{counter.ToString(counterPattern)}. [{video.Title}]({video.Url}){Environment.NewLine}![](https://i.ytimg.com/vi/{video.Id}/mqdefault.jpg){Environment.NewLine}{video.Duration} {Environment.NewLine}{Environment.NewLine}");

                counter++;
            }
        }
    }
}
