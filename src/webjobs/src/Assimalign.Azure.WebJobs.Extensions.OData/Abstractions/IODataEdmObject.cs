using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Edm;

namespace Assimalign.Azure.WebJobs.Bindings.OData.Abstractions
{
    /// <summary>
    /// Represents an instance of an <see cref="IEdmType"/>.
    /// </summary>
    public interface IODataEdmObject
    {
        /// <summary>
        /// Gets the <see cref="IEdmTypeReference"/> of this instance.
        /// </summary>
        /// <returns>The <see cref="IEdmTypeReference"/> of this instance.</returns>
        [SuppressMessage(
            "Microsoft.Design", 
            "CA1024:UsePropertiesWhereAppropriate", 
            Justification = "This should not be serialized. Having it as a method is more appropriate.")]
        IEdmTypeReference GetEdmType();
    }
}
