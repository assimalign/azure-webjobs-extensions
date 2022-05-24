using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.Azure.WebJobs.Bindings.Functions;

using Assimalign.Extensions.Validation;


public class ValidationBindingSamples
{


    [FunctionName("ExampleBindValidatorInterface")]
    public async Task<IActionResult> BindValidatorAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "action")] HttpRequest request,
        [ValidatorBinding("sample-validator-1")] IValidator validator,
        ILogger logger)
    {
        try
        {
            var validation = validator.Validate(new ValidationObject());

            return new OkObjectResult(validation);
        }
        catch (Exception exception)
        {
            throw exception;
        }
    }


}


public class ValidationObject
{
    public string FirstName { get; set; }
}

public class ValidationBindingProfile : ValidationProfile<ValidationObject>
{
    public override void Configure(IValidationRuleDescriptor<ValidationObject> descriptor)
    {
        descriptor.RuleFor(p => p.FirstName)
            .NotEmpty();
    }
}