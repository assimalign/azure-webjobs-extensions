﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.Azure.WebJobs.Bindings.OData.Shared
{
    /// <summary>
    /// The Kind of the object within the DeltaPayload used to distinguish between Entry/DeletedEntry/DeltaLink/AddedLink.
    /// </summary>
    public enum ODataEdmDeltaEntityKind
    {
        /// <summary>
        /// Corresponds to EdmEntityObject (Equivalent of ODataEntry in ODL).
        /// </summary>
        Entry = 0,

        /// <summary>
        /// Corresponds to EdmDeltaDeletedEntityObject (Equivalent of ODataDeltaDeletedEntry in ODL).
        /// </summary>
        DeletedEntry = 1,

        /// <summary>
        /// Corresponds to EdmDeltaDeletedLink (Equivalent of ODataDeltaDeletedLink in ODL).
        /// </summary>
        DeletedLinkEntry = 2,

        /// <summary>
        /// Corresponds to EdmDeltaLink (Equivalent of ODataDeltaLink in ODL).
        /// </summary>
        LinkEntry = 3,

        /// <summary>
        /// Corresponds to any Unknown item added.
        /// </summary>
        Unknown = 4
    }
}
