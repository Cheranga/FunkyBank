using FunkyBank.DTO.Models;

namespace FunkyBank.DTO.Responses
{
    public class GetSpecificCustomerResponse
    {
        public CustomerDisplayModel Customer { get; }

        public GetSpecificCustomerResponse(CustomerDisplayModel customer)
        {
            Customer = customer;
        }
    }
}