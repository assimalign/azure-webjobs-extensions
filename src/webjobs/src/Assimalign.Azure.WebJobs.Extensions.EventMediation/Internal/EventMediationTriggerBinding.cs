using System;
using System.Reflection;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Listeners;
using Microsoft.Azure.WebJobs.Host.Protocols;
using Microsoft.Azure.WebJobs.Host.Triggers;
using Microsoft.Extensions.Logging;

namespace Assimalign.Azure.WebJobs.Extensions;

internal sealed class EventMediationTriggerBinding : ITriggerBinding
{
    private readonly ILogger logger;
    private readonly ParameterInfo parameter;
    private readonly IEventMediatorFactory factory;
    private readonly EventMediationTriggerAttribute attribute;

    public EventMediationTriggerBinding(
        IEventMediatorFactory factory, 
        EventMediationTriggerAttribute attribute, 
        ParameterInfo parameter, 
        ILogger logger)
    {
        this.parameter = parameter;
        this.factory = factory;
        this.attribute = attribute;
        this.logger = logger;
    }

    public Type TriggerValueType => typeof(IEventContext);

    public IReadOnlyDictionary<string, Type> BindingDataContract => new Dictionary<string, Type>();
    
    public Task<ITriggerData> BindAsync(object value, ValueBindingContext context)
    {
        return Task.FromResult<ITriggerData>(new TriggerData(null, new Dictionary<string, object>()));
    }

    public Task<IListener> CreateListenerAsync(ListenerFactoryContext context)
    {
        var mediator = factory.CreateMediator(attribute.MediatorId);
        var listener = new EventMediationTriggerListener(context.Executor, context.Descriptor, parameter, logger)
        {
            EventId = attribute.EventId
        };

        mediator.Attach(listener);

        return Task.FromResult<IListener>(listener);
    }

    public ParameterDescriptor ToParameterDescriptor()
    {
        return default;
    }
}
