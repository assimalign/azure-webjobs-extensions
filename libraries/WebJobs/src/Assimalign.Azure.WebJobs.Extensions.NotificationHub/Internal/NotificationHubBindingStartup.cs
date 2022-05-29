using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;

[assembly: WebJobsStartup(typeof(Assimalign.Azure.WebJobs.Extensions.NotificationHubBindingStartup))]
namespace Assimalign.Azure.WebJobs.Extensions;

internal sealed class NotificationHubBindingStartup : IWebJobsStartup
{
    public void Configure(IWebJobsBuilder builder)
    {
        builder.AddNotificationBinding(configure => { });
    }
}
