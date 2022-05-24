using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Description;

namespace Assimalign.Azure.WebJobs.Bindings.Storage
{
    [Binding]
    [ConnectionProvider(typeof(StorageAccountAttribute))]
    [AttributeUsage(AttributeTargets.ReturnValue | AttributeTargets.Parameter)]
    public sealed class BlobAccessorAttribute : Attribute, IConnectionProvider
    {
        /// <summary>
        /// Constructor for accessing service clients
        /// </summary>
        public BlobAccessorAttribute() { }


        /// <summary>
        /// Constructor for accessing a container client.
        /// </summary>
        /// <param name="container"></param>
        /// <param name="ensureExist">Will create the container if it does not exist</param>
        public BlobAccessorAttribute(string container, bool ensureExist = false)
        {
            Container = container;
            EnsureContainerExist = ensureExist;
        }

        /// <summary>
        /// Constructor for accessing a Blob Client
        /// </summary>
        /// <param name="container"></param>
        /// <param name="blob"></param>
        public BlobAccessorAttribute(string container, string blob)
        {
            Container = container;
            Blob = blob;
        }


        /// <summary>
        /// Will create the container if it does not exist.
        /// </summary>
        public bool EnsureContainerExist { get; set; }


        /// <summary>
        /// The Storage Account Blob Container to access.
        /// </summary>
        public string Container { get; set; }

        /// <summary>
        ///  The name of the Storage Account Blob to access. If blob is nested in a virtual directory make sure to include the path.
        /// </summary>
        public string Blob { get; set; }

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
