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
    public static class GetSpecificCustomerFunction
    {
        [FunctionName("GetSpecificCustomer")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "customers/{id}")]
            HttpRequest request,
            string id,
            [Inject] ICustomerService customerService,
            ILogger logger
        )
        {
            logger.LogInformation($"Calling {nameof(GetCustomersFunction)}");

            if (!int.TryParse(id, out var customerId))
            {
                logger.LogError($"Error: Invalid request, id must be integer: {id}");
                return new BadRequestObjectResult($"Invalid request, id must be integer: {id}");
            }

            var getSpecificCustomerRequest = new GetSpecificCustomerRequest(customerId);
            var isValid = getSpecificCustomerRequest.Validate();

            if (!isValid)
            {
                logger.LogError("Error: Invalid request");
                return new BadRequestObjectResult("Invalid request");
            }

            var operationResult = await customerService.GetSpecificCustomerAsync(getSpecificCustomerRequest).ConfigureAwait(false);

            if (operationResult.Status)
            {
                return new OkObjectResult(operationResult.Data);
            }

            return new BadRequestObjectResult(operationResult.Message);
        }
    }
}
