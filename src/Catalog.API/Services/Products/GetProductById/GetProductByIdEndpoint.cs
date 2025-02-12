
using Catalog.API.Contract.Models;
using Catalog.API.Dtos;
using MongoDB.Bson;

namespace Catalog.API.Services.Products.GetProductById
{
    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("product/{Id}", async (string Id, ISender sender) =>
            {
                var query = new GetProductQuery(ObjectId.Parse(Id));
                var result = await sender.Send(query);
                var product = result.Product;
                return Results.Ok(product);
            })
            .WithName("GetProduct")
            .Produces<ProductDto>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product!")
            .WithDescription("Get Product!");
        }
    }
}
