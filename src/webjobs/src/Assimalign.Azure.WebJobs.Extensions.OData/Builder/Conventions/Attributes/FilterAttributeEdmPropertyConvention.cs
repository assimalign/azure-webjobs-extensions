//-----------------------------------------------------------------------------
// <copyright file="FilterAttributeEdmPropertyConvention.cs" company=".NET Foundation">
//      Copyright (c) .NET Foundation and Contributors. All rights reserved. 
//      See License.txt in the project root for license information.
// </copyright>
//------------------------------------------------------------------------------

using System;
using Assimalign.Azure.WebJobs.Bindings.OData.Common;
using Microsoft.AspNet.OData.Query;

namespace Assimalign.Azure.WebJobs.Bindings.OData.Builder.Conventions.Attributes
{
    internal class FilterAttributeEdmPropertyConvention : AttributeEdmPropertyConvention<PropertyConfiguration>
    {
        public FilterAttributeEdmPropertyConvention()
            : base(attribute => attribute.GetType() == typeof(FilterAttribute), allowMultiple: true)
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
                FilterAttribute filterAttribute = attribute as FilterAttribute;
                ModelBoundQuerySettings querySettings = edmProperty.QueryConfiguration.GetModelBoundQuerySettingsOrDefault();
                if (querySettings.FilterConfigurations.Count == 0)
                {
                    querySettings.CopyFilterConfigurations(filterAttribute.FilterConfigurations);
                }
                else
                {
                    foreach (var property in filterAttribute.FilterConfigurations.Keys)
                    {
                        querySettings.FilterConfigurations[property] =
                            filterAttribute.FilterConfigurations[property];
                    }
                }

                if (filterAttribute.FilterConfigurations.Count == 0)
                {
                    querySettings.DefaultEnableFilter = filterAttribute.DefaultEnableFilter;
                }
            }
        }
    }
}
