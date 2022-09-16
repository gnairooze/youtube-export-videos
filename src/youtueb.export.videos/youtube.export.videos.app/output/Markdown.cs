using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YoutubeExplode.Channels;
using YoutubeExplode.Playlists;

namespace youtube.export.videos.app.output
{
    internal class Markdown:IOutput
    {

        public void Write(Channel channel, IReadOnlyList<PlaylistVideo> videos)
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
