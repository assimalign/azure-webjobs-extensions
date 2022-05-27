using System;
using Microsoft.Azure.WebJobs.Description;

namespace Assimalign.Azure.WebJobs.Extensions;

/// <summary>
/// An attribute to be used to bind a <see cref="IEventMediator"/> to a parameter.
/// </summary>
[Binding]
[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
public sealed class EventMediationBindingAttribute : Attribute
{
    /// <summary>
    /// Creates and binds an instance of <see cref="IEventMediator"/>.
    /// </summary>
    /// <param name="mediatorId">
    /// A case-insensitive unique identifier to be used to 
    /// identify the mediator within the <see cref="IEventMediatorFactory"/>.
    /// </param>
    public EventMediationBindingAttribute(string mediatorId)
    {
        this.MediatorId = mediatorId;
    }

    /// <summary>
    /// A case-insensitive unique identifier for a mediator.
    /// </summary>
    public string MediatorId { get; set; }
}
