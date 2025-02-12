using BuildingBlock.CQRS;
using Catalog.API.Database;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace Catalog.API.Services.Products.DeleteProduct
{
    public record DeleteProductCommand(string Id) : ICommand<bool>;

    public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, bool>
    {
        private readonly CatalogDbContext dbContext;

        public DeleteProductCommandHandler(CatalogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            ObjectId objId = ObjectId.Parse(request.Id);
            var product = await dbContext.Products.FirstOrDefaultAsync(p => p._id == objId, cancellationToken);
            if (product == null)
            {
                return false; // Product not found
            }

            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
