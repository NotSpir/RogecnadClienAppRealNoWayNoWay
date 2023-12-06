using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogecnadClienAppRealNoWayNoWay.Models.DatabaseModels
{
    internal class TracksPlaylist
    {
        public int PlaylistId { get; set; }
        public List<int> TrackIds { get; set; }
    }
}
