
using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart): ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);
    public class StoreBasketCommandHandler(IBasketService basketService, DiscountProtoService.DiscountProtoServiceClient dicountClient) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
        {
            await DeductDiscount(request.Cart, cancellationToken);
            await basketService.StoreBasket(request.Cart, cancellationToken);
            return new StoreBasketResult(request.Cart.UserName);
        }
        private async Task DeductDiscount(ShoppingCart cart , CancellationToken cancellationToken)
        {
            foreach (var x in cart.Items)
            {
                var coupon = await dicountClient.GetDiscountAsync(new GetDiscountRequest { ProductName = x.ProductName}, cancellationToken:cancellationToken);
                x.Price -= (decimal)coupon.Amount;
            }
        }
    }
}
