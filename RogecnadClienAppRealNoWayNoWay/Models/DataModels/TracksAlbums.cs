using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogecnadClienAppRealNoWayNoWay.Models.DatabaseModels
{
    internal class TracksAlbums
    {
        public int AlbumId { get; set; }
        public List<int> TrackIds { get; set; }
    }
}
