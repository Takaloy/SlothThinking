using System.Collections.Generic;

namespace SlothThinking
{
    public interface ILoungeTeam
    {
        int Id { get; }
        string Title { get; }
        int SlothRating { get; }
        IEnumerable<ISloth> Players { get; }
    }
}