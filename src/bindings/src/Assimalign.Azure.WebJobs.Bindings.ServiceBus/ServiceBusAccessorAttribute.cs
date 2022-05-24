using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Description;

namespace Assimalign.Azure.WebJobs.Bindings.ServiceBus
{
    [Binding]
    [ConnectionProvider(typeof(ServiceBusAccountAttribute))]
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false)]
    public sealed class ServiceBusAccessorAttribute : Attribute, IConnectionProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityPath"></param>
        public ServiceBusAccessorAttribute(string entityPath)
        {
            EntityPath = entityPath;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityPath">EntityPath is the Path of the Topic Subscription or Queue.
        /// Example (Subscription): '{Topic Name}'
        /// Example (Queue): '{Queue Name}'</param>
        /// <param name="connection"> ConnectionName is the Connection String set in local.settings.json</param>
        public ServiceBusAccessorAttribute(string connection, string entityPath)
        {
            EntityPath = entityPath;
            Connection = connection;
        }

        /// <summary>
        /// ConnectionName is the Connection String set in local.settings.json
        /// </summary>
        public string Connection { get; set; }

        /// <summary>
        /// EntityPath is the Path of the Topic Subscription or Queue
        /// Example (Subscription): '{Topic Name}/Subscriptions/{Subscription Name}'
        /// Example (Queue): '{Queue Name}'
        /// </summary>
        public string EntityPath { get; set; }
    }
}
