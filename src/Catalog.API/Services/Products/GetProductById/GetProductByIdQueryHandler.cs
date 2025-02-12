using BuildingBlock.CQRS;
using Catalog.API.Contract.Models;
using Catalog.API.Database;
using Catalog.API.Dtos;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace Catalog.API.Services.Products.GetProductById
{

    public record GetProductQuery(ObjectId Id) : IQuery<GetProductResult>;
    public record GetProductResult(ProductDto Product);
    public class GetProductByIdQueryHandler : IQueryHandler<GetProductQuery, GetProductResult>
    {
        private readonly CatalogDbContext dbContext;

        public GetProductByIdQueryHandler(CatalogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<GetProductResult> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var productdb = await dbContext.Products.FirstOrDefaultAsync(x => x._id == request.Id);
            if (productdb == null) {
                throw new Exception("Product not found!");
            }
            var product = new ProductDto(productdb._id, productdb.Name, productdb.Category, productdb.Description, productdb.ImageFile, productdb.Price);
            return new GetProductResult(product);   
        }
    }
}
