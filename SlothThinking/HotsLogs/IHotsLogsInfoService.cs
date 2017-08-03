using System.Threading.Tasks;

namespace SlothThinking
{
    public interface IHotsLogsInfoService
    {
        Task<int> GetRatingFor(ILoungeTeam team);
        Task<int> GetRatingFor(ISloth player);
    }
}
