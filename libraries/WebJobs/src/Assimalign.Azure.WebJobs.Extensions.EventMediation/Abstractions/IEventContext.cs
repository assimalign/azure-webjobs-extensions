using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.Azure.WebJobs.Extensions;

/// <summary>
/// 
/// </summary>
public interface IEventContext
{
    /// <summary>
    /// An object to be used to pass state variables down to the event listeners.
    /// </summary>
    object State { get; }
}
