using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.Azure.WebJobs.Bindings.EventGrid.Common
{
    /// <summary>
    /// A standard message format for submitting domain events across Assimalign Secured Architecture.
    /// </summary>
    public class DomainEventMessage
    {
        /// <summary>
        /// The domain the event occurred.
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// A string value indicating the event that occurred in the domain.
        /// </summary>
        /// <example>order-submitted; order-processed</example>
        public string Event { get; set; }

        /// <summary>
        /// The effected record to reference for a particular event.
        /// </summary>
        public string RecordId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IDictionary<string, dynamic> Meta { get; set; }
    }
}
