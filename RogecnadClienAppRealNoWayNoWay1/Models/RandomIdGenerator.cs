using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogecnadClienAppRealNoWayNoWay.Models
{
    internal class RandomIdGenerator
    {
        const string PUSH_CHARS = "-0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ_abcdefghijklmnopqrstuvwxyz";

        // Timestamp of last push, used to prevent local collisions if you push twice in one ms.
        private static long lastPushTime = 0;
        private static char[] lastRandChars = new char[12];

        // Random number generator
        private static Random rng = new Random();

        public static string GeneratePushID()
        {
            var now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            Console.WriteLine("now=" + now);

            var duplicateTime = (now == lastPushTime);
            lastPushTime = now;

            var timeStampChars = new char[8];
            for (var i = 7; i >= 0; i--)
            {
                timeStampChars[i] = PUSH_CHARS[(int)(now % 64)];
                now = now >> 6;
            }
            if (now != 0) throw new Exception("We should have converted the entire timestamp.");

            var id = string.Join(string.Empty, timeStampChars);

            if (!duplicateTime)
            {
                for (var i = 0; i < 12; i++)
                {
                    lastRandChars[i] = (char)rng.Next(0, 63);
                }
            }
            else
            {
                // If the timestamp hasn't changed since last push, use the same random number, except incremented by 1.
                int i;
                for (i = 11; i >= 0 && lastRandChars[i] == 63; i--)
                {
                    lastRandChars[i] = (char)0;
                }
                lastRandChars[i]++;
            }
            for (var i = 0; i < 12; i++)
            {
                id += PUSH_CHARS[lastRandChars[i]];
            }
            if (id.Length != 20) throw new Exception("Length should be 20.");

            return id;
        }

        public static long ConvertPushID(string id)
        {
            var timestamp = 0L;
            for (var i = 0; i < 8; i++)
            {
                var n = PUSH_CHARS.IndexOf(id[i]);
                timestamp = (timestamp << 6) + n;
            }
            return timestamp;
        }
    }
}
