using Carter;
using Catalog.API.Dtos;
using Catalog.API.Services.Products.UpdateProduct;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Services.Products.UpdateProduct
{
    public class UpdateProductEndpoints : ICarterModule
    {
        public record UpdateProductRequest(ProductDto Product);
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("Product/Update", async (UpdateProductCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);
                if (!result)
                {
                    return Results.NotFound("Product not found.");
                }
                return Results.Ok("Product updated successfully.");
            })
            .WithName("UpdateProduct")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Update Product")
            .WithDescription("Updates an existing product based on the provided details.");
        }
    }
}
