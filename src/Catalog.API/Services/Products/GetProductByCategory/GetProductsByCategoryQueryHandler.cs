using BuildingBlock.CQRS;
using Catalog.API.Database;
using Catalog.API.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Services.Products.GetProductsByCategory
{
    public record GetProductsByCategoryQuery(string categoryName) : IQuery<GetProductsByCategoryResult>;
    public record GetProductsByCategoryResult(IEnumerable<ProductDto> Products);
    public class GetProductsByCategoryQueryHandler : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
    {
        private readonly CatalogDbContext dbContext;

        public GetProductsByCategoryQueryHandler(CatalogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var products = (await dbContext.Products.Where(x => x.Category.Contains(request.categoryName)).ToListAsync()).Select(x => new ProductDto(x._id, x.Name, x.Category, x.Description, x.ImageFile, x.Price));

            return new GetProductsByCategoryResult(products);
        }
    }
}
