using FunkyBank.DTO.Models;

namespace FunkyBank.DTO.Responses
{
    public class UpdateCustomerResponse
    {
        public CustomerDisplayModel Customer { get; }

        public UpdateCustomerResponse(CustomerDisplayModel customer)
        {
            Customer = customer;
        }
    }
}