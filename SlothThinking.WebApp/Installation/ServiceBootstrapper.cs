using Nancy;
using Nancy.TinyIoc;

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
}