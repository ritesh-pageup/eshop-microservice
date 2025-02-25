using Basket.API.Basket.DeleteBasket;
using FluentValidation;

namespace Basket.API.Validators
{
    public class DeleteBasketValidator:AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User name must required!");
        }
    }
}
