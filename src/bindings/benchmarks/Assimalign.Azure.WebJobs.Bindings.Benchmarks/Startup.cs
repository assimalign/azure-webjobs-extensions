using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;


[assembly: WebJobsStartup(typeof(Assimalign.Azure.WebJobs.Bindings.Startup))]

namespace Assimalign.Azure.WebJobs.Bindings;

using Assimalign.Azure.WebJobs.Bindings.Functions;

public class Startup : IWebJobsStartup
{
    public void Configure(IWebJobsBuilder builder)
    {
        builder.AddValidatorBinding(factory =>
        {
            factory.AddValidator("sample-validator-1", configure =>
            {
                configure.AddProfile(new ValidationBindingProfile());
            });
        });

        builder.AddMapperBinding(builder =>
        {
            builder.AddMapper("sample-mapper-1", mapper =>
            {
                mapper.CreateProfile<MapperTestObject1, MapperTestObject2>(descriptor =>
                {
                    descriptor.MapMember(target => target.FirstName, source => source.Name);
                });
            });
        });
    }
}

