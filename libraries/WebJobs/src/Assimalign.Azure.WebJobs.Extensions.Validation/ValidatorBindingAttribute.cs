using System;
using Microsoft.Azure.WebJobs.Description;

namespace Assimalign.Azure.WebJobs.Extensions;

/// <summary>
/// 
/// </summary>
[Binding]
[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
public sealed class ValidatorBindingAttribute : Attribute
{
    /// <summary>
    /// 
    /// </summary>
    public ValidatorBindingAttribute() { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="validatorName">The name of the validator to be returned.</param>
    public ValidatorBindingAttribute(string validatorName)
    {
        this.ValidatorName = validatorName;
    }

    /// <summary>
    /// 
    /// </summary>
    public string ValidatorName { get; } = "default";
}
