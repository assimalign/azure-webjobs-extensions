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
    /// 
    /// </summary>
    object State { get; }

    /// <summary>
    /// 
    /// </summary>
    object StateChanges { get; }
}
