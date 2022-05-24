
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace Assimalign.Azure.WebJobs.Bindings.NotificationHub
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Parameter)]
    public sealed class NotificationHubAccountAttribute : Attribute, IConnectionProvider
    {
        //
        // Summary:
        //     Constructs a new instance.
        //
        // Parameters:
        //   account:
        //     A string value indicating the Azure Storage connection string to use. This string
        //     should be in one of the following formats. These checks will be applied in order
        //     and the first match wins. - The name of an "AzureWebJobs" prefixed app setting
        //     or connection string name. E.g., if your setting name is "AzureWebJobsMyStorage",
        //     you can specify "MyStorage" here. - Can be a string containing %% values (e.g.
        //     %StagingStorage%). The value provided will be passed to any INameResolver registered
        //     on the JobHostConfiguration to resolve the actual setting name to use. - Can
        //     be an app setting or connection string name of your choosing.
        public NotificationHubAccountAttribute(string account)
        {
            Account = account;
        }


        /// <summary>
        /// Gets or sets the app setting name that contains the Azure notification hub connection
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string IConnectionProvider.Connection
        {
            get
            {
                return Account;
            }
            set
            {
                Account = value;
            }
        }
    }
}
