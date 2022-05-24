
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.DependencyInjection;


namespace Microsoft.Azure.WebJobs
{
    using Assimalign.Azure.WebJobs.Bindings.ServiceBus;

    public static class ServiceBusAccessorWebJobsExtensions
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IWebJobsBuilder AddServiceBusBinding(this IWebJobsBuilder builder)
        {
            builder.AddExtension<ServiceBusAccessorExtensionProvider>();
            return builder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IWebJobsBuilder AddServiceBusBinding(this IWebJobsBuilder builder, Action<ServiceBusAccessorOptions> options)
        {

            builder.AddExtension<ServiceBusAccessorExtensionProvider>();
            return builder;
        }
    }
}
