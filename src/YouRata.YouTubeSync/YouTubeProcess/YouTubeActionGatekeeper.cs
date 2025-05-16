using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouRata.Common.Proto;

namespace YouRata.YouTubeSync.YouTubeProcess
{
    public static class YouTubeActionGatekeeper
    {
        public static bool CanStartVideoUpdate(GitHubActionEnvironment actionEnvironment)
        {
            return (actionEnvironment.EnvGitHubEventName.Equals("schedule") || actionEnvironment.EnvGitHubEventName.Equals("workflow_dispatch"));
        }

        public static bool CanStartCorrections(GitHubActionEnvironment actionEnvironment)
        {
            return (actionEnvironment.EnvGitHubEventName.Equals("push"));
        }
    }
}
