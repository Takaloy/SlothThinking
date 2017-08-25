using System.Threading.Tasks;

namespace SlothThinking
{
    public interface IHotsLogsInfoService
    {
        Task<ITeamRating> GetRatingFor(ILoungeTeam team);
        Task<int> GetRatingFor(ISloth player);
    }
}
