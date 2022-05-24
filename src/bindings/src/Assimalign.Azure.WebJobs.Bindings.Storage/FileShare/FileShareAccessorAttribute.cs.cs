using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Description;

namespace Assimalign.Azure.WebJobs.Bindings.Storage
{
    [Binding]
    [ConnectionProvider(typeof(StorageAccountAttribute))]
    [AttributeUsage(AttributeTargets.ReturnValue | AttributeTargets.Parameter)]
    public sealed class FileShareAccessorAttribute : Attribute, IConnectionProvider
    {
        /// <summary>
        /// Constructor for accessing service clients
        /// </summary>
        public FileShareAccessorAttribute() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileshare"></param>
        /// <param name="path"></param>
        public FileShareAccessorAttribute(string fileshare, string path)
        {
            FileShare = fileshare;
            Path = path;
        }

        /// <summary>
        /// 
        /// </summary>
        public string FileShare { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string Path { get; set; }


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
