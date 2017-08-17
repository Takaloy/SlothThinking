using System;
using System.Threading.Tasks;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses;

namespace SlothThinking.WebApp
{
    public class SlothModule : NancyModule
    {
        private readonly ISlothAggregationService _service;

        public SlothModule(ISlothAggregationService service)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            _service = service;

            Get["/v1/teams", true] =
                async (parameters, ct) => await GetTeams(parameters);

            Get["/v1/players", true] = async (parameters, ct) =>
            {
                return await Task.FromResult(GetPlayers(parameters));
            };
           

            Post["/v1/teams", true] = async (x, ct) =>
            {
                var request = this.Bind<TeamsRequest>();
                return await _service.Get(request.Division);
            };
        }

        private dynamic GetPlayers(dynamic parameters)
        {
            var o = Request.Query["team"];
            if (!o.HasValue)
                return new JsonResponse("the parameter team must be specified", new DefaultJsonSerializer())
                {
                    StatusCode = HttpStatusCode.BadRequest
                };

            int teamId = int.Parse(o);
            return  _service.GetPlayersForTeam(teamId);
        }

        private async Task<dynamic> GetTeams(dynamic parameters)
        {

            var o = Request.Query["division"];
            if (!o.HasValue)
                return new JsonResponse("the parameter division must be specified", new DefaultJsonSerializer())
                {
                    StatusCode = HttpStatusCode.BadRequest
                };

            int division = int.Parse(o);
            return await _service.Get(division);
        }
    }

    public class TeamsRequest
    {
        public int Division { get; set; }
    }
}