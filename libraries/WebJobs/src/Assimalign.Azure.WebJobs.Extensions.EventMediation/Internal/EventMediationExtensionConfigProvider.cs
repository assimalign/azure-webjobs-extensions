using System;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Extensions.Logging;

namespace Assimalign.Azure.WebJobs.Extensions;

[Extension("EventMediationExtensions")]
internal sealed class EventMediationExtensionConfigProvider : IExtensionConfigProvider
{
    private readonly ILogger logger;
    private readonly IEventMediatorFactory factory;

    public EventMediationExtensionConfigProvider(
        IEventMediatorFactory factory,
        ILogger<EventMediationExtensionConfigProvider> logger)
    {
        this.factory = factory;
        this.logger = logger;
    }

    public void Initialize(ExtensionConfigContext context)
    {
        context.AddBindingRule<EventMediationBindingAttribute>()
            .BindToInput(attribute =>
            {
                if (string.IsNullOrEmpty(attribute.MediatorId))
                {
                    throw new ArgumentNullException(nameof(attribute.MediatorId));
                }

                return factory.CreateMediator(attribute.MediatorId);
            });

        var bindingRule = context.AddBindingRule<EventMediationTriggerAttribute>();

        bindingRule.BindToTrigger<IEventContext>(new EventMediationTriggerBindingProvider(factory, logger));
    }
}
