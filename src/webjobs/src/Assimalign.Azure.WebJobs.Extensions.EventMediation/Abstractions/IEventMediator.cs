using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.Azure.WebJobs.Extensions;

/// <summary>
/// 
/// </summary>
public interface IEventMediator
{
    /// <summary>
    /// 
    /// </summary>
    string MediatorId { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="handler"></param>
    void Attach(IEventHandler handler);

    /// <summary>
    /// Notifies all handlers encapsulated within this mediator.
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="context"></param>
    void Notify(string eventId, IEventContext context);
}