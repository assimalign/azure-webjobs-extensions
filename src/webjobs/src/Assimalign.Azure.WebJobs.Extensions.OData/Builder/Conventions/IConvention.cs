//-----------------------------------------------------------------------------
// <copyright file="IConvention.cs" company=".NET Foundation">
//      Copyright (c) .NET Foundation and Contributors. All rights reserved. 
//      See License.txt in the project root for license information.
// </copyright>
//------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

namespace Assimalign.Azure.WebJobs.Bindings.OData.Builder.Conventions
{
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = "Marker interface acceptable here for derivation")]
    internal interface IConvention
    {
    }
}
