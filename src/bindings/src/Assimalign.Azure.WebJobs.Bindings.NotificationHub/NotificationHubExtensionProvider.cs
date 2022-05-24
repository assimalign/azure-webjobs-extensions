using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Extensions.Configuration;

namespace Assimalign.Azure.WebJobs.Bindings.NotificationHub
{
    [Extension("NotificationHubExtensions")]
    public class NotificationHubExtensionProvider : IExtensionConfigProvider
    {
        private readonly IConfiguration configuration;

        public NotificationHubExtensionProvider(
            IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Initialize(ExtensionConfigContext context)
        {
            BindNotificationHubClient(context);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        private void BindNotificationHubClient(ExtensionConfigContext context) =>
            context.AddBindingRule<NotificationHubAttribute>()
                .BindToInput(builder =>
                {
                    return new NotificationHubClient(
                        configuration[builder.Connection] ?? builder.Connection, 
                        configuration[builder.Path] ?? builder.Path);
                });
    }
}
