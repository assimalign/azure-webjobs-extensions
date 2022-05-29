using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;


[assembly: WebJobsStartup(typeof(Assimalign.Azure.WebJobs.Extensions.Startup))]
namespace Assimalign.Azure.WebJobs.Extensions;

using Assimalign.Azure.WebJobs.Extensions.Functions;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Azure.WebJobs.Host.Executors;
using Microsoft.Azure.WebJobs.Host.Listeners;
using Microsoft.Azure.WebJobs.Host.Protocols;
using Microsoft.Azure.WebJobs.Host.Triggers;
using System.Threading;

public class Startup : IWebJobsStartup
{
    public void Configure(IWebJobsBuilder builder)
    {
        builder.AddValidatorBinding(factory =>
        {
            factory.AddValidator("sample-validator-1", configure =>
            {
                configure.AddProfile(new ValidationBindingProfile());
            });
        });
        builder.AddMapperBinding(builder =>
        {
            builder.AddMapper("sample-mapper-1", mapper =>
            {
                mapper.CreateProfile<MapperTestObject1, MapperTestObject2>(descriptor =>
                {
                    descriptor.MapMember(target => target.FirstName, source => source.Name);
                });
            });
        });

        builder.AddExtension<ConfigProviderTest>();

        builder.AddEventGridBinding();
    }

    
}


public class Test
{

}

public class ConfigProviderTest : IExtensionConfigProvider
{
    public void Initialize(ExtensionConfigContext context)
    {
        context.AddBindingRule<HttpTriggerAttribute>()
            .BindToTrigger(new ConfigBindingProvider());
    }
}

public class ConfigBindingProvider : ITriggerBindingProvider
{
    public Task<ITriggerBinding> TryCreateAsync(TriggerBindingProviderContext context)
    {
        return Task.FromResult<ITriggerBinding>(new ConfigTestBinding());
    }
}

public class ConfigTestBinding : ITriggerBinding
{
    public Type TriggerValueType => typeof(Test);

    public IReadOnlyDictionary<string, Type> BindingDataContract => new Dictionary<string, Type>();

    public Task<ITriggerData> BindAsync(object value, ValueBindingContext context)
    {
        return Task.FromResult<ITriggerData>(new TriggerData(null, new Dictionary<string, object>()));
    }

    public Task<IListener> CreateListenerAsync(ListenerFactoryContext context)
    {

        return Task.FromResult<IListener>(new NListener(context.Executor));
    }

    public ParameterDescriptor ToParameterDescriptor()
    {
        return default;
    }


    public class NListener : IListener
    {

        public NListener(ITriggeredFunctionExecutor executer)
        {

        }
        public void Cancel()
        {
           
        }

        public void Dispose()
        {
            
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}