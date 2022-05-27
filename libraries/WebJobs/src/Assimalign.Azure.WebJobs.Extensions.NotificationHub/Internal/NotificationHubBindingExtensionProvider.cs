using System;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Assimalign.Azure.WebJobs.Extensions;

[Extension("NotificationHubExtensions")]
internal sealed class NotificationHubBindingExtensionProvider : IExtensionConfigProvider
{
    private readonly IConfiguration configuration;
    private readonly NotificationHubBindingOptions options;

    public NotificationHubBindingExtensionProvider(IConfiguration configuration, IOptions<NotificationHubBindingOptions> options)
    {
        this.configuration = configuration;
        this.options = options.Value;
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
            .BindToInput<INotificationHubClient>(attribute =>
            {
                NotificationHubSettings settings = default;

                if (!string.IsNullOrEmpty(attribute.SettingsName))
                {
                    settings = options.Settings[attribute.SettingsName];
                }                
                if (string.IsNullOrEmpty(attribute.Connection))
                {
                    return new NotificationHubClient(
                        configuration[options.Connection] ?? options.Connection,
                        configuration[attribute.Path] ?? attribute.Path, 
                        settings);
                }
                else
                {
                    return new NotificationHubClient(
                        configuration[attribute.Connection] ?? attribute.Connection,
                        configuration[attribute.Path] ?? attribute.Path,
                        settings);
                }
            });
}