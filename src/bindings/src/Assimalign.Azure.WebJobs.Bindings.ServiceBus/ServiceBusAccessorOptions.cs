using System;
using System.Text;
using Azure.Core;
using Azure.Identity;

namespace Assimalign.Azure.WebJobs.Bindings.ServiceBus
{
    public sealed class ServiceBusAccessorOptions
    {



        /// <summary>
        /// The crednetials to use to access the storage account blob
        /// </summary>
        internal TokenCredential TokenCredential { get; set; } = null;


        /// <summary>
        /// 
        /// </summary>
        internal TokenCredentialOptions TokenCredentialOptions { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="configure"></param>
        public void ConfigureCredentials(Action<DefaultAzureCredentialOptions> configure)
        {
            var options = new DefaultAzureCredentialOptions();
            configure(options);

            TokenCredentialOptions = options;
            TokenCredential = new DefaultAzureCredential(options);
        }
    }
}
