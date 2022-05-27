using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
