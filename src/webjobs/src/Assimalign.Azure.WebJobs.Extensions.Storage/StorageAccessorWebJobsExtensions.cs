using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Azure.WebJobs
{
    using Assimalign.Azure.WebJobs.Bindings.Storage;


    public static class StorageAccessorWebJobsExtensions
    {
        #region blob.bindings

        /// <summary>
        /// Adds Blob Binding Extensions for Azure Functions with custom configuration.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IWebJobsBuilder AddBlobAccessorBinding(this IWebJobsBuilder builder, Action<BlobAccessorOptions> configure = null)
        {
            if (configure == null)
            {
                builder.Services.AddOptions<BlobAccessorOptions>();
            }
            else
            {
                builder.Services.AddOptions<BlobAccessorOptions>()
                    .Configure(configure);
            }

            builder.AddExtension<BlobAccessorExtensionProvider>();
            return builder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IWebJobsBuilder AddBlobAccessorBinding(this IWebJobsBuilder builder, Action<BlobAccessorOptions, IServiceProvider> configure)
        {
            builder.Services.AddOptions<BlobAccessorOptions>()
                .Configure(configure);

            builder.AddExtension<BlobAccessorExtensionProvider>();
            return builder;
        }
        #endregion

        #region queue.bindings

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IWebJobsBuilder AddQueueAccessorBinding(this IWebJobsBuilder builder, Action<QueueAccessorOptions> configure = null)
        {
            if (configure == null)
            {
                builder.Services.AddOptions<QueueAccessorOptions>();
            }
            else
            {
                builder.Services.AddOptions<QueueAccessorOptions>()
                    .Configure(configure);
            }

            builder.AddExtension<QueueAccessorExtensionProvider>();
            return builder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IWebJobsBuilder AddQueueAccessorBinding(this IWebJobsBuilder builder, Action<QueueAccessorOptions, IServiceProvider> configure)
        {
            builder.Services.AddOptions<QueueAccessorOptions>()
                .Configure(configure);

            builder.AddExtension<QueueAccessorExtensionProvider>();
            return builder;
        }

        #endregion

        #region fileshare.bindings

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IWebJobsBuilder AddFileShareAccessorBinding(this IWebJobsBuilder builder, Action<FileShareAccessorOptions> configure = null)
        {
            if (configure == null)
            {
                builder.Services.AddOptions<FileShareAccessorOptions>();
            }
            else
            {
                builder.Services.AddOptions<FileShareAccessorOptions>()
                    .Configure(configure);
            }

            builder.AddExtension<FileShareAccessorExtensionProvider>();
            return builder;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IWebJobsBuilder AddFileShareAccessorBinding(this IWebJobsBuilder builder, Action<FileShareAccessorOptions, IServiceProvider> configure)
        {
            builder.Services.AddOptions<FileShareAccessorOptions>()
                .Configure(configure);

            builder.AddExtension<FileShareAccessorExtensionProvider>();
            return builder;
        }

        #endregion

        #region table.bindings

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IWebJobsBuilder AddTableAccessorBinding(this IWebJobsBuilder builder, Action<TableAccessorOptions> configure = null)
        {
            if (configure == null)
            {
                builder.Services.AddOptions<TableAccessorOptions>();
            }
            else
            {
                builder.Services.AddOptions<TableAccessorOptions>()
                    .Configure(configure);
            }

            builder.AddExtension<TableAccessorExtensionProvider>();
            return builder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IWebJobsBuilder AddTableAccessorBinding(this IWebJobsBuilder builder, Action<TableAccessorOptions, IServiceProvider> configure)
        {

            builder.Services.AddOptions<TableAccessorOptions>()
                .Configure(configure);

            builder.AddExtension<TableAccessorExtensionProvider>();
            return builder;
        }


        #endregion
    }
}

