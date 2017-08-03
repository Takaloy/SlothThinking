using System;
using System.Collections.Generic;

namespace SlothThinking
{
    public interface ILoungeTeam : ISlothTeamInfo
    {
        IEnumerable<ISloth> Players { get; }
    }

    public class LoungeTeam : ILoungeTeam
    {
        private readonly ISlothTeamInfo _teamInfo;

        public LoungeTeam(ISlothTeamInfo teamInfo, IEnumerable<ISloth> sloths)
        {
            if (teamInfo == null) throw new ArgumentNullException(nameof(teamInfo));
            if (sloths == null) throw new ArgumentNullException(nameof(sloths));

            _teamInfo = teamInfo;
            Players = sloths;
        }

        public int Id => _teamInfo.Id;
        public string Title => _teamInfo.Title;
        public int SlothRating => _teamInfo.SlothRating;

        public IEnumerable<ISloth> Players { get; }
    }
}