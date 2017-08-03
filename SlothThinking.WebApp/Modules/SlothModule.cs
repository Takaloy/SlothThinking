using System;
using System.Threading.Tasks;
using Nancy;

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
            int divisionId = int.Parse(Request.Query["division"]);
            return await _service.Get(divisionId);
        }
    }
}