using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Description;

namespace Assimalign.Azure.WebJobs.Bindings;

[Binding]
[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
public sealed class MapperBindingAttribute : Attribute
{
    public MapperBindingAttribute() { }

    public MapperBindingAttribute(string mapperName)
    {
        this.MapperName = mapperName;
    }


    public string MapperName { get; } = "default";
}
