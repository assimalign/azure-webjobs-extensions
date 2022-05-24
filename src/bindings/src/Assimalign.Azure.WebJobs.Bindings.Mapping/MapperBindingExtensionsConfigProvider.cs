using System;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Config;

namespace Assimalign.Azure.WebJobs.Bindings;

using Assimalign.Extensions.Mapping;


[Extension("MapperExtension")]
internal sealed class MapperBindingExtensionsConfigProvider : IExtensionConfigProvider
{
    private readonly IMapperFactory factory;

    public MapperBindingExtensionsConfigProvider(IMapperFactory factory)
    {
       this.factory = factory;
    }

    public void Initialize(ExtensionConfigContext context)
    {
        context.AddBindingRule<MapperBindingAttribute>()
            .BindToInput(attribute =>
            {
                return factory.CreateMapper(attribute.MapperName);
            });
    }
}
