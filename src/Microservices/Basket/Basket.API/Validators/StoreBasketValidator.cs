using Basket.API.Basket.StoreBasket;
using FluentValidation;

namespace Basket.API.Validators
{
    public class StoreBasketValidator:AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null.");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("User name is required.");
        }
    }
}
