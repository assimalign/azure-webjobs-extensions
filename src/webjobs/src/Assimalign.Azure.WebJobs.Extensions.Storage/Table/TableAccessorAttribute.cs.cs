using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Description;

namespace Assimalign.Azure.WebJobs.Bindings.Storage
{
    [Binding]
    [ConnectionProvider(typeof(StorageAccountAttribute))]
    [AttributeUsage(AttributeTargets.ReturnValue | AttributeTargets.Parameter)]
    public sealed class TableAccessorAttribute : Attribute, IConnectionProvider
    {
        /// <summary>
        /// Constructor for accessing service clients
        /// </summary>
        public TableAccessorAttribute() { }


        /// <summary>
        /// Constructor for accessing a container client.
        /// </summary>
        /// <param name="table">The name of the Table to reference</param>
        /// <param name="ensureExists">Ensures the creation of the table in the storage account if it does not exist.</param>
        public TableAccessorAttribute(string table, bool ensureExists = false)
        {
            Table = table;
            EnsureTableExists = ensureExists;
        }

        /// <summary>
        /// The name of the table with the Storage Account.
        /// </summary>
        public string Table { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public bool EnsureTableExists { get; set; }


        /// <summary>
        /// The configuration name or the connection string to the storage account.
        /// </summary>
        public string Connection { get; set; }

        /// <summary>
        /// The endpoint to the storage account to connect to.
        /// </summary>
        public string Uri { get; set; }

    }
}
