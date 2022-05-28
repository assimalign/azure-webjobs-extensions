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

namespace Assimalign.Azure.WebJobs.Extensions.Internal;

internal sealed class DDataSchemaTrigger
{
    [FunctionName("DDataSchema")]
    public async Task<IActionResult> GetDDataSchemaAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "{domain}/$schema")] HttpRequest request,
        string domain,
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
}
