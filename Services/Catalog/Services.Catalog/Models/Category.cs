using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Services.Catalog.Models
{
    public class Category
    {
        [BsonId] // MongoDB tarafında ID olarak algılanması için gerekli.
        [BsonRepresentation(BsonType.ObjectId)] // ID'nin tipi için gerekli. (string)
        public string Id{ get; set; }
        public string Name { get; set; }      
    }
}
