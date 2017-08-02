using Nancy.TinyIoc;
using RestSharp;

namespace SlothThinking.WebApp
{
    public class ContainerRegistration
    {
        private readonly TinyIoCContainer _container;

        public ContainerRegistration(TinyIoCContainer container)
        {
            _container = container;
        }

        public void Register()
        {
            _container.Register<IRestClient>((c, p) => new RestClient("https://heroeslounge.gg/api/v1/"));
            _container.Register<ISlothQueryService, SlothQueryService>();
            _container.Register<SlothAggregationService>();
        }
    }
}