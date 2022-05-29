using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.Azure.WebJobs.Bindings.OData
{
    public class ODataOptions
    {

        /// <summary>
        /// A Collection of Types registered for each function using the HttpAccessorAttribute at Startup
        /// </summary>
        internal IDictionary<string, Type> Types = new Dictionary<string, Type>();

        /// <summary>
        /// 
        /// </summary>
        public ODataSerializer Serializer { get; set; } = new ODataBindingSerializerDefault();
    }
}
