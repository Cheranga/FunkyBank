using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace FunkyBank.CustomersApi.Functions
{
    public static class WarmUpFunction
    {
        [FunctionName("Warmup")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "warmup")]
            HttpRequest request,
            ILogger logger)
        {
            logger.LogInformation("Warmup function got called");

            return new OkResult();
        }
    }
}
