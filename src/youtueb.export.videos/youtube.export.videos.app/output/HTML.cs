using Mustache;
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
    internal class HTML:IOutput
    {

        public void Write(Channel channel, IReadOnlyList<PlaylistVideo> videos)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            var title = rgx.Replace(channel.Title, "");
            var createdate = new DateTime(DateTime.Now.Ticks);
            var createtext = createdate.ToString("yyyy-MM-dd-HH-mm-fff");

            var filename = $"{title}-{createtext}.html";

            HtmlFormatCompiler compiler = new HtmlFormatCompiler();
            var template = File.ReadAllText(@"output\templates\cards.html");

            var generator = compiler.Compile(template);
            var html = generator.Render(new
            {
                ChannelName = channel.Title,
                ChannelUrl = channel.Url,
                videosCount = videos.Count,
                createDate = createdate.ToString("yyyy-MM-dd HH:mm"),
                videos = videos
            });

            File.WriteAllText(filename, html);
        }

    }
}
