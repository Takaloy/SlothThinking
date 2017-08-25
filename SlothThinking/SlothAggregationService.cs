using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlothThinking
{
    public interface ISlothAggregationService
    {
        Task<IEnumerable<ILoungeTeam>> Get(int division);
        IEnumerable<ISloth> GetPlayersForTeam(int teamId);
        
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
            if (division < 1 || division > 5)
                throw new ArgumentException($"don't think we have division {division} .. ");    //https://heroeslounge.gg/api/v1/divisions/ service for checks in future

            var teams = await _slothQueryService.GetTeams(division).ConfigureAwait(false);

            var tasks = teams.Select(Get).ToList();
            return (await Task.WhenAll(tasks).ConfigureAwait(false)).ToList();
        }

        public IEnumerable<ISloth> GetPlayersForTeam(int teamId)
        {
            var players = _slothQueryService.GetPlayersSync(teamId);
            return players;
        }

        private async Task<ILoungeTeam> Get(ISlothTeamInfo slothTeam)
        {
            var players = await _slothQueryService.GetPlayers(slothTeam.Id).ConfigureAwait(false);
            var loungeTeam = new LoungeTeam(slothTeam, players);
            return loungeTeam;
        }
    }
}