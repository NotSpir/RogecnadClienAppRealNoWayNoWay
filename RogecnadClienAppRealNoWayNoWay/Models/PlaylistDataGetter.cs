using RogecnadClienAppRealNoWayNoWay.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace RogecnadClienAppRealNoWayNoWay.Models
{
    internal class PlaylistDataGetter
    {
        public Playlist playlist { get; set; }
        public List<TracksPlaylist> Tracks { get
            {
                List<TracksPlaylist> tracksInPlaylists = new List<TracksPlaylist>();
                var result = FirebaseClientModel.client.Get("PlaylistsTracks");
                Dictionary<string, TracksPlaylist> getTracks = result.ResultAs<Dictionary<string, TracksPlaylist>>();
                if (getTracks != null)
                foreach (var item in getTracks)
                {
                    if (item.Value.PlaylistId == playlist.Id)
                    {
                        var val = new TracksPlaylist() { PlaylistId = item.Value.PlaylistId, TrackId = item.Value.TrackId };
                        tracksInPlaylists.Add(val);
                    }
                    
                }
                return tracksInPlaylists;
            } 
        }

        public BitmapImage GetCoverImage
        {
            get
            {
                if (Tracks.Count > 0)
                {
                    BitmapImage cover = new BitmapImage();
                    var result = FirebaseClientModel.client.Get("Soundtracks");
                    Dictionary<string, SoundTrack> getTracks = result.ResultAs<Dictionary<string, SoundTrack>>();
                    foreach (var item in getTracks)
                    {
                        if (item.Value.Id == Tracks.FirstOrDefault().TrackId)
                        {
                            cover = item.Value.GetCoverImage;
                            break;
                        }

                    }
                    return cover;
                }
                return null;
                
            }
        }

        public int TrackCount { get { return Tracks.Count(); } }

        public string PlaylistName { get { return playlist.PlaylistName; } }

    }
}

