using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SlothThinking
{
    public interface ISlothAggregationService
    {
        Task<IEnumerable<ILoungeTeam>> Get(int division);
    }

    public class SlothAggregationService : ISlothAggregationService
    {
        private readonly ISlothQueryService _slothQueryService;

        public SlothAggregationService(ISlothQueryService slothQueryService)
        {
            if (slothQueryService == null)
                throw new ArgumentNullException(nameof(slothQueryService));

            _slothQueryService = slothQueryService;
        }

        public async Task<IEnumerable<ILoungeTeam>> Get(int division)
        {
            var teams = await _slothQueryService.GetTeams(division);

            var result = new List<ILoungeTeam>();
            foreach (var slothTeam in teams)
            {
                var players = await _slothQueryService.GetPlayers(slothTeam.Id);
                result.Add(new LoungeTeam(slothTeam, players));
            }

            return result;
        }
    }
}