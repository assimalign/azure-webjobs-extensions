using System;
using System.IO;
using System.Linq;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;


namespace Assimalign.Azure.WebJobs.Bindings.ServiceBus
{

    using Assimalign.Azure.WebJobs.Bindings.ServiceBus.Utilities;


    [Extension("ServiceBusExtensions")]
    internal sealed class ServiceBusAccessorExtensionProvider : IExtensionConfigProvider
    {
        private readonly IConfiguration configuration;
        private readonly ServiceBusAccessorOptions options;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="messageProvider"></param>
        public ServiceBusAccessorExtensionProvider(
            IConfiguration configuration,
            IOptions<ServiceBusAccessorOptions> options)
        {
            this.configuration = configuration;
            this.options = options.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Initialize(ExtensionConfigContext context)
        {
            BindServiceBusClient(context);
            BindServiceBusSender(context);
            BindServiceBusReceiver(context);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        private void BindServiceBusClient(ExtensionConfigContext context) =>
            context.AddBindingRule<ServiceBusAccessorAttribute>()
                .BindToInput<ServiceBusClient>(attribute =>
                {
                    var serviceBusClient = Cacher<string, ServiceBusClient>.Memoize(entity =>
                    {
                        if (options.TokenCredential != null)
                        {
                            var serviceBusNamespace = attribute.Namespace == null ?
                                configuration[options.Namespace] ?? options.Namespace :
                                configuration[attribute.Namespace] ?? attribute.Namespace;

                            return new ServiceBusClient(serviceBusNamespace, options.TokenCredential, options.ClientOptions);
                        }
                        else
                        {
                            return new ServiceBusClient(configuration[attribute.Connection] ?? attribute.Connection, options.ClientOptions);
                        }
                    });

                    return serviceBusClient.Invoke("service-bus-client");
                });


        /// <summary>
        /// Binds a Service Bus Sender to be able to send Queue & Topic Messages.
        /// </summary>
        /// <param name="context"></param>
        private void BindServiceBusSender(ExtensionConfigContext context) =>
            context.AddBindingRule<ServiceBusAccessorAttribute>()
                .BindToInput<ServiceBusSender>(attribute =>
                {
                    var serviceBusSender = Cacher<string, ServiceBusSender>.Memoize(entity =>
                    {
                        ServiceBusClient serviceBusClient = null;

                        if (options.TokenCredential != null)
                        {
                            var serviceBusNamespace = attribute.Namespace == null ?
                                configuration[options.Namespace] ?? options.Namespace :
                                configuration[attribute.Namespace] ?? attribute.Namespace;

                            serviceBusClient = new ServiceBusClient(serviceBusNamespace, options.TokenCredential, options.ClientOptions);
                        }
                        else
                        {
                            serviceBusClient = new ServiceBusClient(configuration[attribute.Connection] ?? attribute.Connection, options.ClientOptions);
                        }

                        return serviceBusClient.CreateSender(entity);
                    });

                    return serviceBusSender.Invoke(configuration[attribute.EntityPath] ?? attribute.EntityPath);
                });

        /// <summary>
        /// Binds a Service Bus Receiver to be able to receive Queue & Topic Messages.
        /// </summary>
        /// <param name="context"></param>
        private void BindServiceBusReceiver(ExtensionConfigContext context) =>
            context.AddBindingRule<ServiceBusAccessorAttribute>()
                .BindToInput<ServiceBusReceiver>(attribute =>
                {
                    var serviceBusReceiver = Cacher<string, ServiceBusReceiver>.Memoize(entity =>
                    {
                        ServiceBusClient serviceBusClient = null;

                        if (options.TokenCredential != null)
                        {
                            var serviceBusNamespace = attribute.Namespace == null ?
                                configuration[options.Namespace] ?? options.Namespace :
                                configuration[attribute.Namespace] ?? attribute.Namespace;

                            serviceBusClient = new ServiceBusClient(serviceBusNamespace, options.TokenCredential, options.ClientOptions);
                        }
                        else
                        {
                            serviceBusClient = new ServiceBusClient(configuration[attribute.Connection] ?? attribute.Connection, options.ClientOptions);
                        }

                        return serviceBusClient.CreateReceiver(entity);
                    });

                    return serviceBusReceiver.Invoke(configuration[attribute.EntityPath] ?? attribute.EntityPath);
                });

    }
}