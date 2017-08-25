namespace SlothThinking
{
    public class TeamRatingAccordingToHotsLogs : ITeamRating
    {
        public decimal PlayersRated { get; set; }
        public int WeightedRating { get; set; }
        public decimal Certainty { get; set; }

        public static ITeamRating Empty()
        {
            return new TeamRatingAccordingToHotsLogs()
            {
                Certainty = new decimal(0.0),
                PlayersRated = new decimal(0.0),
                WeightedRating = 0
            };
        }
    }
}