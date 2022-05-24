using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Azure.WebJobs
{
    using Assimalign.Azure.WebJobs.Bindings.NotificationHub;


    public static class NotificationHubWebJobsExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IWebJobsBuilder AddNotificationBinding(this IWebJobsBuilder builder)
        {
            builder.AddExtension<NotificationHubExtensionProvider>();
            return builder;
        }
    }
}
