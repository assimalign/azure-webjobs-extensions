using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Azure.WebJobs
{
    using Assimalign.Azure.WebJobs.Extensions;

    /// <summary>
    /// 
    /// </summary>
    public static class EventGridBindingWebJobsExtensions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IWebJobsBuilder AddEventGridBinding(this IWebJobsBuilder builder)
        {
            return builder.AddEventGridBinding(configure => { });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IWebJobsBuilder AddEventGridBinding(this IWebJobsBuilder builder, Action<EventGridBindingOptions> configure)
        {
            builder.Services.AddOptions<EventGridBindingOptions>()
                .Configure(configure);

            builder.AddExtension<EventGridBindingExtensionConfigProvider>();
            return builder;
        }
    }
}
