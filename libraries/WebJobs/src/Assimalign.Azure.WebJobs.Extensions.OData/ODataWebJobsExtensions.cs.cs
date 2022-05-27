using System;
using System.Linq;
using Microsoft.OData.UriParser;
using Microsoft.Extensions.DependencyInjection;


namespace Microsoft.Azure.WebJobs
{
    using Assimalign.Azure.WebJobs.Bindings.OData;


    public static class ODataWebJobsExtensions
    {
        public static IWebJobsBuilder AddOData(this IWebJobsBuilder builder)
        {
            // Setup per-route dependency injection. When routes are added, additional
            // per-route classes will be injected, such as IEdmModel and IODataRoutingConventions.
            builder.Services.AddSingleton<IPerRouteContainer, PerRouteContainer>();

            // Add OData and query options. Opting not to use IConfigurationOptions in favor of
            // fluent extensions APIs to IRouteBuilder.
            builder.Services.AddSingleton<ODataOptions>();
            builder.Services.AddSingleton<DefaultQuerySettings>();

            // Add the batch path mapping class to store batch route names and prefixes.
            builder.Services.AddSingleton<ODataBatchPathMapping>();

            // We need to decorate the ActionSelector.
            var selector = services.First(s => s.ServiceType == typeof(IActionSelector) && s.ImplementationType != null);
            builder.Services.Remove(selector);
            builder.Services.Add(new ServiceDescriptor(selector.ImplementationType, selector.ImplementationType, ServiceLifetime.Singleton));

            // Add our action selector. The ODataActionSelector creates an ActionSelector in it's constructor
            // and pass all non-OData calls to this inner selector.
            builder.Services.AddSingleton<IActionSelector>(s =>
            {
                return new ODataActionSelector(
                    (IActionSelector)s.GetRequiredService(selector.ImplementationType),
                    (IModelBinderFactory)s.GetRequiredService(typeof(IModelBinderFactory)),
                    (IModelMetadataProvider)s.GetRequiredService(typeof(IModelMetadataProvider)));
            });

            builder.Services.AddSingleton<ODataEndpointRouteValueTransformer>();

            // OData Endpoint selector policy
            builder.Services.AddSingleton<MatcherPolicy, ODataEndpointSelectorPolicy>();

            // LinkGenerator
            var linkGenerator = builder.Services.First(s => s.ServiceType == typeof(LinkGenerator) && s.ImplementationType != null);
            builder.Services.Remove(linkGenerator);
            builder.Services.Add(new ServiceDescriptor(linkGenerator.ImplementationType, linkGenerator.ImplementationType, ServiceLifetime.Singleton));

            builder.Services.AddSingleton<LinkGenerator>(s =>
            {
                return new ODataEndpointLinkGenerator((LinkGenerator)s.GetRequiredService(linkGenerator.ImplementationType));
            });

            // Add the ActionContextAccessor; this allows access to the ActionContext which is needed
            // during the formatting process to construct a IUrlHelper.
            builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            return new ODataBuilder(services);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IWebJobsBuilder AddHttpAccessorBinding(this IWebJobsBuilder builder)
        {
            builder.Services.AddHttpContextAccessor();
            var functions = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.DefinedTypes.SelectMany(b => b.DeclaredMethods))
                .Where(x => x.CustomAttributes.Where(c => c.AttributeType == typeof(FunctionNameAttribute)).Any());

            builder.Services.AddOptions<ODataOptions>()
                .Configure(options =>
                {
                    foreach (var function in functions)
                    {
                        var functionName = function.CustomAttributes
                            .FirstOrDefault(x => x.AttributeType == typeof(FunctionNameAttribute)).ConstructorArguments
                            .First().Value.ToString();

                        var parameterType = function.GetParameters()
                            .FirstOrDefault(x => x.CustomAttributes.Any(x => x.AttributeType == typeof(ODataResourceAttribute)))?.ParameterType;

                        if (functionName != null && parameterType != null)
                        {
                            options.Types.Add(functionName, parameterType);
                        }
                    }
                });

            builder.AddExtension<HttpAccessorExtensionProvider>();
            return builder;
        }

    }
}
