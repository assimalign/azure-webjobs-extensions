
using System;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Azure.Storage.Blobs;

namespace Assimalign.Azure.WebJobs.Bindings.Storage
{
    using Assimalign.Azure.WebJobs.Bindings.Storage.Utilities;


    [Extension("BlobAccessorExtension")]
    internal class BlobAccessorExtensionProvider : IExtensionConfigProvider
    {
        private readonly IConfiguration configuration;
        private readonly BlobAccessorOptions options;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="options"></param>
        public BlobAccessorExtensionProvider(
            IConfiguration configuration,
            IOptions<BlobAccessorOptions> options)
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
            BindBlobServiceClient(context);
            BindBlobContainerClient(context);
            BindBlobClient(context);
            BindBlobStringContent(context);
            BindBlobByteArrayContent(context);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        private void BindBlobServiceClient(ExtensionConfigContext context) =>
            context.AddBindingRule<BlobAccessorAttribute>()
                .BindToInput<BlobServiceClient>(attribute =>
                {
                    var memoizedClient = Cacher<string, BlobServiceClient>.Memoize(key =>
                    {
                        BlobServiceClient client = null;

                        if (options.TokenCredential != null)
                        {
                            var uri = attribute.Uri == null ?
                                new Uri($"{(configuration[options.Uri] ?? options.Uri).TrimEnd('/')}") :
                                new Uri($"{(configuration[attribute.Uri] ?? attribute.Uri).TrimEnd('/')}");

                            client = new BlobServiceClient(uri, options.TokenCredential, options.BlobClientOptions);
                        }
                        else if (!string.IsNullOrEmpty(options.Connection))
                        {
                            var connection = configuration[options.Connection] ?? options.Connection;
                            client = new BlobServiceClient(connection, options.BlobClientOptions);
                        }
                        else
                        {
                            var connection = configuration[attribute.Connection] ?? attribute.Connection;
                            client = new BlobServiceClient(connection, options.BlobClientOptions);
                        }

                        return client;
                    });

                    var client = memoizedClient.Invoke($"blob-service-client-{attribute.Container}".ToLower());
                    return client;
                });

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        private void BindBlobContainerClient(ExtensionConfigContext context) =>
            context.AddBindingRule<BlobAccessorAttribute>()
            .BindToInput<BlobContainerClient>(attribute =>
            {

                var memoizedClient = Cacher<string, BlobContainerClient>.Memoize(key =>
                {
                    BlobContainerClient client = null;

                    // Check for Token Credential to see if MSI needs to be used
                    if (options.TokenCredential != null)
                    {
                        var uri = attribute.Uri == null ?
                            new Uri($"{(configuration[options.Uri] ?? options.Uri).TrimEnd('/')}/{attribute.Container}") :
                            new Uri($"{(configuration[attribute.Uri] ?? attribute.Uri).TrimEnd('/')}/{attribute.Container}");

                        client = new BlobContainerClient(uri, options.TokenCredential, options.BlobClientOptions);
                    }
                    // Check for a Connection string other than the default storage account the Web Jobs Service Run on
                    else if (!string.IsNullOrEmpty(options.Connection))
                    {
                        var connection = configuration[options.Connection] ?? options.Connection;
                        client = new BlobContainerClient(connection, attribute.Container, options.BlobClientOptions);
                    }
                    else
                    {
                        var connection = configuration[attribute.Connection] ?? attribute.Connection;
                        client = new BlobContainerClient(connection, attribute.Container, options.BlobClientOptions);
                    }

                    // Create Container if not exist flag is added
                    if (attribute.EnsureContainerExist)
                        client.CreateIfNotExists();

                    return client;
                });

                var client = memoizedClient.Invoke($"blob-container-client-{attribute.Container}".ToLower());
                return client;
            });

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        private void BindBlobClient(ExtensionConfigContext context) =>
            context.AddBindingRule<BlobAccessorAttribute>()
            .BindToInput<BlobClient>(attribute =>
            {
                var memoizedClient = Cacher<string, BlobClient>.Memoize(key =>
                {
                    BlobClient client = null;
                    if (options.TokenCredential != null)
                    {
                        var uri = attribute.Uri == null ?
                            new Uri($"{(configuration[options.Uri] ?? options.Uri).TrimEnd('/')}/{attribute.Container}/{attribute.Blob}") :
                            new Uri($"{(configuration[attribute.Uri] ?? attribute.Uri).TrimEnd('/')}/{attribute.Container}/{attribute.Blob}");

                        client = new BlobClient(uri, options.TokenCredential, options.BlobClientOptions);
                    }
                    else if (!string.IsNullOrEmpty(options.Connection))
                    {
                        var connection = configuration[options.Connection] ?? options.Connection;
                        client = new BlobClient(connection, attribute.Container, attribute.Blob, options.BlobClientOptions);
                    }
                    else
                    {
                        var connection = configuration[attribute.Connection] ?? attribute.Connection;
                        client = new BlobClient(connection, attribute.Container, attribute.Blob, options.BlobClientOptions);
                    }

                    return client;
                });

                var client = memoizedClient.Invoke($"blob-client-{attribute.Container}-{attribute.Blob}");
                return client;
            });


