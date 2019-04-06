using System.Collections.Generic;
using System.Linq;
using FunkyBank.DTO.Models;

namespace FunkyBank.DTO.Responses
{
    public class GetCustomersResponse
    {
        public List<CustomerDisplayModel> Customers { get; }

        public GetCustomersResponse(IEnumerable<CustomerDisplayModel> customers)
        {
            Customers = customers?.ToList() ?? new List<CustomerDisplayModel>();
        }
    }
}