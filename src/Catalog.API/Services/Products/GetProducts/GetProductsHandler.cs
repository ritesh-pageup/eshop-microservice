using BuildingBlock.CQRS;
using Catalog.API.Contract.Models;
using Catalog.API.Database;
using Catalog.API.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Services.Products.GetProducts;
public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductsResult>;
public record GetProductsResult(IEnumerable<ProductDto> Products);

public class GetProductsQueryHandler
        : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
    private readonly CatalogDbContext context;

    public GetProductsQueryHandler(CatalogDbContext context)
    {
        this.context = context;
    }
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = (await context.Products.Skip(((query.PageNumber ?? 1) - 1) * (query.PageSize ?? 10)).ToListAsync()).Select(x => new ProductDto(x._id, x.Name, x.Category, x.Description, x.ImageFile, x.Price));
        
        return new GetProductsResult(products);
    }
}

