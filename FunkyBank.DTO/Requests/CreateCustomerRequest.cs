using FunkyBank.Core;

namespace FunkyBank.DTO.Requests
{
    public class CreateCustomerRequest : IValidatable
    {
        public CreateCustomerRequest(string name, string address)
        {
            Name = name;
            Address = address;
        }

        public string Name { get; }
        public string Address { get; }

        //
        // NOTE: Add application specific validations in here
        //
        public bool IsValid() => !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Address);
    }
}