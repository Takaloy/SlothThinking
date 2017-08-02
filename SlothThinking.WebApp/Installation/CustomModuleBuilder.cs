using Nancy;
using Nancy.ModelBinding;
using Nancy.Routing;
using Nancy.Validation;
using Nancy.ViewEngines;

namespace SlothThinking.WebApp
{
    public class CustomModuleBuilder : INancyModuleBuilder
    {
        private readonly IModelBinderLocator _modelBinderLocator;
        private readonly IResponseFormatterFactory _responseFormatterFactory;
        private readonly IModelValidatorLocator _validatorLocator;
        private readonly IViewFactory _viewFactory;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DefaultNancyModuleBuilder" /> class.
        /// </summary>
        /// <param name="viewFactory">The <see cref="IViewFactory" /> instance that should be assigned to the module.</param>
        /// <param name="responseFormatterFactory">
        ///     An <see cref="IResponseFormatterFactory" /> instance that should be used to
        ///     create a response formatter for the module.
        /// </param>
        /// <param name="modelBinderLocator">A <see cref="IModelBinderLocator" /> instance that should be assigned to the module.</param>
        /// <param name="validatorLocator">A <see cref="IModelValidatorLocator" /> instance that should be assigned to the module.</param>
        public CustomModuleBuilder(IViewFactory viewFactory, IResponseFormatterFactory responseFormatterFactory,
            IModelBinderLocator modelBinderLocator,
            IModelValidatorLocator validatorLocator)
        {
            _viewFactory = viewFactory;
            _responseFormatterFactory = responseFormatterFactory;
            _modelBinderLocator = modelBinderLocator;
            _validatorLocator = validatorLocator;
        }

        /// <summary>
        ///     Builds a fully configured <see cref="INancyModule" /> instance, based upon the provided <paramref name="module" />.
        /// </summary>
        /// <param name="module">The <see cref="INancyModule" /> that should be configured.</param>
        /// <param name="context">The current request context.</param>
        /// <returns>A fully configured <see cref="INancyModule" /> instance.</returns>
        public INancyModule BuildModule(INancyModule module, NancyContext context)
        {
            module.Context = context;
            module.Response = _responseFormatterFactory.Create(context);
            module.ViewFactory = _viewFactory;
            module.ModelBinderLocator = _modelBinderLocator;
            module.ValidatorLocator = _validatorLocator;

            module.Before.AddItemToStartOfPipeline(ctx =>
            {
                var routeDescription = ctx.ResolvedRoute.Description;
                NewRelic.Api.Agent.NewRelic.SetTransactionName(module.GetType().Name, $"{routeDescription.Path}");
                return null;
            });

            return module;
        }
    }
}