using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Assimalign.Azure.WebJobs.Extensions.Functions;

public class EventMediationTriggerSample
{
    [FunctionName("ExampleBindMediatorInterface")]
    public async Task<IActionResult> RunHttpFuncAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "test/mediator/trigger")] HttpRequest request,
        [EventMediationBinding("test-mediator")] IEventMediator mediator,
        ILogger logger)
    {
        try
        {

            mediator.Notify("test-event-02", new EContext());

            return new OkResult();
            //throw new NotImplementedException();
        }
        catch (Exception exception)
        {
            throw exception;
        }
    }

    public class EContext : IEventContext
    {
        public object State => throw new NotImplementedException();

        public object StateChanges => throw new NotImplementedException();
    }


    [FunctionName("ExampleTriggerListenForInAppEvent01")]
    public async Task Run1Async(
        [EventMediationTrigger("test-mediator", "test-event-01")] IEventContext context)
    {

    }

    [FunctionName("ExampleTriggerListenForInAppEvent02")]
    public async Task Run2Async(
        [EventMediationTrigger("test-mediator", "test-event-02")] IEventContext context)
    {
        throw new Exception();
    }
}
