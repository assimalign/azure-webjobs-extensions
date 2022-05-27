
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Extensions.Logging;

namespace Assimalign.Azure.WebJobs.Extensions;

[Extension("EventMediationExtension")]
internal sealed class EventMediationTriggerExtensionConfigProvider : IExtensionConfigProvider
{
    private readonly ILogger logger;
    private readonly IEventMediatorFactory factory;

    public EventMediationTriggerExtensionConfigProvider(
        IEventMediatorFactory factory, 
        ILogger<EventMediationTriggerExtensionConfigProvider> logger)
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

        context.AddBindingRule<EventMediationTriggerAttribute>()
            .BindToTrigger<IEventContext>(new EventMediationTriggerBindingProvider(factory, logger));
    }
}
