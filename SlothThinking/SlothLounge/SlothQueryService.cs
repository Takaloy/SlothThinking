using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace SlothThinking
{
    public class SlothQueryService : ISlothQueryService
    {
        private readonly IRestClient _restClient;
        private const int TIMEOUT_IN_SECONDS = 10;

        public SlothQueryService(IRestClient restClient)
        {
            _restClient = restClient ?? throw new ArgumentNullException(nameof(restClient));
        }

        public async Task<IEnumerable<ISlothTeamInfo>> GetTeams(int division)
        {
            var restRequest = new RestRequest($"/divisions/{division}/teams");
            var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(TIMEOUT_IN_SECONDS));

            var response = await _restClient.ExecuteTaskAsync<List<SlothTeamInfo>>(restRequest, cancellationToken.Token);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception($"Status Code {response.StatusCode}. {response.ErrorMessage}");

            return response.Data;
        }

        public async Task<IEnumerable<ISloth>> GetPlayers(int teamId)
        {
            var restRequest = new RestRequest($"/teams/{teamId}/sloths");
            var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(TIMEOUT_IN_SECONDS));

            var response = await _restClient.ExecuteTaskAsync<List<Sloth>>(restRequest, cancellationToken.Token);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception($"Status Code {response.StatusCode}. {response.ErrorMessage}");

            return response.Data;
        }

        public IEnumerable<ISloth> GetPlayersSync(int teamId)
        {
            var restRequest = new RestRequest($"/teams/{teamId}/sloths");

            var response = _restClient.Execute<List<Sloth>>(restRequest);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception($"Status Code {response.StatusCode}. {response.ErrorMessage}");

            return response.Data;
        }

        /// <summary>
        /// it is annoying having to have a seperate package (newtonsoft) to manage this.
        /// submitted https://github.com/kealsera/rikki/issues/30 
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ILoungeReplay>> GetReplaysForMatch(int matchId)
        {
            var restRequest = new RestRequest($"/matches/{matchId}/replays");
            restRequest.AddHeader("Accept", "application/json");
            var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(TIMEOUT_IN_SECONDS));

            var response = await _restClient.ExecuteTaskAsync(restRequest, cancellationToken.Token);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception($"Status Code {response.StatusCode}. {response.ErrorMessage}");

            var content = JsonConvert.DeserializeObject<List<LoungeReplay>>(response.Content);
            return content;
        }
    }
}
