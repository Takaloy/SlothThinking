using System.Collections.Generic;
using System.Threading.Tasks;

namespace SlothThinking
{
    public interface ISlothQueryService
    {
        Task<IEnumerable<ISlothTeamInfo>> GetTeams(int division);
        Task<IEnumerable<ISloth>> GetPlayers(int teamId);
        IEnumerable<ISloth> GetPlayersSync(int teamId);
        Task<IEnumerable<ILoungeReplay>> GetReplaysForMatch(int matchId);
    }
}