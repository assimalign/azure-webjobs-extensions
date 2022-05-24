//-----------------------------------------------------------------------------
// <copyright file="INavigationSourceConvention.cs" company=".NET Foundation">
//      Copyright (c) .NET Foundation and Contributors. All rights reserved. 
//      See License.txt in the project root for license information.
// </copyright>
//------------------------------------------------------------------------------

namespace Assimalign.Azure.WebJobs.Bindings.OData.Builder.Conventions
{
    internal interface INavigationSourceConvention : IConvention
    {
        void Apply(NavigationSourceConfiguration configuration, ODataModelBuilder model);
    }
}