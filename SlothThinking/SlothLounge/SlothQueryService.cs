using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using RestSharp.Extensions;

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

            var response = await _restClient.ExecuteTaskAsync<List<SlothTeamInfo>>(restRequest, cancellationToken.Token).ConfigureAwait(false);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception($"Status Code {response.StatusCode}. {response.ErrorMessage}");

            return response.Data;
        }

        public async Task<IEnumerable<ISloth>> GetPlayers(int teamId)
        {
            var restRequest = new RestRequest($"/teams/{teamId}/sloths");
            var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(TIMEOUT_IN_SECONDS));

            var response = await _restClient.ExecuteTaskAsync<List<Sloth>>(restRequest, cancellationToken.Token).ConfigureAwait(false);

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
        /// BUG: does not serealize understore to pascalcase correctly
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ILoungeReplay>> GetReplaysForMatch(int matchId)
        {
            var restRequest = new RestRequest($"/matches/{matchId}/replays");
            restRequest.AddHeader("Accept", "application/json");
            var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(TIMEOUT_IN_SECONDS));

            var response = await _restClient.ExecuteTaskAsync(restRequest, cancellationToken.Token).ConfigureAwait(false);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception($"Status Code {response.StatusCode}. {response.ErrorMessage}");

            var content = JsonConvert.DeserializeObject<List<LoungeReplay>>(response.Content);
            return content;
        }

        /// <summary>
        /// not adding this to interface YET. as I think we should figure if we want to save it locally or use the API as a passthrough
        /// </summary>
        /// <param name="replays"></param>
        /// <param name="targetDirectory"></param>
        public void SaveReplaysTo(IEnumerable<ILoungeReplay> replays, string targetDirectory)
        {
            if (!Directory.Exists(targetDirectory))
                Directory.CreateDirectory(targetDirectory);

            foreach (var loungeReplay in replays)
            {
                
                var restClient = new RestClient(loungeReplay.Path);
                var saveFileName = Path.Combine(targetDirectory, $"{loungeReplay.Id}.{loungeReplay.Extension}");
                restClient.DownloadData(new RestRequest()).SaveAs(saveFileName);
            }
        }
    }
}
