using System.IO;
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
    public static class UpdateCustomerFunction
    {
        [FunctionName("UpdateCustomer")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "customers")]
            HttpRequest req,
            [Inject] ICustomerService customerService,
            ILogger logger)
        {
            logger.LogInformation($"Calling {nameof(CreateCustomerFunction)}");

            var updateCustomerRequest = JsonConvert.DeserializeObject<UpdateCustomerRequest>(await new StreamReader(req.Body).ReadToEndAsync());

            var isValid = updateCustomerRequest.Validate();

            if (!isValid)
            {
                logger.LogError("Error: Invalid request");
                return new BadRequestObjectResult("Invalid request");
            }

            var operationResult = await customerService.UpdateCustomerAsync(updateCustomerRequest).ConfigureAwait(false);

            if (operationResult.Status)
            {
                return new OkObjectResult(operationResult.Data);
            }

            return new BadRequestObjectResult(operationResult.Message);
        }
    }
}