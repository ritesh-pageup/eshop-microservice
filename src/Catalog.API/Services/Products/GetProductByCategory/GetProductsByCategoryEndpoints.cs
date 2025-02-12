
using Catalog.API.Dtos;
using Catalog.API.Services.Products.GetProducts;
using Catalog.API.Services.Products.GetProductsByCategory;

namespace Catalog.API.Services.Products.GetProductByCategory
{
    public record GetProductsByCategoryResponse(IEnumerable<ProductDto> Products);
    public class GetProductsByCategoryEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("ProductByCategory/{Category}", async (string Category, ISender sender) =>
            {
                var query = new GetProductsByCategoryQuery(Category);
                var result = await sender.Send(query);
                var response = new GetProductsByCategoryResponse(result.Products);
                return Results.Ok(response);
            })
            .WithName("GetProductsByCategory")
            .Produces<GetProductsByCategoryResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products by Category!")
            .WithDescription("Get Products by category!");
        }
    }
}
