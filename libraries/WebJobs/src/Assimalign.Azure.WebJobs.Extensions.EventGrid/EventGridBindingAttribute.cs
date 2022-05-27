using System;
using Azure.Messaging.EventGrid;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Description;

namespace Assimalign.Azure.WebJobs.Extensions;

/// <summary>
/// 
/// </summary>
[Binding]
[ConnectionProvider(typeof(EventGridAccountAttribute))]
[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
public sealed class EventGridBindingAttribute : Attribute, IConnectionProvider
{
    /// <summary>
    /// 
    /// </summary>
    public EventGridBindingAttribute() { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uri"></param>
    public EventGridBindingAttribute(string uri)
    {
        Uri = uri;
    }

    /// <summary>
    /// 
    /// </summary>
    public string Uri { get;}

    /// <summary>
    /// 
    /// </summary>
    public string Connection { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public EventGridPublisherClientOptions.ServiceVersion ServiceVersion { get; set; }
}
