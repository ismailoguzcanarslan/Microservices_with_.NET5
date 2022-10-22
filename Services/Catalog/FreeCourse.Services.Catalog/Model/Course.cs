using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;

namespace FreeCourse.Services.Catalog.Model
{
    public class Course
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }
        public string Picture { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedOn { get; set; }
        public string UserId { get; set; }
        public Feature Feature { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; }
        public string Description { get; set; }

        //MongoDb tarafında collectionlar oluşurken bunu eklememesi için kullanılan annotation. Kursları ile beraber kategorileri de dönmek istediğimiz için kullanıyoruz.
        [BsonIgnore]
        public Category Category { get; set; }
    }
}
