using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Description;

namespace Assimalign.Azure.WebJobs.Extensions;

/// <summary>
/// 
/// </summary>
[Binding]
[ConnectionProvider(typeof(NotificationHubAccountAttribute))]
[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
public sealed class NotificationHubBindingAttribute : Attribute, IConnectionProvider
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    public NotificationHubBindingAttribute(string path)
    {
        Path = path;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <param name="settingsName"></param>
    public NotificationHubBindingAttribute(string path, string settingsName)
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Connection { get; set; }

    /// <summary>
    /// Specifies which settings to use on which is configured on startup.
    /// </summary>
    public string SettingsName { get; set; }
}
