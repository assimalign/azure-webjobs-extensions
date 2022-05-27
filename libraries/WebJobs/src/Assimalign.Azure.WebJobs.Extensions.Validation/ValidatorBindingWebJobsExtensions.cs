using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.DependencyInjection;

namespace Assimalign.Azure.WebJobs.Extensions;

using Assimalign.Extensions.Validation;
using Assimalign.Azure.WebJobs.Extensions.Internal;

/// <summary>
/// 
/// </summary>
public static class ValidatorBindingWebJobsExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IWebJobsBuilder AddValidatorBinding(this IWebJobsBuilder builder, Action<ValidatorBuilder> configure)
    {
        return builder.AddValidatorBinding(factory =>
        {
            factory.AddValidator("default", configure);
        });
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IWebJobsBuilder AddValidatorBinding(this IWebJobsBuilder builder, Action<ValidatorFactoryBuilder> configure)
    {
        builder.AddExtension(
            new ValidatorBindingExtensionsConfigProvider(
                ValidatorFactory.Configure(configure)));

        return builder;
    }
}