using System;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Extensions.Configuration;

namespace Assimalign.Azure.WebJobs.Extensions;

[Extension("NotificationHubExtensions")]
internal sealed class NotificationHubBindingExtensionProvider : IExtensionConfigProvider
{
    private readonly IConfiguration configuration;

    public NotificationHubBindingExtensionProvider(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public void Initialize(ExtensionConfigContext context)
    {
        BindNotificationHubClient(context);
    }

    private void BindNotificationHubClient(ExtensionConfigContext context) =>
        context.AddBindingRule<NotificationHubBindingAttribute>()
            .BindToInput<INotificationHubClient>(builder =>
            {
                return new NotificationHubClient(
                    configuration[builder.Connection] ?? builder.Connection, 
                    configuration[builder.Path] ?? builder.Path);
            });
}
