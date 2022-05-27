using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.Azure.WebJobs.Bindings.OData.Abstractions
{
    using Assimalign.Azure.WebJobs.Bindings.OData.Shared;

    /// <summary>
    /// Represents an instance of an <see cref="IODataEdmChangedObject"/>.
    /// Base interface to be implemented by any Delta object required to be part of the DeltaFeed Payload.
    /// </summary>
    public interface IODataEdmChangedObject : IODataEdmStructuredObject
    {
        /// <summary>
        /// DeltaKind for the objects part of the DeltaFeed Payload.
        /// Used to determine which Delta object to create during serialization.
        /// </summary>
        ODataEdmDeltaEntityKind DeltaKind { get; }
    }
}
