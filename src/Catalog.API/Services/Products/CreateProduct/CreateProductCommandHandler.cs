using BuildingBlock.CQRS;
using Catalog.API.Contract.Models;
using Catalog.API.Database;
using FluentValidation;
using MongoDB.Bson;

namespace Catalog.API.Services.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
: ICommand<CreateProductResult>;
public record CreateProductResult(ObjectId _id);


public class CreateProductCommandHandler(CatalogDbContext context) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        //var validationResult = await validator.ValidateAsync(command, cancellationToken);
        //var errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
        //if (errors.Any())
        //{
        //    throw new ValidationException(errors.FirstOrDefault());
        //}           

        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };



        context.Products.Add(product);
        await context.SaveChangesAsync();

        //return result
        return new CreateProductResult(product._id);
    }
}
