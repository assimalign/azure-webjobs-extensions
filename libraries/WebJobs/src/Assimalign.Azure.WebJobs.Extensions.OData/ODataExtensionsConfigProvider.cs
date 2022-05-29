
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Extensions.Options;


namespace Assimalign.Azure.WebJobs.Bindings.OData
{
    internal sealed partial class ODataExtensionsConfigProvider : IExtensionConfigProvider
    {

        private readonly IHttpContextAccessor http;
        private readonly ODataOptions options;


        public ODataExtensionsConfigProvider(
           IHttpContextAccessor http,
           IOptions<ODataOptions> options)
        {
            this.http = http;
            this.options = options.Value;
        }


        public void Initialize(ExtensionConfigContext context)
        {
            context.AddBindingRule<ODataResourceAttribute>()
                 .BindToInput<object>((attribute, context) => 
                    BindODataHttpResourceRequest(attribute, context));

        }



        /// <summary>
        /// Binds JSON resource requests to the specified 
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private Task<object> BindODataHttpResourceRequest(ODataResourceAttribute attribute, ValueBindingContext context)
        {
            //context.FunctionContext.
            var functionName = context.FunctionContext.MethodName;
            var type = options.Types[functionName];
            var query = http.HttpContext.Request.Query.ToDictionary(x => x.Key, x => x.Value.ToString());
            var headers = http.HttpContext.Request.Headers.ToDictionary(x => x.Key, x => x.Value.ToString());
            var body = http.HttpContext.Request.Body;

            return Task.Run(async () =>
            {
                var results = Activator.CreateInstance(type);

                if (body.Length > 0)
                {
                    body.Position = 0;
                    results = await options.Serializer.DeserializeAsync(body, type, headers);
                }

                return results;
            });
        }
    }
}
