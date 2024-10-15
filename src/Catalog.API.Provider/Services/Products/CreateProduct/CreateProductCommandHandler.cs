using Amazon.Runtime.Internal;
using BuildingBlock.CQRS;
using Catalog.API.Contract.Models;
using Catalog.API.Provider.Database;
using MediatR;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.API.Provider.Services.Products.CreateProduct
{
    public record CreateProductCommand(
        string Name,
        List<string> Category ,
        string Description,
        string ImageFile,
        decimal Price): ICommand<CreateProductResult>;
    public record CreateProductResult(ObjectId _id);
    internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        private readonly ConnectionHelper connectionHelper;

        public CreateProductCommandHandler(ConnectionHelper connectionHelper)
        {
            this.connectionHelper = connectionHelper;
        }

        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //using (var context = CatalogDbContext.Create(this.connectionHelper))
            //{
                Product product = new Product()
                {
                    Category = command.Category,
                    ImageFile = command.ImageFile,
                    Description = command.Description,
                    Price = command.Price,
                    Name = command.Name,
                };

            return new CreateProductResult(new ObjectId());
                //context.Products.Add(product); 
                //await context.SaveChangesAsync();   
            //}

        }
    }
}
