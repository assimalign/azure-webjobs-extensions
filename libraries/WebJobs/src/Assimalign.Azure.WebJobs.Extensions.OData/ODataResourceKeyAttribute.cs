using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.Azure.WebJobs.Bindings.OData
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true)]
    public sealed class ODataResourceKeyAttribute : Attribute
    {
        
    }
}
