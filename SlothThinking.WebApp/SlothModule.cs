using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nancy;
using Nancy.Routing;

namespace SlothThinking.WebApp
{
    public class SlothModule : NancyModule
    {
        private readonly SlothAggregationService _service;

        public SlothModule(SlothAggregationService service)
        {
            _service = service;

            Get["/v1/teams", true] =
                async (parameters, ct) => await GetTeams(parameters);
        }

        private async Task<dynamic> GetTeams(dynamic parameters)
        {
            int divisionId = int.Parse(Request.Query["division"]);
            string team = Request.Query["team"]?.ToString();
            return await _service.Get(divisionId, team);
        }
    }
}