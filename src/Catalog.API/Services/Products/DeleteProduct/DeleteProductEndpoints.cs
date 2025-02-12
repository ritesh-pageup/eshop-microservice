using Carter;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Services.Products.DeleteProduct
{
    public class DeleteProductEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("Product/Delete/{id}", async (string id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductCommand(id));
                if (!result)
                {
                    return Results.NotFound("Product not found.");
                }
                return Results.Ok("Product deleted successfully.");
            })
            .WithName("DeleteProduct")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Delete Product")
            .WithDescription("Deletes an existing product based on the provided ID.");
        }
    }
}
