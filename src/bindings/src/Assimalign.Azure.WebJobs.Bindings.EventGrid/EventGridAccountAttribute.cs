using System;
using Microsoft.Azure.WebJobs;


namespace Assimalign.Azure.WebJobs.Bindings.EventGrid
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Parameter)]
    public class EventGridAccountAttribute : Attribute, IConnectionProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        public EventGridAccountAttribute(string account)
        {
            Account = account;
        }


        /// <summary>
        /// Gets or sets the app setting name that contains the Azure event grid connection
        /// </summary>
        public string Account { get; set; }


        /// <summary>
        /// 
        /// </summary>
        string IConnectionProvider.Connection
        {
            get =>  Account;
            set => Account = value;
        }
    }
}
