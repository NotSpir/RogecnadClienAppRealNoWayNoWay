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
        public List<string> Tracks { get
            {
                List<string> tracksInPlaylists = new List<string>();
                var result = FirebaseClientModel.client.Get("TracksPlaylist/" + playlist.Id);
                Dictionary<string, string> getTracks = result.ResultAs<Dictionary<string, string>>();
                if (getTracks != null)
                foreach (var track in getTracks)
                {
                    tracksInPlaylists.Add(track.Key);
                }

                if (tracksInPlaylists.Count() > 0)
                    return tracksInPlaylists;
                else return new List<string>();
            } 
        }

        public BitmapImage GetCoverImage
        {
            get
            {
                if (Tracks.Count > 0)
                {
                    BitmapImage cover = new BitmapImage();
                    string trackID = Tracks.LastOrDefault();
                    var result = FirebaseClientModel.client.Get("Soundtracks/" + trackID).ResultAs<SoundTrack>();
                    cover = result.GetCoverImage;
                    return cover;
                }
                return null;

            }
        }

        public int TrackCount
        {
            get
            {
                    return Tracks.Count();
            }
        }
        public string PlaylistName { get { return playlist.PlaylistName; } }

    }
}

