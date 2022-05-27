using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.DependencyInjection;

namespace Assimalign.Azure.WebJobs.Extensions;

/// <summary>
/// 
/// </summary>
public static class NotificationHubBindingWebJobsExtensions
{
    private static Action<NotificationHubBindingOptions, IServiceProvider> OptionsConfiguration { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IWebJobsBuilder AddNotificationBinding(this IWebJobsBuilder builder, Action<NotificationHubBindingOptions> configure)
    {
        OptionsConfiguration = (options, serviceProvider) =>
        {
            configure.Invoke(options);
        };

        builder.Services.AddOptions<NotificationHubBindingOptions>()
            .Configure(OptionsConfiguration);

        return builder;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IWebJobsBuilder AddNotificationBinding(this IWebJobsBuilder builder, Action<NotificationHubBindingOptions, IServiceProvider> configure)
    {
        OptionsConfiguration = configure;

        builder.Services.AddOptions<NotificationHubBindingOptions>()
            .Configure(OptionsConfiguration);

        return builder;
    }
}
