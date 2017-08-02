using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace SlothThinking
{
    public interface ISlothQueryService
    {
        Task<IEnumerable<ISlothTeam>> GetTeams(int division);
        Task<IEnumerable<ISloth>> GetPlayers(int teamId);
    }

    public class SlothQueryService : ISlothQueryService
    {
        private readonly IRestClient _restClient;
        private const int TIMEOUT_IN_SECONDS = 2;

        public SlothQueryService(IRestClient restClient)
        {
            _restClient = restClient ?? throw new ArgumentNullException(nameof(restClient));
        }

        public async Task<IEnumerable<ISlothTeam>> GetTeams(int division)
        {
            var restRequest = new RestRequest($"/divisions/{division}/teams");
            var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(TIMEOUT_IN_SECONDS));

            var response = await _restClient.ExecuteTaskAsync<List<SlothTeam>>(restRequest, cancellationToken.Token);

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
    }
}
