using Microsoft.Extensions.DependencyInjection;

namespace Assimalign.Azure.WebJobs.Bindings.OData.Abstractions
{
    /// <summary>
    /// An interface for configuring essential OData services.
    /// </summary>
    public interface IODataBuilder
    {
        /// <summary>
        /// Gets the <see cref="IServiceCollection"/> where essential OData services are configured.
        /// </summary>
        IServiceCollection Services { get; }
    }
}
