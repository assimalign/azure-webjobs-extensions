using System;
using Microsoft.Azure.WebJobs.Description;

namespace Assimalign.Azure.WebJobs.Extensions;

/// <summary>
/// 
/// </summary>
[Binding]
[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
public sealed class EventMediationTriggerAttribute : Attribute
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediatorId"></param>
    /// <param name="eventId"></param>
    public EventMediationTriggerAttribute(string mediatorId, string eventId)
    {
        this.MediatorId = mediatorId;
        this.EventId = eventId;
    }

    /// <summary>
    /// 
    /// </summary>
    public string MediatorId { get; }
    /// <summary>
    /// The Event ID in which to subscribe to .
    /// </summary>
    public string EventId { get; }
}