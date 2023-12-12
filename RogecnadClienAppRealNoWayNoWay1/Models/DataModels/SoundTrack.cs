using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace RogecnadClienAppRealNoWayNoWay.Models.DatabaseModels
{
    internal class SoundTrack
    {
        public string Id { get; set; }
        public string UploaderId { get; set; }
        public string TrackName { get; set; }
        public string GenreId { get; set; }
        public string TrackCoverBytes { get; set; }

        public string Duration { get; set; }

        public BitmapImage GetCoverImage
        {
            get {
                byte[] bytes = Convert.FromBase64String(TrackCoverBytes);
                using (var ms = new System.IO.MemoryStream(bytes))
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = ms;
                    image.EndInit();
                    return image;
                }
            }
        }
    }
}
