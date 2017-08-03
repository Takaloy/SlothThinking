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
            _container.Register<ISlothQueryService>((c, p) => new SlothQueryService(new RestClient("https://heroeslounge.gg/api/v1/")));
            _container.Register<ISlothAggregationService, SlothAggregationService>();
            _container.Register<ISlothRatingsCalculator, WeightedSlothRatingsCalculator>();

            _container.Register<IHotsLogsInfoService>((c, p) => new HotsLogsInfoService(
                new RestClient("https://api.hotslogs.com/"),
                _container.Resolve<ISlothRatingsCalculator>()));
        }
    }
}