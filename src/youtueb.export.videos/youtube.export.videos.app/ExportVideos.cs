using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using System.Threading.Tasks;
using youtube.export.videos.app.output;
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

        public static async Task ExportChannel(IOutput output)
        {
            try
            {
                #region Step 1
                Console.Write("Enter YouTube Channel URL: ");
                var url = Console.ReadLine() ?? "";

                var channel = await _Youtube.Channels.GetAsync(url);
                #endregion

                await Step2(channel, output);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            
        }

        private static async Task Step2(Channel channel, IOutput output)
        {
            var videos = await _Youtube.Channels.GetUploadsAsync(channel.Url);

            output.Write(channel, videos);

            Console.WriteLine("Done");
        }

        public static async Task ExportUser(IOutput output)
        {
            try
            {
                #region Step 1
                Console.Write("Enter YouTube User URL: ");
                var url = Console.ReadLine() ?? "";

                var channel = await _Youtube.Channels.GetByUserAsync(url);
                #endregion

                await Step2(channel, output);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            
        }

        public static async Task ExportSlug(IOutput output)
        {
            try
            {
                #region Step 1
                Console.Write("Enter YouTube Slug URL: ");
                var url = Console.ReadLine() ?? "";

                var channel = await _Youtube.Channels.GetBySlugAsync(url);
                #endregion

                await Step2(channel, output);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }
    }
}
