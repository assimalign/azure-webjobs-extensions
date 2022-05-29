
using System;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Azure.Data.Tables;

namespace Assimalign.Azure.WebJobs.Bindings.Storage
{
    using Assimalign.Azure.WebJobs.Bindings.Storage.Utilities;


    [Extension("TableAccessorExtension")]
    internal class TableAccessorExtensionProvider : IExtensionConfigProvider
    {
        private readonly IConfiguration configuration;
        private readonly TableAccessorOptions options;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="options"></param>
        public TableAccessorExtensionProvider(
            IConfiguration configuration,
            IOptions<TableAccessorOptions> options)
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
            BindCloudTableClient(context);
            BindCloudTable(context);
        }


        /// <summary>
        /// Binds a Cloud Table Client to the Table Accessor Attribute
        /// </summary>
        /// <param name="context"></param>
        private void BindCloudTableClient(ExtensionConfigContext context) =>
            context.AddBindingRule<TableAccessorAttribute>()
                .BindToInput<TableServiceClient>(attribute =>
                {
                    var memoizedClient = Cacher<string, TableServiceClient>.Memoize(key =>
                    {
                        TableServiceClient client = null;

                        if (options.TokenCredential != null)
                        {
                            var uri = attribute.Uri == null ?
                                new Uri($"{(configuration[options.Uri] ?? options.Uri).TrimEnd('/')}") :
                                new Uri($"{(configuration[attribute.Uri] ?? attribute.Uri).TrimEnd('/')}");

                            client = new TableServiceClient(uri, options.TokenCredential, options.TableClientOptions);
                        }
                        else if (!string.IsNullOrEmpty(options.Connection))
                        {
                            var connection = configuration[options.Connection] ?? options.Connection;
                            client = new TableServiceClient(connection, options.TableClientOptions);
                        }
                        else
                        {
                            var connection = configuration[attribute.Connection] ?? attribute.Connection;
                            client = new TableServiceClient(connection, options.TableClientOptions);
                        }

                        return client;
                    });

                    var client = memoizedClient.Invoke($"table-service-client-{attribute.Table}".ToLower());
                    return client;

                });


        /// <summary>
        /// Binds a Cloud Table to the Table Accessor Attribute
        /// </summary>
        /// <param name="context"></param>
        private void BindCloudTable(ExtensionConfigContext context) =>
             context.AddBindingRule<TableAccessorAttribute>()
                 .BindToInput<TableClient>(attribute =>
                 {
                     var memoizedClient = Cacher<string, TableClient>.Memoize(key =>
                     {
                         TableClient client = null;

                         if (options.TokenCredential != null)
                         {
                             var uri = attribute.Uri == null ?
                                 new Uri($"{(configuration[options.Uri] ?? options.Uri).TrimEnd('/')}") :
                                 new Uri($"{(configuration[attribute.Uri] ?? attribute.Uri).TrimEnd('/')}");

                             client = new TableClient(uri, attribute.Table, options.TokenCredential, options.TableClientOptions);
                         }
                         else if (!string.IsNullOrEmpty(options.Connection))
                         {
                             var connection = configuration[options.Connection] ?? options.Connection;
                             client = new TableClient(connection, attribute.Table, options.TableClientOptions);
                         }
                         else
                         {
                             var connection = configuration[attribute.Connection] ?? attribute.Connection;
                             client = new TableClient(connection, attribute.Table, options.TableClientOptions);
                         }

                         return client;
                     });

                     var client = memoizedClient.Invoke($"table-client-{attribute.Table}");
                     return client;
                 });

    }
}

