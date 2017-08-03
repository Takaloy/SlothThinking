using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using RestSharp;

namespace SlothThinking
{

    public class HotsLogsInfoService : IHotsLogsInfoService
    {
        private readonly IRestClient _restClient;
        private readonly ISlothRatingsCalculator _calculator;

        public HotsLogsInfoService(IRestClient restClient, ISlothRatingsCalculator calculator)
        {
            if (restClient == null) throw new ArgumentNullException(nameof(restClient));
            if (calculator == null) throw new ArgumentNullException(nameof(calculator));

            _restClient = restClient;
            _calculator = calculator;
        }

        public async Task<int> GetRatingFor(ILoungeTeam team)
        {
            var playerWithRatings = new Dictionary<ISloth, int>();

            foreach (var player in team.Players)
            {
                var rating = await GetRatingFor(player);

                if (rating > 0)
                    playerWithRatings.Add(player, rating);
            }

            if (playerWithRatings.Count == 0)
                return 0;

            var totalRating = playerWithRatings.Values.Sum();
            return totalRating / playerWithRatings.Count;
        }

        public async Task<int> GetRatingFor(ISloth player)
        {
            var restRequest = new RestRequest($"/Public/Players/2/{player.BattleTag.Replace("#", "_")}");
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            var response = await _restClient.ExecuteTaskAsync<HotsLogsPlayerProfile>(restRequest, cts.Token);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.StatusCode.ToString());

            if (response.Data == null)
                return 0;

            var rating = _calculator.GetRatingFor(response.Data);
            return rating;
        }
    }
}
