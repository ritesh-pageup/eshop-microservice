using Amazon.Runtime.Internal;
using MediatR;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.API.Provider.Services.Products.CreateProduct
{
    public record CreateProductCommand(
        string Name,
        List<string> Category ,
        string Description,
        string ImageFile,
        decimal Price): IRequest<CreateProductResult>;
    public record CreateProductResult(ObjectId _id);
    internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
    {
        public Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
