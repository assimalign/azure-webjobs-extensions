using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.Identity;
using Azure.Messaging.EventGrid;

namespace Assimalign.Azure.WebJobs.Extensions;

public sealed class EventGridBindingOptions
{

    public EventGridBindingOptions()
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

    internal TokenCredential TokenCredential { get; set; } 

    internal TokenCredentialOptions TokenCredentialOptions { get; set; } = new DefaultAzureCredentialOptions();


    /// <summary>
    /// Configure the default Azure Credentials options to be used 
    /// when creating a <see cref="EventGridPublisherClient"/>.
    /// </summary>
    /// <param name="configure"></param>
    public void ConfigureAzureCredentials(Action<DefaultAzureCredentialOptions> configure)
    {
        var options = new DefaultAzureCredentialOptions();
        
        configure.Invoke((DefaultAzureCredentialOptions)TokenCredentialOptions);

        TokenCredential = new DefaultAzureCredential(options);
    }
}
