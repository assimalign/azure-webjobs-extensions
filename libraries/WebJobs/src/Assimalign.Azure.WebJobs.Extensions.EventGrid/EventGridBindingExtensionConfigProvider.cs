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
using Azure.Identity;

namespace Assimalign.Azure.WebJobs.Extensions;

using Assimalign.Azure.WebJobs.Bindings.EventGrid.Utilities;


[Extension("EventGridExtensions")]
internal sealed class EventGridBindingExtensionConfigProvider : IExtensionConfigProvider
{
    private readonly IConfiguration configuration;
    private readonly IOptions<EventGridBindingOptions> options;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="options"></param>
    public EventGridBindingExtensionConfigProvider(
        IConfiguration configuration, 
        IOptions<EventGridBindingOptions> options)
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

    public const string EventGridMsiKey = "AzureEventGrid";


    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    private void BindEventGridPublisher(ExtensionConfigContext context) =>
        context.AddBindingRule<EventGridBindingAttribute>()
            .BindToInput(attribute =>
            {
                var uri = configuration[attribute.Uri] ?? attribute.Uri;
                var cachedClient = Cacher<string, EventGridPublisherClient>.Memoize(key =>
                {
                    var client = default(EventGridPublisherClient);
                    var clientOptions = new EventGridPublisherClientOptions();
                    var clientCredentials = default(AzureKeyCredential);

                    if (!string.IsNullOrEmpty(attribute.Connection))
                    {
                        if (attribute.Connection.Equals(EventGridMsiKey, StringComparison.InvariantCultureIgnoreCase))
                        {
                            client = new EventGridPublisherClient(new Uri(uri), options.Value.TokenCredential ?? new DefaultAzureCredential());
                        }
                        else
                        {
                            clientCredentials = new AzureKeyCredential(configuration[attribute.Connection] ?? attribute.Connection);
                            client = new EventGridPublisherClient(new Uri(uri), clientCredentials);
                        }
                    }
                    else
                    {
                        if (options.Value.TokenCredential is not null)
                        {
                            client = new EventGridPublisherClient(new Uri(uri), options.Value.TokenCredential);
                        }
                        else if (!string.IsNullOrEmpty(options.Value.Connection))
                        {
                            clientCredentials = new AzureKeyCredential(configuration[options.Value.Connection] ?? options.Value.Connection);
                            client = new EventGridPublisherClient(new Uri(uri), clientCredentials);
                        }
                    }
                    return client;
                });

                return cachedClient.Invoke($"{uri}");                    
            });
}
