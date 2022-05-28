using System;
using System.Linq;
using System.Reflection;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host.Protocols;
using Microsoft.Extensions.DependencyInjection;

namespace Assimalign.Azure.WebJobs.Extensions;

public static class DDataWebJobsExtensions
{
    public static IWebJobsBuilder AddDDataBinding(this IWebJobsBuilder builder)
    {

        return builder;
    }
}