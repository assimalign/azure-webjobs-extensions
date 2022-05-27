using System;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Config;

namespace Assimalign.Azure.WebJobs.Extensions.Internal;

using Assimalign.Extensions.Validation;


[Extension("ValidatorExtension")]
internal sealed class ValidatorBindingExtensionsConfigProvider : IExtensionConfigProvider
{
    private readonly IValidatorFactory factory;

    public ValidatorBindingExtensionsConfigProvider(IValidatorFactory factory)
    {
        this.factory = factory;
    }


    public void Initialize(ExtensionConfigContext context)
    {
        context.AddBindingRule<ValidatorBindingAttribute>()
            .BindToInput(attribute =>
            {
                if (string.IsNullOrEmpty(attribute.ValidatorName))
                {
                    throw new ArgumentNullException(nameof(attribute.ValidatorName));
                }
                return factory.CreateValidator(attribute.ValidatorName);
            });
    }
}
