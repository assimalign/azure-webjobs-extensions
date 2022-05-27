using System;
using System.Linq;
using System.Reflection;
using Microsoft.Azure.WebJobs;

namespace Assimalign.Azure.WebJobs.Extensions;

public static class DDataWebJobsExtensions
{
    public static IWebJobsBuilder AddDDataBinding(this IWebJobsBuilder builder)
    {
        Assembly.GetExecutingAssembly()
            .GetTypes()
            .FirstOrDefault()


        return builder;
    }
}