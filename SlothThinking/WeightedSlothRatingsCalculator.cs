namespace SlothThinking
{
    public interface ISlothRatingsCalculator
    {
        int GetRatingFor(IHotsLogsPlayerProfile sloth);
    }

    public class WeightedSlothRatingsCalculator : ISlothRatingsCalculator
    {
        public int GetRatingFor(IHotsLogsPlayerProfile sloth)
        {
            var totalWeight = 0;
            var totalPoints = 0;

            foreach (var ranking in sloth.LeaderboardRankings)
            {
                if (ranking.LeagueRank == null)
                    continue;

                var weight = GetWeightFor(ranking.GameMode);
                totalWeight += weight;
                totalPoints += ranking.CurrentMMR * weight;
            }

            if (totalWeight == 0)
                return 0;

            return totalPoints / totalWeight;
        }

        private static int GetWeightFor(GameMode mode)
        {
            switch (mode)
            {
                case GameMode.QuickMatch:
                    return 0;
                case GameMode.HeroLeague:
                    return 5;
                case GameMode.TeamLeague:
                    return 3;
                case GameMode.UnrankedDraft:
                    return 2;
                default:
                    return 0;
            }
        }
    }
}