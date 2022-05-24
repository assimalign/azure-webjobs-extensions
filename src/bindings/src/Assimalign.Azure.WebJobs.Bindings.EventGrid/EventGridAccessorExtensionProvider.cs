using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Config;
using Azure.Messaging.EventGrid;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Azure;

namespace Assimalign.Azure.WebJobs.Bindings.EventGrid
{
    
    using Assimalign.Azure.WebJobs.Bindings.EventGrid.Utilities;


    [Extension("EventGridExtensions")]
    internal class EventGridAccessorExtensionProvider : IExtensionConfigProvider
    {
        private readonly IConfiguration configuration;
        private readonly IOptions<EventGridAccessorOptions> options;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="options"></param>
        public EventGridAccessorExtensionProvider(
            IConfiguration configuration, 
            IOptions<EventGridAccessorOptions> options)
        {
            this.configuration = configuration;
            this.options = options;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Initialize(ExtensionConfigContext context)
        {
            BindEventGridPublisher(context);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        private void BindEventGridPublisher(ExtensionConfigContext context) =>
            context.AddBindingRule<EventGridAccessorAttribute>()
                .BindToInput(builder =>
                {
                    var uri = configuration[builder.Uri] ?? builder.Uri;
                    var cachedClient = Cacher<string, EventGridPublisherClient>.Memoize(key =>
                    {
                        EventGridPublisherClient client = null;
                        AzureKeyCredential credentials = null;

                        if (options.Value.TokenCredential != null)
                        {
                            client = new EventGridPublisherClient(new Uri(uri), options.Value.TokenCredential);
                        }
                        else if (!string.IsNullOrEmpty(options.Value.Connection))
                        {
                            credentials = new AzureKeyCredential(configuration[options.Value.Connection] ?? options.Value.Connection);
                            client = new EventGridPublisherClient(new Uri(uri), credentials);
                        }
                        else
                        {
                            credentials = new AzureKeyCredential(configuration[builder.Connection] ?? builder.Connection);
                            client = new EventGridPublisherClient(new Uri(uri), credentials);
                        }
                        
                        return client;
                    });

                    return cachedClient.Invoke($"{uri}");                    
                });
    }
}
