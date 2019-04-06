namespace FunkyBank.DTO.Models
{
    public class CustomerDisplayModel
    {
        public string Name { get; }
        public string Address { get; }

        public CustomerDisplayModel(string name, string address)
        {
            Name = name;
            Address = address;
        }
    }
}