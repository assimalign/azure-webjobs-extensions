using System;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.Azure.WebJobs.Bindings.OData.Abstractions
{
    /// <summary>
    /// Represents an instance of an enum value.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = "Marker interface acceptable here for derivation")]
    public interface IODataEnumObject : IODataEdmObject
    {
        
    }
}
