using System;
using System.Text.Json;
using Azure.Messaging.EventGrid;

namespace Assimalign.Azure.WebJobs.Bindings.EventGrid.Common
{
    /// <summary>
    /// Represents a standard Domain Event message within Assimalign Secured Architecture.
    /// </summary>
    public class DomainEvent : EventGridEvent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="eventType"></param>
        /// <param name="dataVersion"></param>
        /// <param name="data"></param>
        public DomainEvent(string subject, string eventType, string dataVersion, BinaryData data) : 
            base(subject, eventType, dataVersion, data) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="eventType"></param>
        /// <param name="dataVersion"></param>
        /// <param name="data"></param>
        /// <param name="dataSerializationType"></param>
        public DomainEvent(string subject, string eventType, string dataVersion, object data, Type dataSerializationType = null) : 
            base(subject, eventType, dataVersion, data, dataSerializationType) { }


        /// <summary>
        /// Gets a Domain Event Message.
        /// </summary>
        public DomainEventMessage Message
        {
            get
            {
                try
                {
                    return base.Data.ToObjectFromJson<DomainEventMessage>(new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true,
                        AllowTrailingCommas = true,
                        IgnoreNullValues = false
                    });
                }
                catch
                {
                    return null;
                }   
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain">The name of the domain in which the event occurred.</param>
        /// <param name="eventId">Is a unique</param>
        /// <param name="record"></param>
        /// <param name="recordRoute">Will serve as the subject of the event gird message. Best to be </param>
        /// <param name="eventVersion"></param>
        /// <param name="topic">
        /// The event grid domain topic for the event grid message. Only set 
        /// the topic name when publishing directly to a topic. The topic name is represented
        /// in the event grid URI.
        /// </param>
        /// <returns></returns>
        public static DomainEvent Create(string domain, string eventId, string record, string recordRoute, string eventVersion, string topic = null)
        {
            return new DomainEvent(
                recordRoute,
                eventId,
                eventVersion,
                BinaryData.FromObjectAsJson(new DomainEventMessage()
                {
                    Domain = domain,
                    Event = eventId,
                    RecordId = record
                }))
            {
                Topic = topic
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static DomainEvent Parse(EventGridEvent message)
        {
            return new DomainEvent(
                message.Subject,
                message.EventType,
                message.DataVersion,
                message.Data)
            {
                Id = message.Id,
                Topic = message.Topic,
                EventTime = message.EventTime,

            };
        }
    }
}
