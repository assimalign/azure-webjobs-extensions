using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Host.Executors;
using Microsoft.Azure.WebJobs.Host.Listeners;
using Microsoft.Azure.WebJobs.Host.Protocols;

namespace Assimalign.Azure.WebJobs.Extensions;

internal sealed class EventMediationTriggerListener : IListener, IEventHandler
{
    // private Task

    private readonly ILogger logger;
    private readonly ParameterInfo parameter;
    private readonly FunctionDescriptor descriptor;
    private readonly ITriggeredFunctionExecutor executor;
    private CancellationTokenSource cancellationTokenSource;

    public EventMediationTriggerListener(
        ITriggeredFunctionExecutor executor, 
        FunctionDescriptor descriptor,
        ParameterInfo parameter, 
        ILogger logger)
    {
        this.executor = executor;
        this.parameter = parameter;
        this.logger = logger;
        this.descriptor = descriptor;
    }

    public string EventId { get; init; }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        return Task.CompletedTask;
    }


    public async void Invoke(IEventContext context)
    {
        var functionResults = await executor.TryExecuteAsync(new TriggeredFunctionData()
        {
            TriggerValue = context

        }, cancellationTokenSource.Token);

        if (functionResults.Exception is not null)
        {
            logger.LogError(functionResults.Exception, $"An execution error occurred on Function '{descriptor.ShortName}'");
        }
    }



    public Task StopAsync(CancellationToken cancellationToken)
    {
        cancellationTokenSource.Cancel();

        return Task.CompletedTask;
    }

    public void Cancel() => StopAsync(CancellationToken.None).Wait();
    public void Dispose() { }
}
