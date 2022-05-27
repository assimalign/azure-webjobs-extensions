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

namespace Assimalign.Azure.WebJobs.Extensions.Functions;

using Assimalign.Extensions.Mapping;


public class MappingBindingSamples
{
    [FunctionName("ExampleBindMapperInterface")]
    public async Task<IActionResult> BindValidatorAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "map")] HttpRequest request,
        [MapperBinding("sample-mapper-1")] IMapper mapper,
        ILogger logger)
    {
        try
        {
            if (request.Query.TryGetValue("name", out var name))
            {
                var obj = mapper.Map<MapperTestObject1>(new MapperTestObject2()
                {
                    Name = name[0]
                });

                return new OkObjectResult(obj);
            }


            return new BadRequestResult();

        }
        catch (Exception exception)
        {
            throw exception;
        }
    }
}



public class MapperTestObject1
{
    public string FirstName { get; set; }
}

public class MapperTestObject2
{
    public string Name { get; set; }
}