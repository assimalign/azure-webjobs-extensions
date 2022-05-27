using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.Azure.WebJobs.Bindings.OData.Abstractions
{
    /// <summary>
    /// Represents an instance of an <see cref="IODataEdmChangedObject"/>.
    /// Holds the properties necessary to create the ODataDeltaDeletedLink.
    /// </summary>
    public interface IODataEdmDeltaDeletedLink : IODataEdmDeltaLinkBase, IODataEdmChangedObject
    {
    }
}
