using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Hosting;

[assembly: WebJobsStartup(typeof(Assimalign.Azure.WebJobs.Bindings.Demo.Startup))]
namespace Assimalign.Azure.WebJobs.Bindings.Demo
{
    using Assimalign.Azure.WebJobs.Bindings.Demo.Functions.Http;

    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {

            builder.AddHttpAccessorBinding();
        }
    }
}
