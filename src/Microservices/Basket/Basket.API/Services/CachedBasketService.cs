
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Services
{
    public class CachedBasketService(IBasketService basketService, IDistributedCache cache) : IBasketService
    {
        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            var response = await basketService.DeleteBasket(userName, cancellationToken);
            await cache.RemoveAsync(userName);
            return response;
        }

        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            var cachedBasket = await cache.GetStringAsync(userName);
            if (!string.IsNullOrEmpty(cachedBasket)) {
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket);
            }
            var basket = await basketService.GetBasket(userName, cancellationToken);
            await cache.SetStringAsync(userName, JsonSerializer.Serialize<ShoppingCart>(basket), cancellationToken);
            return basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken cancellationToken = default)
        {
            var basket = await basketService.StoreBasket(cart, cancellationToken);
            await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize<ShoppingCart>(basket), cancellationToken);
            return basket;
        }
    }
}
