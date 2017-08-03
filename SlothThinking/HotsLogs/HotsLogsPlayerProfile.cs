using System.Collections.Generic;

namespace SlothThinking
{
    public class HotsLogsPlayerProfile : IHotsLogsPlayerProfile
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public List<LeaderboardRanking> LeaderboardRankings { get; set; } = new List<LeaderboardRanking>();
    }
}