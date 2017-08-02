using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlothThinking.WebApp
{
    public class SlothAggregationService
    {
        private readonly ISlothQueryService _slothQueryService;

        public SlothAggregationService(ISlothQueryService slothQueryService)
        {
            if (slothQueryService == null) throw new ArgumentNullException(nameof(slothQueryService));
            _slothQueryService = slothQueryService;
        }

        public async Task<IEnumerable<ILoungeTeam>> Get(int division, string team = null)
        {
            var teams = await _slothQueryService.GetTeams(division);

            if (!string.IsNullOrEmpty(team))
            {
                team = team.ToLower();
                teams = teams.Where(x => x.Title.ToLower().Contains(team));
            }

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