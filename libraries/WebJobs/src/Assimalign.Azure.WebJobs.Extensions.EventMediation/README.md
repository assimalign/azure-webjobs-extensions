# Event Mediation Triggers
Event Mediation Triggers for Azure Functions are a solution for listening to in-app asyncchronous events. This is useful when needing to break-up 

## Use Cases:
- 




# Example

```csharp

// Step 1. Create a context for the use of passing data and other information to the event handlers.
public class SomeContext : IEventContext
{
    public object State { get; set; }
    public object StateChanges { get; set; }
}


// Step 2. Create a Function to emit/notify the occurrence of an event.
//         The Function below will be responsible for notifying the triggers listening for particular event that 
[FunctionName("SomeCoreEventFunction")]
    public async Task<IActionResult> RunCoreEventAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "mediator/trigger")] HttpRequest request,
        [EventMediationBinding("core-event-mediator")] IEventMediator mediator, // Binds the specified mediator. Mediators are injected on the fly.
        ILogger logger)
    {
        try
        {
            // Create your own context and pass whatever data is needed down stream.
            mediator.Notify("core-event-01", new SomeContext());

            return new OkResult();
        }
        catch (Exception exception)
        {
            return new BadRequest();
        }
    }


    /*
        The Triggers listed below will be responsible for carrying out logic for a particular event. 
    
    */
    [FunctionName("CoreEvent_01_Listener")]
    public async Task Run1Async(
        [EventMediationTrigger("core-event-mediator", "core-event-01")] IEventContext context, 
        ILogger logger)
    {
        if (context is SomeContext someContext)
        {
            logger.LogInformation("'test-event-01' was fired for 'test-mediator'.");
        }
    }

    [FunctionName("CoreEvent_02_Listener")]
    public async Task Run2Async(
        [EventMediationTrigger("core-event-mediator", "core-event-02")] IEventContext context,
        ILogger logger)
    {
        logger.LogInformation("'test-event-02' was fired for 'test-mediator'.");
    }

```