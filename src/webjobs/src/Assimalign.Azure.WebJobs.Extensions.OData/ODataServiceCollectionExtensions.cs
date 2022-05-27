using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.Azure.WebJobs.Bindings.OData
{
    public static class ODataServiceCollectionExtensions
    {
        /// <summary>
        /// Enables query support for actions with an <see cref="IQueryable" /> or <see cref="IQueryable{T}" /> return
        /// type. To avoid processing unexpected or malicious queries, use the validation settings on
        /// <see cref="EnableQueryAttribute"/> to validate incoming queries. For more information, visit
        /// http://go.microsoft.com/fwlink/?LinkId=279712.
        /// </summary>
        /// <param name="services">The services collection.</param>
        public static IServiceCollection AddODataQueryFilter(this IServiceCollection services)
        {
            return AddODataQueryFilter(services, new EnableQueryAttribute());
        }

        /// <summary>
        /// Enables query support for actions with an <see cref="IQueryable" /> or <see cref="IQueryable{T}" /> return
        /// type. To avoid processing unexpected or malicious queries, use the validation settings on
        /// <see cref="EnableQueryAttribute"/> to validate incoming queries. For more information, visit
        /// http://go.microsoft.com/fwlink/?LinkId=279712.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <param name="queryFilter">The action filter that executes the query.</param>
        public static IServiceCollection AddODataQueryFilter(this IServiceCollection services, IActionFilter queryFilter)
        {
            if (services == null)
            {
                throw Error.ArgumentNull("services");
            }

            services.TryAddEnumerable(ServiceDescriptor.Singleton<IFilterProvider>(new QueryFilterProvider(queryFilter)));
            return services;
        }
    }
}
