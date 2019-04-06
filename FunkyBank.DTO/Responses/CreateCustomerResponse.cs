using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using FunkyBank.DTO.Models;

namespace FunkyBank.DTO.Responses
{
    public class CreateCustomerResponse
    {
        public CustomerDisplayModel Customer { get; }

        public CreateCustomerResponse(CustomerDisplayModel customer)
        {
            Customer = customer;
        }
    }
}
