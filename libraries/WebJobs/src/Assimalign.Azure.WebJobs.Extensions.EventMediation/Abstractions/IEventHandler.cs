namespace Assimalign.Azure.WebJobs.Extensions;

/// <summary>
/// 
/// </summary>
public interface IEventHandler
{
    /// <summary>
    /// 
    /// </summary>
    string EventId { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    void Invoke(IEventContext context);
}
