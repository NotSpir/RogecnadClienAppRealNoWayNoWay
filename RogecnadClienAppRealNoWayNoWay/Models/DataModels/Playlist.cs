using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogecnadClienAppRealNoWayNoWay.Models.DatabaseModels
{
    internal class Playlist
    {
        public int Id { get; set; }
        public string PlaylistName { get; set; }
        public int CreatorId { get; set; }
    }
}
