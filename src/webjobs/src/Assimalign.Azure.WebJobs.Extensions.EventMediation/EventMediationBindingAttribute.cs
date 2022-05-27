using System;
using Microsoft.Azure.WebJobs.Description;

namespace Assimalign.Azure.WebJobs.Extensions;


/// <summary>
/// 
/// </summary>
[Binding]
[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
public sealed class EventMediationBindingAttribute : Attribute
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediatorId"></param>
    public EventMediationBindingAttribute(string mediatorId)
    {
        this.MediatorId = mediatorId;
    }

    /// <summary>
    /// 
    /// </summary>
    public string MediatorId { get; set; }
}
