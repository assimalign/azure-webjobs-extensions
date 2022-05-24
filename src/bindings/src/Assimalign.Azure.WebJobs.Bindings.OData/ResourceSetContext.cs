﻿using System;
using Microsoft.OData.Edm;

namespace Assimalign.Azure.WebJobs.Bindings.OData
{
    using Assimalign.Azure.WebJobs.Bindings.OData.Abstractions;
    /// <summary>
    /// Contains context information about the resource set currently being serialized.
    /// </summary>
    public partial class ResourceSetContext
    {
        /// <summary>
        /// Gets the <see cref="IEdmEntitySetBase"/> this instance belongs to.
        /// </summary>
        public IEdmEntitySetBase EntitySetBase { get; set; }

        /// <summary>
        /// Gets the value of this feed instance.
        /// </summary>
        public object ResourceSetInstance { get; set; }

        /// <summary>
        /// Gets or sets the HTTP request that caused this instance to be generated.
        /// </summary>
        internal IWebApiRequestMessage InternalRequest { get; private set; }

        /// <summary>
        /// Gets or sets the <see cref="IWebApiUrlHelper"/> to be used for generating links while serializing this
        /// feed instance.
        /// </summary>
        internal IWebApiUrlHelper InternalUrlHelper { get; private set; }
    }
}
