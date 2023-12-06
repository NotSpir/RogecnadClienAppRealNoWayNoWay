using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogecnadClienAppRealNoWayNoWay.Models.DatabaseModels
{
    internal class Album
    {
        public int Id { get; set; }
        public int CreatorId { get; set; }
        public string AlbumName { get; set; }
        public string AlbumCoverName { get; set; }
    }
}
