using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Description;

namespace Assimalign.Azure.WebJobs.Bindings.Http
{
    [Binding()]
    [AttributeUsage(AttributeTargets.ReturnValue | AttributeTargets.Parameter)]
    public class HttpAccessorAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        public HttpAccessorAttribute(BindingTarget target = BindingTarget.Body)
        {
            Target = target;
        }

        /// <summary>
        /// Specifies the part of the Http Request to bind the model to.
        /// </summary>
        public BindingTarget Target { get; set; }
    }
}
