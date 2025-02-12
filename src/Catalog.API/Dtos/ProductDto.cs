using MongoDB.Bson;

namespace Catalog.API.Dtos
{
    public class ProductDto
    {
        public ProductDto(ObjectId _id, string name, List<string> category, string description, string imageFile, decimal price) {
            this._id = _id.ToString();
            this.Name = name;
            this.Category = category;
            this.Description = description;
            this.ImageFile = imageFile;
            this.Price = price;
        }
        public string _id { get; set; }
        public string Name { get; set; } = default!;
        public List<string> Category { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ImageFile { get; set; } = default!;
        public decimal Price { get; set; }

        public ObjectId GetObjectId()
        {
            return ObjectId.Parse(this._id);
        }
    }
}
