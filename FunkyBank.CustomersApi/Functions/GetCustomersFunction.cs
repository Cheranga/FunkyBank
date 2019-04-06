using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AzureFunctions.Autofac;
using FunkyBank.Core;
using FunkyBank.DTO.Requests;
using FunkyBank.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunkyBank.CustomersApi.Functions
{
    [DependencyInjectionConfig(typeof(ApiBootstrapper))]
    public static class GetCustomersFunction
    {
        [FunctionName("GetCustomers")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "customers")]
            HttpRequest req,
            [Inject] ICustomerService customerService,
            ILogger logger)
        {
            logger.LogInformation($"Calling {nameof(GetCustomersFunction)}");

            var getCustomersRequest = JsonConvert.DeserializeObject<GetCustomersRequest>(await new StreamReader(req.Body).ReadToEndAsync());

            var isValid = getCustomersRequest.Validate();

            if (!isValid)
            {
                logger.LogError("Error: Invalid request");
                return new BadRequestObjectResult("Invalid request");
            }

            var operationResult = await customerService.GetCustomersAsync(getCustomersRequest).ConfigureAwait(false);

            if (operationResult.Status)
            {
                return new OkObjectResult(operationResult.Data);
            }

            return new BadRequestObjectResult(operationResult.Message);
        }
    }
}
