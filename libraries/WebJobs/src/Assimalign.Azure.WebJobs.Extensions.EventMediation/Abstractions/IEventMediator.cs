namespace Assimalign.Azure.WebJobs.Extensions;

/// <summary>
/// 
/// </summary>
public interface IEventMediator
{
    /// <summary>
    /// The unique identifier the instance of the Event Mediator.
    /// </summary>
    string MediatorId { get; }

    /// <summary>
    /// Attaches a handler to the instance of the Mediator 
    /// to be notified when an event has occurred.
    /// </summary>
    /// <param name="handler">An object to be invoked when an event id matches the event id of the handler.</param>
    void Attach(IEventHandler handler);

    /// <summary>
    /// Notifies all handlers encapsulated within this mediator.
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="context"></param>
    void Notify(string eventId, IEventContext context);
}