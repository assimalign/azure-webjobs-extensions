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
    public string Path { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Connection { get; set; }
}
