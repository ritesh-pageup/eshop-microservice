namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery (string UserName): IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCart Cart);
    public class GetBasketQueryHandler(IBasketService basketService) : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            var basket = await basketService.GetBasket(request.UserName, cancellationToken);
            return new GetBasketResult(basket);
        }
    }
}
