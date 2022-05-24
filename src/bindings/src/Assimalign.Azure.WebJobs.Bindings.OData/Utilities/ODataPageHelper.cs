﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace Assimalign.Azure.WebJobs.Bindings.OData.Utilities
{
    using Assimalign.Azure.WebJobs.Bindings.OData.Options;
    using System.Globalization;

    internal class ODataPageHelper
    {
        public static Uri GetNextPageLink(Uri requestUri, int pageSize, object instance = null, Func<object, String> objectToSkipTokenValue = null)
        {
            //Contract.Assert(requestUri != null);

            var queryValues = QueryHelpers.ParseQuery(requestUri.Query);
            var queryParameters = queryValues
                .SelectMany(
                    collectionSelector: kvp => kvp.Value, 
                    resultSelector: (kvp, value) => new KeyValuePair<string, string>(kvp.Key, value));

            return GetNextPageLink(requestUri, queryParameters, pageSize, instance, objectToSkipTokenValue);
        }

        internal static Uri GetNextPageLink(Uri requestUri, IEnumerable<KeyValuePair<string, string>> queryParameters, int pageSize, object instance = null, Func<object, string> objectToSkipTokenValue = null, CompatibilityOptions options = CompatibilityOptions.None)
        {
           // Contract.Assert(requestUri != null);
            //Contract.Assert(queryParameters != null);

            StringBuilder queryBuilder = new StringBuilder();

            int nextPageSkip = pageSize;

            String skipTokenValue = objectToSkipTokenValue == null ? null : objectToSkipTokenValue(instance);
            //If no value for skiptoken can be extracted; revert to using skip 
            bool useDefaultSkip = String.IsNullOrWhiteSpace(skipTokenValue);

            foreach (KeyValuePair<string, string> kvp in queryParameters)
            {
                string key = kvp.Key.ToLowerInvariant();
                string value = kvp.Value;

                switch (key)
                {
                    case "$top":
                        int top;
                        if (Int32.TryParse(value, out top))
                        {
                            // We decrease top by the pageSize because that's the number of results we're returning in the current page. If the $top query option's value is less than or equal to the page size, there is no next page.
                            if ((options & CompatibilityOptions.AllowNextLinkWithNonPositiveTopValue) != 0 || top > pageSize)
                            {
                                value = (top - pageSize).ToString(CultureInfo.InvariantCulture);
                            }
                            else
                            {
                                return null;
                            }
                        }
                        break;
                    case "$skip":
                        if (useDefaultSkip)
                        {
                            //Need to increment skip only if we are not using skiptoken 
                            int skip;
                            if (Int32.TryParse(value, out skip))
                            {
                                // We increase skip by the pageSize because that's the number of results we're returning in the current page
                                nextPageSkip += skip;
                            }
                        }
                        continue;
                    case "$skiptoken":
                        continue;
                    default:
                        break;
                }

                if (key.Length > 0 && key[0] == '$')
                {
                    // $ is a legal first character in query keys
                    key = '$' + Uri.EscapeDataString(key.Substring(1));
                }
                else
                {
                    key = Uri.EscapeDataString(key);
                }

                value = Uri.EscapeDataString(value);

                queryBuilder.Append(key);
                queryBuilder.Append('=');
                queryBuilder.Append(value);
                queryBuilder.Append('&');
            }

            if (useDefaultSkip)
            {
                queryBuilder.AppendFormat("$skip={0}", nextPageSkip);
            }
            else
            {
                queryBuilder.AppendFormat("$skiptoken={0}", skipTokenValue);
            }

            var uriBuilder = new UriBuilder(requestUri)
            {
                Query = queryBuilder.ToString()
            };

            return uriBuilder.Uri;
        }
    }
}
