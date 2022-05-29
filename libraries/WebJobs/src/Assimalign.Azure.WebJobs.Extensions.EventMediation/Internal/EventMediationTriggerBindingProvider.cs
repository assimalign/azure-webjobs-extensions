using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Triggers;
using Microsoft.Extensions.Logging;

namespace Assimalign.Azure.WebJobs.Extensions;

internal sealed class EventMediationTriggerBindingProvider : ITriggerBindingProvider
{
	private readonly ILogger logger;
	private readonly IEventMediatorFactory factory;
    
	public EventMediationTriggerBindingProvider(IEventMediatorFactory factory, ILogger logger)
    {
		this.factory = factory;
		this.logger = logger;
    }

    public Task<ITriggerBinding> TryCreateAsync(TriggerBindingProviderContext context)
    {
		var result = Task.FromResult<ITriggerBinding>(default);
		var attribute = context.Parameter.GetCustomAttribute<EventMediationTriggerAttribute>(false);

		if (attribute is not null)
		{
			result = Task.FromResult<ITriggerBinding>(
				new EventMediationTriggerBinding(factory, attribute, context.Parameter, logger));
		}

		return result;
	}
}
