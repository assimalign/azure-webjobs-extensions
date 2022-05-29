using System;
using System.Linq;
using Azure.Core;
using Azure.Identity;
using Azure.Storage.Queues;

namespace Assimalign.Azure.WebJobs.Bindings.Storage
{

    public sealed class QueueAccessorOptions
    {

        /// <summary>
        /// The configuration key to the connection string or the actual 
        /// connection string to use for the storage account connection.
        /// </summary>
        public string Connection { get; set; }


        /// <summary>
        /// The base URI to the Storage Account. This is can be a configuration key or the 
        /// actual URI. This is required if setting Token Credentials.
        /// </summary>
        public string Uri { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public QueueClientOptions QueueClientOptions { get; set; } = new QueueClientOptions();


        /// <summary>
        /// The credentials to use for the connection of the storage account.
        /// </summary>
        public TokenCredential TokenCredential { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="credentials"></param>
        public void AddAzureCredentials(DefaultAzureCredential credentials) =>
            TokenCredential = credentials;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configure"></param>
        public void AddAzureCredentials(Action<DefaultAzureCredential> configure)
        {
            var credential = new DefaultAzureCredential();
            configure(credential);

            TokenCredential = credential;
        }
    }
}

