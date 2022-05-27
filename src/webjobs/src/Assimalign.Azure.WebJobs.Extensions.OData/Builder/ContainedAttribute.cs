//-----------------------------------------------------------------------------
// <copyright file="ContainedAttribute.cs" company=".NET Foundation">
//      Copyright (c) .NET Foundation and Contributors. All rights reserved. 
//      See License.txt in the project root for license information.
// </copyright>
//------------------------------------------------------------------------------

using System;

namespace Assimalign.Azure.WebJobs.Bindings.OData.Builder
{
    /// <summary>
    /// Mark a navigation property as containment.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ContainedAttribute : Attribute
    {
    }
}
