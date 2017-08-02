using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using RestSharp;

namespace SlothThinking.WebApp
{
    public class ServiceBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {

            base.ConfigureApplicationContainer(container);
            var helper = new ContainerRegistration(container);
            helper.Register();
        }
    }

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