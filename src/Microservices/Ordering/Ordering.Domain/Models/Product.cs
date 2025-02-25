

namespace Ordering.Domain.Models
{
    public class Product: Entity<int>
    {
        public string ProductId { get; set; }
        public string Name {  get; set; }
        public decimal Price { get; set; }

        public static Product Create(int id, string name, decimal price, string productId)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            var product = new Product
            {
                Id = id,
                Name = name,
                Price = price,
                ProductId = productId
            };

            return product;
        }
    }
}
