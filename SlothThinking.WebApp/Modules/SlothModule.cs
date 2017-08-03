using System;
using System.Threading.Tasks;
using Nancy;
using Nancy.Responses;

namespace SlothThinking.WebApp
{
    public class SlothModule : NancyModule
    {
        private readonly ISlothAggregationService _service;

        public SlothModule(ISlothAggregationService service)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));

            _service = service;

            Get["/v1/teams", true] =
                async (parameters, ct) => await GetTeams(parameters);
        }

        private async Task<dynamic> GetTeams(dynamic parameters)
        {
            var o = Request.Query["division"];
            if (!o.HasValue)
                return new JsonResponse("the parameter division must be specified", new DefaultJsonSerializer())
                {
                    StatusCode = HttpStatusCode.BadRequest
                };

            int divisionId = int.Parse(o);
            return await _service.Get(divisionId);
        }
    }
}