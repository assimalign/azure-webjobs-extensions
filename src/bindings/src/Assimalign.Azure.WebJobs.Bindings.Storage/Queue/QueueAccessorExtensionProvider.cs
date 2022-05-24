
using System;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Config;
using Azure.Storage.Queues;


namespace Assimalign.Azure.WebJobs.Bindings.Storage
{
    using Assimalign.Azure.WebJobs.Bindings.Storage.Utilities;

    [Extension("QueueAccessorExtension")]
    internal class QueueAccessorExtensionProvider : IExtensionConfigProvider
    {

        private readonly IConfiguration configuration;
        private readonly QueueAccessorOptions options;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="options"></param>
        public QueueAccessorExtensionProvider(
            IConfiguration configuration,
            IOptions<QueueAccessorOptions> options)
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
            BindQueueServiceClient(context);
            BindQueueClient(context);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        private void BindQueueServiceClient(ExtensionConfigContext context) =>
            context.AddBindingRule<QueueAccessorAttribute>()
                .BindToInput<QueueServiceClient>(attribute =>
                {
                    var memoizedClient = Cacher<string, QueueServiceClient>.Memoize(key =>
                    {
                        QueueServiceClient client = null;

                        if (options.TokenCredential != null)
                        {
                            var uri = attribute.Uri == null ?
                                new Uri($"{(configuration[options.Uri] ?? options.Uri).TrimEnd('/')}") :
                                new Uri($"{(configuration[attribute.Uri] ?? attribute.Uri).TrimEnd('/')}");

                            client = new QueueServiceClient(uri, options.TokenCredential, options.QueueClientOptions);
                        }
                        else if (!string.IsNullOrEmpty(options.Connection))
                        {
                            var connection = configuration[options.Connection] ?? options.Connection;
                            client = new QueueServiceClient(connection, options.QueueClientOptions);
                        }
                        else
                        {
                            var connection = configuration[attribute.Connection] ?? attribute.Connection;
                            client = new QueueServiceClient(connection, options.QueueClientOptions);
                        }

                        return client;
                    });

                    return memoizedClient.Invoke($"queue-service-client-{attribute.Connection}");
                });


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        private void BindQueueClient(ExtensionConfigContext context) =>
            context.AddBindingRule<QueueAccessorAttribute>()
                .BindToInput(attribute =>
                {
                    var memoizedClient = Cacher<string, QueueClient>.Memoize(key =>
                    {
                        QueueClient client = null;

                        if (options.TokenCredential != null)
                        {
                            var uri = attribute.Uri == null ?
                                new Uri($"{(configuration[options.Uri] ?? options.Uri).TrimEnd('/')}/{attribute.Queue}") :
                                new Uri($"{(configuration[attribute.Uri] ?? attribute.Uri).TrimEnd('/')}/{attribute.Queue}");

                            client = new QueueClient(uri, options.TokenCredential, options.QueueClientOptions);
                        }
                        else if (!string.IsNullOrEmpty(options.Connection))
                        {

                            var connection = configuration[options.Connection] ?? options.Connection;
                            client = new QueueClient(connection, attribute.Queue, options.QueueClientOptions);
                        }
                        else
                        {
                            var connection = configuration[attribute.Connection] ?? attribute.Connection;
                            client = new QueueClient(connection, attribute.Queue, options.QueueClientOptions);
                        }

                        return client;
                    });

                    return memoizedClient.Invoke($"queue-client-{attribute.Connection}-{attribute.Queue}");
                });
    }
}

