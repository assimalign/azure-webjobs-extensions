using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Assimalign.Azure.WebJobs.Bindings.Http
{
    using Assimalign.Azure.WebJobs.Bindings.Http.Utilities;

    public abstract class HttpAccessorQueryParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual object Parse(IDictionary<string,string> parameters, Type type)
        {
            var instance = Activator.CreateInstance(type);
            foreach (var property in type.GetProperties())
            {
                if (parameters.TryGetValue(property.Name, out var value))
                {
                    property.SetValue(instance, value.ToType(property.PropertyType));
                }
            }

            return instance;
        }
    }

    internal class HttpAccessorQueryParserDefault : HttpAccessorQueryParser
    {
        public override object Parse(IDictionary<string, string> parameters, Type type)
        {
            return base.Parse(parameters, type);
        }
    }
}
