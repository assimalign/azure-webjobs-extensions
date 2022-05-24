using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.Azure.WebJobs.Bindings.Http
{
    public sealed class HttpAccessorOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public HttpAccessorOptions() { }


        /// <summary>
        /// A Collection of Types registered for each function using the HttpAccessorAttribute at Startup
        /// </summary>
        internal IDictionary<string, Type> Types = new Dictionary<string, Type>();

        /// <summary>
        /// The deserializer for binding the content from the body of the http request
        /// to the specified type. By default JSON and XML are supported
        /// </summary>
        public HttpAccessorSerializer Serializer { get; set; } = new HttpAccessorSerializerDefault();

        /// <summary>
        /// Parses the query parameters for the specified type
        /// </summary>
        public HttpAccessorQueryParser Parser { get; set; } = new HttpAccessorQueryParserDefault();
    }
}
