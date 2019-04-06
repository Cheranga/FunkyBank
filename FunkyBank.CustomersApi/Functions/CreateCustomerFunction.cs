using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using AzureFunctions.Autofac;
using FunkyBank.Core;
using FunkyBank.DataAccess.Dapper;
using FunkyBank.DTO.Requests;
using FunkyBank.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunkyBank.CustomersApi.Functions
{
    [DependencyInjectionConfig(typeof(ApiBootstrapper))]
    public static class CreateCustomerFunction
    {
        [FunctionName("CreateCustomer")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "customers")] HttpRequest req,
            [Inject]ICustomerService customerService,
            ILogger logger)
        {
            logger.LogInformation($"Calling {nameof(CreateCustomerFunction)}");

            var createCustomerRequest = JsonConvert.DeserializeObject<CreateCustomerRequest>(await new StreamReader(req.Body).ReadToEndAsync());

            var isValid = createCustomerRequest.Validate();

            if (!isValid)
            {
                logger.LogError("Error: Invalid request");
                return new BadRequestObjectResult("Invalid request");
            }

            var operationResult = await customerService.CreateCustomerAsync(createCustomerRequest).ConfigureAwait(false);

            if (operationResult.Status)
            {
                return new OkObjectResult(operationResult.Data);
            }

            return new BadRequestObjectResult(operationResult.Message);
        }
    }
}
