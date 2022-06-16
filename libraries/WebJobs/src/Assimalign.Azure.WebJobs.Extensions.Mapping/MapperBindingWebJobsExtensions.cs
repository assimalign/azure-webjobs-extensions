using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.DependencyInjection;

namespace Assimalign.Azure.WebJobs.Extensions;

using Assimalign.Extensions.Mapping;

/// <summary>
/// 
/// </summary>
public static class MapperBindingWebJobsExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IWebJobsBuilder AddMapperBinding(this IWebJobsBuilder builder, Action<MapperBuilder> configure)
    {
        return builder.AddMapperBinding(factory =>
        {
            factory.AddMapper("default", configure);
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IWebJobsBuilder AddMapperBinding(this IWebJobsBuilder builder, Action<MapperFactoryBuilder> configure)
    {
        builder.AddExtension(
            new MapperBindingExtensionsConfigProvider(
                MapperFactory.Configure(configure)));

        return builder;
    }
}
