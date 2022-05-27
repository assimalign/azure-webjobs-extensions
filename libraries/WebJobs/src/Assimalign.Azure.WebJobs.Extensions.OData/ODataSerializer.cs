using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Assimalign.Azure.WebJobs.Bindings.OData
{
    public abstract class ODataSerializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="type"></param>
        /// <param name="headers">The request headers</param>
        /// <returns></returns>
        public virtual Task<object> DeserializeAsync(Stream stream, Type type, IDictionary<string, string> headers)
        {
            if (headers.TryGetValue("Content-Type", out var contentType))
            {
                if (contentType.Equals("application/json"))
                {
                    return DeserializeJsonAsync(stream, type);
                }
                if (contentType.Equals("application/xml"))
                {
                    return DeserializeXmlAsync(stream, type);
                }
            }

            return Task.FromResult<object>(null);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private Task<object> DeserializeJsonAsync(Stream stream, Type type)
        {
            return JsonSerializer.DeserializeAsync(stream, type, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                IgnoreNullValues = true,
                AllowTrailingCommas = true,
                ReadCommentHandling = JsonCommentHandling.Skip

            }).AsTask();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private Task<object> DeserializeXmlAsync(Stream stream, Type type)
        {
            var serializer = new XmlSerializer(type);

            return Task.FromResult(serializer.Deserialize(stream));
        }

    }

    internal class ODataBindingSerializerDefault : ODataSerializer
    {
        public override Task<object> DeserializeAsync(Stream stream, Type type, IDictionary<string, string> headers)
        {
            return base.DeserializeAsync(stream, type, headers);
        }
    }
}
