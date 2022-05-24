using System;

namespace Microsoft.Azure.WebJobs
{
    using Assimalign.Azure.WebJobs.Bindings.EventGrid;

    /// <summary>
    /// 
    /// </summary>
    public static class EventGridAccessorWebJobsExtensions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IWebJobsBuilder AddEventGridAccessorBinding(this IWebJobsBuilder builder)
        {
            builder.AddExtension<EventGridAccessorExtensionProvider>();
            return builder;
        }
    }
}
