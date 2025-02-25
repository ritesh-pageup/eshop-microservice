

namespace Ordering.Domain.Models
{
    public class Customer:Entity<CustomerId>
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;

        public static Customer Create(CustomerId customerId, string name, string email, string phone)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(email);
            var customer = new Customer
            {
                Id = customerId,
                Name = name,
                Email = email,
                Phone = phone
            };
            return customer;
        }
    }
}
