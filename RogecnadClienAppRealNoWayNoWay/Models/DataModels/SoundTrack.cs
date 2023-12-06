using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogecnadClienAppRealNoWayNoWay.Models.DatabaseModels
{
    internal class SoundTrack
    {
        public int Id { get; set; }
        public int UploaderId { get; set; }
        public int GenreId { get; set; }
        public string TrackName { get; set; }
    }
}
