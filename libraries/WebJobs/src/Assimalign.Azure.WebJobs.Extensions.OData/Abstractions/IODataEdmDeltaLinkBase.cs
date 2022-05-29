using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.Azure.WebJobs.Bindings.OData.Abstractions
{
    /// <summary>
    /// Represents an instance of an <see cref="IODataEdmChangedObject"/>.
    /// Holds the properties necessary to create either ODataDeltaLink or ODataDeltaDeletedLink.
    /// </summary>
    public interface IODataEdmDeltaLinkBase
    {
        /// <summary>
        /// The Uri of the entity from which the relationship is defined, which may be absolute or relative.
        /// </summary>
        Uri Source { get; set; }

        /// <summary>
        /// The Uri of the related entity, which may be absolute or relative.
        /// </summary>
        Uri Target { get; set; }

        /// <summary>
        /// The name of the relationship property on the parent object.
        /// </summary>
        string Relationship { get; set; }
    }
}
