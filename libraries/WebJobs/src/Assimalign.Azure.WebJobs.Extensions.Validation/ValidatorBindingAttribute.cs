using System;
using Microsoft.Azure.WebJobs.Description;

namespace Assimalign.Azure.WebJobs.Extensions;

using Assimalign.Extensions.Validation;

/// <summary>
/// An azure binding attribute for <see cref="IValidator"/>.
/// </summary>
[Binding]
[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
public sealed class ValidatorBindingAttribute : Attribute
{
    /// <summary>
    /// The default constructor for creating a validator under the default Validator Name.
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
