using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Description;

namespace Assimalign.Azure.WebJobs.Bindings.Storage
{
    [Binding]
    [ConnectionProvider(typeof(StorageAccountAttribute))]
    [AttributeUsage(AttributeTargets.ReturnValue | AttributeTargets.Parameter)]
    public sealed class QueueAccessorAttribute : Attribute, IConnectionProvider
    {
        /// <summary>
        /// Constructor for accessing service clients
        /// </summary>
        public QueueAccessorAttribute() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queue"></param>

        public QueueAccessorAttribute(string queue)
        {
            Queue = queue;
        }


        /// <summary>
        /// 
        /// </summary>
        public string Queue { get; set; }


        /// <summary>
        /// The configuration name or the connection string to the storage account.
        /// </summary>
        public string Connection { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Uri { get; set; }

    }
}
