using Catalog.API.Services.Products.UpdateProduct;
using FluentValidation;

namespace Catalog.API.Validators
{
    public class UpdateProductValidator: AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required!");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required!");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Image file is required!");
        }
    }
}
