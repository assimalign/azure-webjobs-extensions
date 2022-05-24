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

namespace Assimalign.Azure.WebJobs.Bindings.Demo.Functions.Http
{
    using Assimalign.Azure.WebJobs.Bindings.Http;

    public class ModelBinderDemo
    {

        public ModelBinderDemo()
        {

        }




        [FunctionName("http_model_binder_sample_1")]
        public async Task<IActionResult> PostSampleOneAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "modelbinder/route/one")] HttpRequest request,
            [HttpAccessor] User user,
            ILogger logger)
        {
            try
            {
                return new OkObjectResult(user);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }



        [FunctionName("http_model_binder_sample_2")]
        public async Task<IActionResult> PostSampleTwoAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "modelbinder/route/two")] HttpRequest request,
            [HttpAccessor] User user,
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

    public class User
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
