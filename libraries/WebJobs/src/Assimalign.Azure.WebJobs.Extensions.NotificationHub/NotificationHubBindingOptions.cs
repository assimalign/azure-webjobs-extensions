using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.NotificationHubs;

namespace Assimalign.Azure.WebJobs.Extensions;

/// <summary>
/// Default options to be used for the Azure Notification Hub bindings.
/// </summary>
public sealed class NotificationHubBindingOptions
{

    /// <summary>
    /// 
    /// </summary>
    public NotificationHubBindingOptions()
    {
        this.Settings = new(StringComparer.InvariantCultureIgnoreCase);
    }

    internal ConcurrentDictionary<string, NotificationHubSettings> Settings { get; }

    /// <summary>
    /// The default connection string to be used connecting to the Azure Notification Hub Namespace.
    /// </summary>
    /// <remarks>
    /// This can either be a configuration value or valid connection string.
    /// </remarks>
    public string Connection { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="settingsName"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public NotificationHubBindingOptions AddClientSettings(string settingsName, Action<NotificationHubSettings> configure)
    {
        var settings = new NotificationHubSettings();

        configure.Invoke(settings);

        this.Settings[settingsName] = settings;

        return this;
    }
}
