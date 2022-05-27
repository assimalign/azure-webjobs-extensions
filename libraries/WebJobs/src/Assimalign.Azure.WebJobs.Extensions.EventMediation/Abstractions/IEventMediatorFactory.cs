namespace Assimalign.Azure.WebJobs.Extensions;

/// <summary>
/// A factory implementation for creating <see cref="IEventMediator"/>.
/// </summary>
public interface IEventMediatorFactory
{
    /// <summary>
    /// Creates a new instance of <see cref="IEventMediator"/>.
    /// </summary>
    /// <param name="mediatorId">The unique identifier the instance of the Event Mediator.</param>
    /// <returns><see cref="IEventMediator"/></returns>
    IEventMediator CreateMediator(string mediatorId);
}
