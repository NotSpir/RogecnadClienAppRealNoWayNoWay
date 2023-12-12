using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace RogecnadClienAppRealNoWayNoWay.Models
{
    public class AudioPlayer
    {
        public static MediaPlayer soundPlayer = new MediaPlayer();
        public static bool IsPlaying = false;
        public static MediaPlayer InitializePlayer(string fileToPlay)
        {
            if (IsPlaying)
                AudioPlayer.Stop(AudioPlayer.soundPlayer);
            MediaPlayer player = new MediaPlayer();
            player.Open(new Uri(fileToPlay));
            return player;
        }

        public static void Play(MediaPlayer player, TimeSpan startPoint)
        {
            player.Volume = AppManager.SongVolume;
            player.Position = startPoint;
            AudioPlayer.IsPlaying = true;
            player.Play();
        }

        public static void Stop(MediaPlayer player)
        {
            player?.Stop();
            AudioPlayer.IsPlaying = false; 
        }

        public static void changeTime(MediaPlayer player, int seconds)
        {
            player.Position = new TimeSpan(seconds * 10);
        }

        public static void changeVolume(MediaPlayer player, double value)
        {
            AppManager.SongVolume = value;
            player.Volume = value;
        }

        static TimeSpan GetMediaDuration(string mediaFile)
        {
            return GetMediaDuration(mediaFile, TimeSpan.Zero);
        }

        public static TimeSpan GetMediaDuration(string mediaFile, TimeSpan maxTimeToWait)
        {
            var mediaData = new MediaData() { MediaUri = new Uri(mediaFile) };

            var thread = new Thread(GetMediaDurationThreadStart);
            DateTime deadline = DateTime.Now.Add(maxTimeToWait);
            thread.Start(mediaData);

            while (!mediaData.Done && ((TimeSpan.Zero == maxTimeToWait) || (DateTime.Now < deadline)))
                Thread.Sleep(100);

            Dispatcher.FromThread(thread).InvokeShutdown();

            if (!mediaData.Done)
                throw new Exception(string.Format("GetMediaDuration timed out after {0}", maxTimeToWait));
            if (mediaData.Failure)
                throw new Exception(string.Format("MediaFailed {0}", mediaFile));

            return mediaData.Duration;
        }

        static void GetMediaDurationThreadStart(object context)
        {
            var mediaData = (MediaData)context;
            var mediaPlayer = new MediaPlayer();

            mediaPlayer.MediaOpened +=
                delegate
                {
                    if (mediaPlayer.NaturalDuration.HasTimeSpan)
                        mediaData.Duration = mediaPlayer.NaturalDuration.TimeSpan;
                    mediaData.Success = true;
                    mediaPlayer.Close();
                };

            mediaPlayer.MediaFailed +=
                delegate
                {
                    mediaData.Failure = true;
                    mediaPlayer.Close();
                };

            mediaPlayer.Open(mediaData.MediaUri);

            Dispatcher.Run();
        }

        public static TimeSpan StripMilliseconds(TimeSpan time)
        {
            return new TimeSpan(time.Days, time.Hours, time.Minutes, time.Seconds);
        }
    }

    class MediaData
    {
        public Uri MediaUri;
        public TimeSpan Duration = TimeSpan.Zero;
        public bool Success;
        public bool Failure;
        public bool Done { get { return (Success || Failure); } }
    }
}

