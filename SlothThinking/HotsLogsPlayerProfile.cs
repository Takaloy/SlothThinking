using System;
using System.Collections.Generic;
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

    public interface IHotsLogsPlayerProfile
    {
        int PlayerId { get; }
        string Name { get; }
        List<LeaderboardRanking> LeaderboardRankings { get; }
    }

    public class HotsLogsPlayerProfile : IHotsLogsPlayerProfile
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public List<LeaderboardRanking> LeaderboardRankings { get; set; } = new List<LeaderboardRanking>();
    }

    public enum GameMode
    {
        QuickMatch,
        HeroLeague,
        TeamLeague,
        UnrankedDraft
    }
}