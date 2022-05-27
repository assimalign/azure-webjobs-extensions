using System;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;

[assembly: WebJobsStartup(typeof(Assimalign.Azure.WebJobs.Extensions.EventMediationStartup))]
namespace Assimalign.Azure.WebJobs.Extensions;

internal sealed class EventMediationStartup : IWebJobsStartup
{
    public void Configure(IWebJobsBuilder builder)
    {
        //Assembly.GetExecutingAssembly().GetTypes()
        //    .SelectMany(x=>x.)

        builder.Services.AddSingleton<IEventMediatorFactory, EventMediatorFactoryDefault>();
        builder.AddExtension<EventMediationTriggerExtensionConfigProvider>();
    }
}
