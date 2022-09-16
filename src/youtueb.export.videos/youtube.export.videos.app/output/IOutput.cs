using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode.Channels;
using YoutubeExplode.Playlists;

namespace youtube.export.videos.app.output
{
    internal interface IOutput
    {
        void Write(Channel channel, IReadOnlyList<PlaylistVideo> videos);
    }
}
