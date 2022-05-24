//-----------------------------------------------------------------------------
// <copyright file="CountAttributeEdmPropertyConvention.cs" company=".NET Foundation">
//      Copyright (c) .NET Foundation and Contributors. All rights reserved. 
//      See License.txt in the project root for license information.
// </copyright>
//------------------------------------------------------------------------------

using System;
using Assimalign.Azure.WebJobs.Bindings.OData.Common;
using Microsoft.AspNet.OData.Query;

namespace Assimalign.Azure.WebJobs.Bindings.OData.Builder.Conventions.Attributes
{
    internal class CountAttributeEdmPropertyConvention : AttributeEdmPropertyConvention<PropertyConfiguration>
    {
        public CountAttributeEdmPropertyConvention()
            : base(attribute => attribute.GetType() == typeof(CountAttribute), allowMultiple: false)
        {
        }

        public override void Apply(PropertyConfiguration edmProperty,
            StructuralTypeConfiguration structuralTypeConfiguration,
            Attribute attribute,
            ODataConventionModelBuilder model)
        {
            if (edmProperty == null)
            {
                throw Error.ArgumentNull("edmProperty");
            }

            if (!edmProperty.AddedExplicitly)
            {
                CountAttribute countAttribute = attribute as CountAttribute;
                if (countAttribute.Disabled)
                {
                    edmProperty.QueryConfiguration.GetModelBoundQuerySettingsOrDefault().Countable = false;
                }
                else
                {
                    edmProperty.QueryConfiguration.GetModelBoundQuerySettingsOrDefault().Countable = true;
                }
            }
        }
    }
}
