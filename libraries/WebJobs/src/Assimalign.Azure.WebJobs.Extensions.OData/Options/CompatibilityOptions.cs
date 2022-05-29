﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.Azure.WebJobs.Bindings.OData.Options
{
    /// <summary>
    /// Contains bitmasks for features that need backward compatibilty.
    /// </summary>
    [Flags]
    public enum CompatibilityOptions
    {
        /// <summary>
        /// No compatibility options are selected. 
        /// </summary>
        None = 0x0,

        /// <summary>
        ///  Generate nextlink even if the top value specified in request is less than page size when the request extension method is directly called.
        /// </summary>
        AllowNextLinkWithNonPositiveTopValue = 0x1,

        /// <summary>
        /// Disable case-insensitive request property binding.
        /// </summary>
        DisableCaseInsensitiveRequestPropertyBinding = 0x2
    }
}
