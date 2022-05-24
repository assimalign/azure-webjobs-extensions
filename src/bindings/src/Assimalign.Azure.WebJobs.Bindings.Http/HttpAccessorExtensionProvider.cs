using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Extensions.Options;

namespace Assimalign.Azure.WebJobs.Bindings.Http
{
    internal class HttpAccessorExtensionProvider : IExtensionConfigProvider
    {
        private readonly IHttpContextAccessor http;
        private readonly HttpAccessorOptions options;


        public HttpAccessorExtensionProvider(
           IHttpContextAccessor http,
           IOptions<HttpAccessorOptions> options)
        {
            this.http = http;
            this.options = options.Value;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Initialize(ExtensionConfigContext context)
        {
            context.AddBindingRule<HttpAccessorAttribute>()
                .BindToInput<object>((attribute, context) => 
                    BindHttpRequest(attribute, context));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private Task<object> BindHttpRequest(HttpAccessorAttribute attribute, ValueBindingContext context)
        {
            var functionName = context.FunctionContext.MethodName;
            var type = options.Types[functionName];
            var query = http.HttpContext.Request.Query.ToDictionary(x => x.Key, x => x.Value.ToString());
            var headers = http.HttpContext.Request.Headers.ToDictionary(x => x.Key, x => x.Value.ToString());
            var body = http.HttpContext.Request.Body;

            return Task.Run(async () =>
            {
                var results = Activator.CreateInstance(type);

                if (attribute.Target == BindingTarget.Both || attribute.Target == BindingTarget.Query)
                {
                    results = options.Parser.Parse(query, type);
                }
                else if (attribute.Target == BindingTarget.Both || attribute.Target == BindingTarget.Body)
                {
                    if (body.Length > 0)
                    {
                        body.Position = 0;
                        results = await options.Serializer.DeserializeAsync(body, type, headers);
                    }
                }

                return results;
            });
        }
    }
}
