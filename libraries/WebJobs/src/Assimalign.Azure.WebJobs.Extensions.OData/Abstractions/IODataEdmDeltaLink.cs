using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.Azure.WebJobs.Bindings.OData.Abstractions
{
    /// <summary>
    /// Represents an instance of an <see cref="IEdmChangedObject"/>.
    /// Holds the properties necessary to create the ODataDeltaLink.
    /// </summary>
    public interface IODataEdmDeltaLink : IODataEdmDeltaLinkBase, IODataEdmChangedObject
    {
    }
}
