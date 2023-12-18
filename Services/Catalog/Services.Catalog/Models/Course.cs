using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;

namespace Services.Catalog.Models
{
    public class Course
    {
        [BsonId] // MongoDB tarafında ID olarak algılanması için gerekli.
        [BsonRepresentation(BsonType.ObjectId)] // ID'nin tipi için gerekli. (string)
        public string Id { get; set; }
        public string Name { get; set; }
        [BsonRepresentation(BsonType.Decimal128)] 
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string Picture { get; set; }
        [BsonRepresentation(BsonType.DateTime)] 
        public DateTime CreatedTime { get; set; }
        public Feature Feature { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; }
        [BsonIgnore] // MongoDB tarafında karşılığı olmacak ben kodlama yaparken kullanacağım.
        public Category Category { get; set; }

    }
}
