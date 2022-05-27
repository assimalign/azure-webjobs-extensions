using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Description;

namespace Assimalign.Azure.WebJobs.Extensions;

/// <summary>
/// 
/// </summary>
[Binding]
[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
public sealed class MapperBindingAttribute : Attribute
{
    /// <summary>
    /// 
    /// </summary>
    public MapperBindingAttribute() { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mapperName"></param>
    public MapperBindingAttribute(string mapperName)
    {
        this.MapperName = mapperName;
    }

    /// <summary>
    /// 
    /// </summary>
    public string MapperName { get; } = "default";
}
