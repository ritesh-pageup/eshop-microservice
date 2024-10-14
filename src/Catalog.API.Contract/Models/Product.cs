using MongoDB.Bson;

namespace Catalog.API.Contract.Models
{
    public class Product
    {
        public ObjectId _id { get; set; }
        public string Name { get; set; } = default!;
        public List<string> Category { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ImageFile { get; set; } = default!;
        public decimal Price { get; set; }
    }
}
