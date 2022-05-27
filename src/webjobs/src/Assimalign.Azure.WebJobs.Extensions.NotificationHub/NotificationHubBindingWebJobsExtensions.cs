using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace Assimalign.Azure.WebJobs.Extensions;

/// <summary>
/// 
/// </summary>
public static class NotificationHubBindingWebJobsExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IWebJobsBuilder AddNotificationBinding(this IWebJobsBuilder builder)
    {
        builder.AddExtension<NotificationHubBindingExtensionProvider>();
        return builder;
    }
}
