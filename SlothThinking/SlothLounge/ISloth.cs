namespace SlothThinking
{
    public interface ISloth
    {
        int Id { get; }

        string Title { get; }

        string BattleTag { get; }

        /// <summary>
        ///     only ranked mode MMR
        /// </summary>
        int Mmr { get; }

        /// <summary>
        ///     includes both ranked and unranked mode MMR
        /// </summary>
        int AllMmr { get; }
    }
}