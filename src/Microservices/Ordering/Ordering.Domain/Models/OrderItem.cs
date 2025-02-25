

namespace Ordering.Domain.Models
{
    public class OrderItem: Entity<OrderItemId>
    {
        internal OrderItem(OrderId orderId, string productId, decimal price, int quantity) {
            Id = OrderItemId.Of(Guid.NewGuid());
            this.OrderId = orderId; 
            this.ProductId = productId;
            this.Price = price;
            this.Quantity = quantity;
        }
        public OrderId OrderId { get; set; }
        public string? ProductId { get; set; }
        public decimal Price { get; set; }  
        public int Quantity { get; set; }
    }
}
