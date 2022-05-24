using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Description;

namespace Assimalign.Azure.WebJobs.Bindings.EventGrid
{
    using Common;
    /// <summary>
    /// 
    /// </summary>
    [Binding]
    [ConnectionProvider(typeof(EventGridAccountAttribute))]
    [AttributeUsage(AttributeTargets.ReturnValue | AttributeTargets.Parameter)]
    public class EventGridAccessorAttribute : Attribute, IConnectionProvider
    {
        /// <summary>
        /// 
        /// </summary>
        public EventGridAccessorAttribute() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        public EventGridAccessorAttribute(string uri)
        {
            Uri = uri;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Connection { get; set; }
    }
}
