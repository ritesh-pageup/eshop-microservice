
namespace Ordering.Domain.ValueObjects
{
    public record Payment
    {
        public string? CardName { get; set; }
        public string? CardNumber { get; set; }
        public string? Expiration { get; set; }
        public string CVV { get; set; } = default!;
        public int PaymentMethod { get; set; }
    }
}
