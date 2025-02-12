using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MongoDB.Bson;

namespace Catalog.API.Services.Products.CreateProduct
{
    public record CreateProductRequest(string Name,
        List<string> Category,
        string Description,
        string ImageFile,
        decimal Price);
    public record CreateProductResponse(ObjectId _id);
    public class CreateProductEndpoint : CarterModule
    {
        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("product", async (CreateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductCommand>();
                var result = sender.Send(command);

                var response = result.Result.Adapt<CreateProductResponse>();
                return Results.Created($"/product/{response._id}", response);
            })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Product")
            .WithDescription("Create Product"); 
        }
    }
}
