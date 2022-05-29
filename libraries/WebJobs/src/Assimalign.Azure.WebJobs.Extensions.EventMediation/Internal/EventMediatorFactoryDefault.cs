using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace Assimalign.Azure.WebJobs.Extensions;

internal sealed class EventMediatorFactoryDefault : IEventMediatorFactory
{
    private readonly ILoggerFactory factory;
    private readonly ConcurrentDictionary<string, IEventMediator> mediators;

    public EventMediatorFactoryDefault(ILoggerFactory factory)
    {
        this.factory = factory;
        this.mediators = new();
    }

    public IEventMediator CreateMediator(string mediatorId)
    {
        return mediators.GetOrAdd(mediatorId, id =>
        {
            return new EventMediatorDefault(factory.CreateLogger<EventMediatorDefault>())
            {
                MediatorId = id
            };
        });
    }
}