        /// <summary>
        /// Binds the content of a blob as a string value.
        /// </summary>
        /// <param name="context"></param>
        private void BindBlobStringContent(ExtensionConfigContext context) =>
            context.AddBindingRule<BlobAccessorAttribute>()
                .BindToInput<string>(attribute =>
                {
                    var memoizedClient = Cacher<string, string>.Memoize(key =>
                    {
                        BlobClient client = null;
                        if (options.TokenCredential != null)
                        {
                            var uri = new Uri($"{configuration[options.Uri] ?? options.Uri.TrimEnd('/')}/{attribute.Container}/{attribute.Blob}");
                            client = new BlobClient(uri, options.TokenCredential, options.BlobClientOptions);
                        }
                        else if (!string.IsNullOrEmpty(options.Connection))
                        {
                            var connection = configuration[options.Connection] ?? options.Connection;
                            client = new BlobClient(connection, attribute.Container, attribute.Blob, options.BlobClientOptions);
                        }
                        else
                        {
                            var connection = configuration[attribute.Connection] ?? attribute.Connection;
                            client = new BlobClient(connection, attribute.Container, attribute.Blob, options.BlobClientOptions);
                        }

                        return client.DownloadContent()
                        .GetRawResponse().Content
                        .ToString();
                    });

                    var client = memoizedClient.Invoke($"blob-client-string-content-{attribute.Container}-{attribute.Blob}".ToLower());
                    return client;
                });


        /// <summary>
        /// Binds the content of a blob as a byte array value.
        /// </summary>
        /// <param name="context"></param>
        private void BindBlobByteArrayContent(ExtensionConfigContext context) =>
            context.AddBindingRule<BlobAccessorAttribute>()
                .BindToInput<byte[]>(attribute =>
                {
                    var memoizedClient = Cacher<string, byte[]>.Memoize(key =>
                    {
                        BlobClient client = null;
                        if (options.TokenCredential != null)
                        {
                            var uri = new Uri($"{configuration[options.Uri] ?? options.Uri.TrimEnd('/')}/{attribute.Container}/{attribute.Blob}");
                            client = new BlobClient(uri, options.TokenCredential, options.BlobClientOptions);
                        }
                        else if (!string.IsNullOrEmpty(options.Connection))
                        {
                            var connection = configuration[options.Connection] ?? options.Connection;
                            client = new BlobClient(connection, attribute.Container, attribute.Blob, options.BlobClientOptions);
                        }
                        else
                        {
                            var connection = configuration[attribute.Connection] ?? attribute.Connection;
                            client = new BlobClient(connection, attribute.Container, attribute.Blob, options.BlobClientOptions);
                        }

                        return client.DownloadContent()
                        .GetRawResponse().Content
                        .ToArray();
                    });

                    var client = memoizedClient.Invoke($"blob-client-byte-content-{attribute.Container}-{attribute.Blob}".ToLower());
                    return client;
                });

    }
}
