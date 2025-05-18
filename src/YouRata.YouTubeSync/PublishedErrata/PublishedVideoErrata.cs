using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace YouRata.YouTubeSync.PublishedErrata
{
    internal class PublishedVideoErrata
    {
        private readonly List<string> _errataEntries;
        private const string _timeValueDashes = "------------------------------------------------";

        public PublishedVideoErrata()
        {
            _errataEntries = new List<string>();
        }

        internal static PublishedVideoErrata BuildFromBulletin(string bulletin)
        {
            PublishedVideoErrata bulletinErrata = new PublishedVideoErrata();
            if (string.IsNullOrEmpty(bulletin)) return bulletinErrata;
            string zeroTimeValueMark = $"00 {_timeValueDashes}";
            int zeroTimeValueStart = bulletin.IndexOf(zeroTimeValueMark, StringComparison.InvariantCulture);
            if (zeroTimeValueStart <= 0) return bulletinErrata;
            // Remove the first time value mark
            string errataBulletinText = StripTimeValueMarks(bulletin.Remove(0, (zeroTimeValueStart + zeroTimeValueMark.Length)));
            bulletinErrata.ErrataEntries.AddRange(SplitErrataLines(errataBulletinText));
            return bulletinErrata;
        }

        private static List<string> SplitErrataLines(string rawBulletin)
        {
            if (string.IsNullOrWhiteSpace(rawBulletin)) return new List<string>();
            string[] bulletinLines = rawBulletin.Split(Environment.NewLine);
            List<string> errataLines = new List<string>();
            StringBuilder errataLineBuilder = new StringBuilder();
            foreach (string bulletinLine in bulletinLines)
            {
                string bulletinData = bulletinLine.Trim();
                if (Regex.IsMatch(bulletinData, @"\d{1,2}(:\d{2}){1,2}\b"))
                {
                    if (errataLineBuilder.Length > 0)
                    {
                        errataLines.Add(errataLineBuilder.ToString());
                        errataLineBuilder.Clear();
                    }
                    errataLineBuilder.Append(bulletinData);
                }
                else if (!string.IsNullOrWhiteSpace(bulletinData))
                {
                    errataLineBuilder.Append(" " + bulletinData);
                }
            }
            // Add the last line if not already done
            if (errataLineBuilder.Length > 0)
            {
                errataLines.Add(errataLineBuilder.ToString());
            }
            return errataLines;
        }

        private static string StripTimeValueMarks(string timeMarkedBulletin)
        {
            return Regex.Replace(timeMarkedBulletin, @"(?m)^(\d{1,2}:)?\d{1,2}:\d{2}\s*-+\s*$", string.Empty);
        }

        public List<string> ErrataEntries => _errataEntries;
    }
}
