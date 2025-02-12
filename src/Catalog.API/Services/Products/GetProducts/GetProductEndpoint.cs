using BuildingBlock.CQRS;
using Catalog.API.Contract.Models;
using Catalog.API.Dtos;

namespace Catalog.API.Services.Products.GetProducts;
public record GetProductsResponse(IEnumerable<ProductDto> Products);
public record GetProductsRequest(int? PageNumber, int? PageSize);
public class GetProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("products", async (GetProductsRequest request, ISender sender) =>
        {
            var command = request.Adapt<GetProductsQuery>();
            var result = await sender.Send(command);  
            var response = result.Products.Adapt<GetProductsResponse>();
            return Results.Ok(result);
        })
            .WithName("GetProducts")
            .Produces<GetProductsResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products")
            .WithDescription("Get Products");
    }
}
