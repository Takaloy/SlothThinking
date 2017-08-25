namespace SlothThinking
{
    public interface ITeamRating
    {
        /// <summary>
        /// percentage of players rated
        /// </summary>
        decimal PlayersRated { get; }

        /// <summary>
        /// weighted rating based on the rules engine
        /// </summary>
        int WeightedRating { get; }

        /// <summary>
        /// certainty calculation
        /// </summary>
        decimal Certainty { get; }
    }
}