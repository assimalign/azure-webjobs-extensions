
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Extensions.Configuration;
using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Azure.Storage.Files.Shares;
using Microsoft.Extensions.Options;

namespace Assimalign.Azure.WebJobs.Bindings.Storage
{
    [Extension("FileShareAccessorExtension")]
    internal class FileShareAccessorExtensionProvider : IExtensionConfigProvider
    {

        private readonly IConfiguration configuration;
        private readonly FileShareAccessorOptions options;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="options"></param>
        public FileShareAccessorExtensionProvider(
            IConfiguration configuration,
            IOptions<FileShareAccessorOptions> options)
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

        }


        //private void BindFileShareClient(ExtensionConfigContext context) =>
        //    context.AddBindingRule<FileShareAccessorAttribute>()
        //        .BindToInput<ShareServiceClient>(attribute =>
        //        {
        //            var memoizedClient = Cacher<string, ShareServiceClient>.Memoize(key =>
        //            {
        //                ShareServiceClient client = null;

        //                if (options.TokenCredential != null)
        //                {
        //                    var uri = attribute.Uri == null ?
        //                        new Uri($"{(configuration[options.Uri] ?? options.Uri).TrimEnd('/')}/{attribute.Queue}") :
        //                        new Uri($"{(configuration[attribute.Uri] ?? attribute.Uri).TrimEnd('/')}/{attribute.Queue}");

        //                    client = new ShareServiceClient(uri, options.ShareClientOptions);
        //                }
        //                else if (!string.IsNullOrEmpty(options.Connection))
        //                {
        //                    var connection = configuration[options.Connection] ?? options.Connection;
        //                    client = new ShareServiceClient(connection, options.ShareClientOptions);
        //                }
        //                else
        //                {
        //                    var connection = configuration[attribute.Connection] ?? attribute.Connection;
        //                    client = new ShareServiceClient(connection, options.ShareClientOptions);
        //                }

        //                return client;
        //            });

        //            return memoizedClient.Invoke($"queue-service-client-{attribute.Connection}");
        //        });


        //private void BindFileShareClient(ExtensionConfigContext context) =>
        //    context.AddBindingRule<FileShareAccessorAttribute>()
        //        .BindToInput<ShareFileClient>(attribute =>
        //        {
        //            return new ShareFileClient()
        //        });

    }
}

