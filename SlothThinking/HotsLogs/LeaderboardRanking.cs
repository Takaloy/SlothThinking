using System.Diagnostics.CodeAnalysis;

namespace SlothThinking
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class LeaderboardRanking
    {
        public GameMode GameMode { get; set; }
        public int? LeagueID { get; set; }
        public int? LeagueRank { get; set; }
        public int CurrentMMR { get; set; }
    }
}