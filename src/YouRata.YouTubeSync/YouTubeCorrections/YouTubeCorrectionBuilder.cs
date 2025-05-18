using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouRata.Common.Configuration.YouTube;
using YouRata.Common.YouTube;
using YouRata.YouTubeSync.PublishedErrata;

namespace YouRata.YouTubeSync.YouTubeCorrections
{
    internal sealed class YouTubeCorrectionBuilder
    {
        // --- YouTube error correction template ---
        private readonly YouTubeConfiguration _configuration;
        private readonly PublishedVideoErrata _errata;

        public YouTubeCorrectionBuilder(YouTubeConfiguration config, PublishedVideoErrata errata)
        {
            _configuration = config;
            _errata = errata;
        }

        public string Build()
        {
            string youtubeCorrections = string.Empty;
            if (_errata.ErrataEntries.Count > 0)
            {
                AddBeginning(ref youtubeCorrections);
                AddErrata(ref youtubeCorrections);
                AddCloser(ref youtubeCorrections);
            }

            return youtubeCorrections;
        }

        private void AddBeginning(ref string correctionsTemplate)
        {
            correctionsTemplate = $"{YouTubeConstants.CorrectionBegin}{Environment.NewLine}";
        }

        private void AddErrata(ref string correctionsTemplate)
        {
            foreach(string errataEntry in _errata.ErrataEntries)
            {
                correctionsTemplate += errataEntry + Environment.NewLine;
            }
        }

        private void AddCloser(ref string correctionsTemplate)
        {
            correctionsTemplate += _configuration.CorrectionsCloser + Environment.NewLine;
        }
    }
}
