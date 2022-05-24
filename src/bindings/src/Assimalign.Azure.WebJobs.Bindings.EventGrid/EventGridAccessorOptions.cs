using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Identity;

namespace Assimalign.Azure.WebJobs.Bindings.EventGrid
{
    public sealed class EventGridAccessorOptions
    {

        public EventGridAccessorOptions()
        {

        }



        /// <summary>.
        /// The configuration key to the connection string or the actual connection string to use for the storage account connection.
        /// </summary>
        public string Connection { get; set; }


        /// <summary>
        /// The base uri to the Storage Account. This is can be a configuration key or the 
        /// actual uri. This is required if setting Token Credentials.
        /// </summary>
        public string Uri { get; set; }



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
