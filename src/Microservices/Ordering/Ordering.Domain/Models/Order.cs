namespace Ordering.Domain.Models
{
    public class Order: Aggregate<OrderId>
    {
        private readonly List<OrderItem> _orderItems = new();
        public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
        public CustomerId CustomerId { get; set; }
        public OrderName OrderName {  get; set; }  
        public Address ShippingAddress {  get; set; }
        public Address BillingAddress {  get; set; }
        public Payment Payment { get; set; }
        public OrderStatus Status {  get; set; }   
        public decimal TotalPrice { get => _orderItems.Sum(x => x.Price * x.Quantity); private set { } }

        public static Order Create(OrderId id, CustomerId customerId, OrderName orderName, Address shippingAddress, Address billingAddress, Payment payment)
        {
            var order = new Order
            {
                Id = id,
                CustomerId = customerId,
                OrderName = orderName,
                ShippingAddress = shippingAddress,
                BillingAddress = billingAddress,
                Payment = payment,
                Status = OrderStatus.Pending
            };

            //order.AddDomainEvent(new OrderCreatedEvent(order));

            return order;
        }
    }
}
