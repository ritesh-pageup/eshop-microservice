
using BuildingBlock.CQRS;
using FluentValidation;
using global::Catalog.API.Database;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace Catalog.API.Services.Products.UpdateProduct
{
    public record UpdateProductCommand(string _id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<bool>;

    public class UpdateProductCommandHandler(CatalogDbContext dbContext) : ICommandHandler<UpdateProductCommand, bool>
    {
            

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            //var validationResult = await validator.ValidateAsync(request,cancellationToken);
            //var errors = validationResult.Errors;
            //if (errors.Any())
            //{
            //    throw new ValidationException(errors);
            //}
            ObjectId objId = ObjectId.Parse(request._id);
            var product = await dbContext.Products.FirstOrDefaultAsync(p => p._id == objId, cancellationToken);
            if (product == null)
            {
                return false; // Product not found
            }

            // Update product properties
            product.Name = request.Name;
            product.Category = request.Category;
            product.Description = request.Description;
            product.ImageFile = request.ImageFile;
            product.Price = request.Price;

            await dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
