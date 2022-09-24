
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Assimalign.Azure.WebJobs.Extensions;

internal class EventMediatorDefault : IEventMediator
{
    private int notified;
    private readonly ILogger logger;
    public EventMediatorDefault(ILogger<EventMediatorDefault> logger)
    {
        this.notified = 0;
        this.logger = logger;   
    }

    public IList<IEventHandler> Handlers { get; set; } = new List<IEventHandler>();

    public string MediatorId { get; init; }

    public void Attach(IEventHandler handler)
    {
        Handlers.Add(handler);
    }

    public void Notify(string eventId, IEventContext context)
    {
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        foreach (var handler in Handlers)
        {
            if (handler.EventId.Equals(eventId, StringComparison.InvariantCultureIgnoreCase))
            {
                handler.Invoke(context);
                notified++;
            }
        }

        if (notified == 0)
        {
            logger.LogWarning(
                "No 'EventMediationTrigger' were invoked for Event ID: '{0}' under Mediation ID: '{1}'", eventId, MediatorId);
        }

        notified = 0;
    }
}


internal sealed class EventMediatorDefault<TEventId> : EventMediatorDefault, IEventMediator<TEventId>
    where TEventId : struct
{
    public EventMediatorDefault(ILogger<EventMediatorDefault<TEventId>> logger) : base(logger) {  }

    public void Notify(TEventId eventId, IEventContext context)
    {
       base.Notify(eventId.ToString(), context);
    }
}