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
using Azure.Messaging.EventGrid;

namespace Assimalign.Azure.WebJobs.Extensions.Functions;

[EventGridAccount("AzureEventGrid")]
public class EventGridBindingSample
{


    [FunctionName("%FunctionName%")]
    public async Task<IActionResult> RunNameTestAsync(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "action")] HttpRequest request,
        ILogger logger)
    {
        try
        {
            throw new NotImplementedException();
        }
        catch (Exception exception)
        {
            throw exception;
        }
    }


    [FunctionName("ExampleBindEventGridInterface")]
    public async Task<IActionResult> BindValidatorAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "eventGridClient")] HttpRequest request,
        [EventGridBinding("{Some event grid endpoint}")] EventGridPublisherClient client,
        ILogger logger)
    {
        try
        {
            await client.SendEventAsync(new EventGridEvent("test", "test", "v1.0", BinaryData.FromString("{\"test\": \"value\"}"))
            {
                Topic = "test-topic"
            });
            return new OkResult();

        }
        catch (Exception exception)
        {
            throw exception;
        }
    }
}
