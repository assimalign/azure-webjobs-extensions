using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Azure.WebJobs
{
    using Assimalign.Azure.WebJobs.Bindings.Http;

    public static class HttpAccessorWebJobsExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IWebJobsBuilder AddHttpAccessorBinding(this IWebJobsBuilder builder)
        {
            builder.Services.AddHttpContextAccessor();
            var functions = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.DefinedTypes.SelectMany(b => b.DeclaredMethods))
                .Where(x => x.CustomAttributes.Any(c => c.AttributeType == typeof(FunctionNameAttribute)));

            builder.Services.AddOptions<HttpAccessorOptions>()
                .Configure(options =>
                {
                    var httpfunctions = functions.Select(x => x.GetParameters());

                    foreach (var function in functions)
                    {
                        var functionName = function.CustomAttributes
                            .FirstOrDefault(x => x.AttributeType == typeof(FunctionNameAttribute)).ConstructorArguments
                            .First().Value.ToString();

                        var parameterType = function.GetParameters()
                            .FirstOrDefault(x => x.CustomAttributes
                            .Any(x => x.AttributeType == typeof(HttpAccessorAttribute)))?.ParameterType;

                        if (functionName != null && parameterType != null)
                        {
                            options.Types.Add(functionName, parameterType);
                        }
                    }
                });

            builder.AddExtension<HttpAccessorExtensionProvider>();
            return builder;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IWebJobsBuilder AddHttpAccessorBinding(this IWebJobsBuilder builder, Action<HttpAccessorOptions> configure)
        {
            builder.Services.AddHttpContextAccessor();
            var functions = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.DefinedTypes.SelectMany(b => b.DeclaredMethods))
                .Where(x => x.CustomAttributes.Where(c => c.AttributeType == typeof(FunctionNameAttribute)).Any());

            builder.Services.AddOptions<HttpAccessorOptions>()
                .Configure(options =>
                {
                    configure(options);
                    foreach (var function in functions)
                    {
                        var functionName = function.CustomAttributes
                            .FirstOrDefault(x => x.AttributeType == typeof(FunctionNameAttribute)).ConstructorArguments
                            .First().Value.ToString();

                        var parameterType = function.GetParameters()
                            .FirstOrDefault(x => x.CustomAttributes.Any(x => x.AttributeType == typeof(HttpAccessorAttribute)))
                            .ParameterType;

                        options.Types.Add(functionName, parameterType);
                    }
                });

            builder.AddExtension<HttpAccessorExtensionProvider>();
            return builder;
        }
    }
}
