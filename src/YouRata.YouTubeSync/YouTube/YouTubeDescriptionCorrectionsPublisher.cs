using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouRata.Common.Configuration.YouTube;
using YouRata.Common.YouTube;

namespace YouRata.YouTubeSync.YouTube
{
    internal static class YouTubeDescriptionCorrectionsPublisher
    {

        public static string GetUpdatedDescription(string description, string corrections, YouTubeConfiguration config)
        {
            if (!string.IsNullOrWhiteSpace(description))
            {
                int existingCorrectionsStart = description.IndexOf(YouTubeConstants.CorrectionBegin);
                if (existingCorrectionsStart >= 0)
                {
                    int existingCorrectionsLength = 0;
                    int existingCorrectionsEnd = description.IndexOf(config.CorrectionsCloser, existingCorrectionsStart);
                    if (existingCorrectionsEnd > 0)
                    {
                        existingCorrectionsLength = ((existingCorrectionsEnd + config.CorrectionsCloser.Length) - existingCorrectionsStart);
                    }
                    else
                    {
                        existingCorrectionsLength = (description.Length - existingCorrectionsStart);
                    }
                    description = description.Remove(existingCorrectionsStart, existingCorrectionsLength);
                }
                if ((corrections.Length + description.Length) > YouTubeConstants.MaxDescriptionLength && config.TruncateDescriptionOverflow)
                {
                    // Old description is too long to add errata link text, truncate it
                    description = description.Substring(0, description.Length - corrections.Length);
                }
            }
            return description + Environment.NewLine + corrections;
        }
    }
}
