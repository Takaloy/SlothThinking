using System.Collections.Generic;

namespace SlothThinking
{
    public interface IHotsLogsPlayerProfile
    {
        int PlayerId { get; }
        string Name { get; }
        List<LeaderboardRanking> LeaderboardRankings { get; }
    }
}