using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ContentCanvas.API.Model
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } // MongoDB ObjectId

        [BsonRequired]
        public string Title { get; set; }

        public string? Content { get; set; }

        public bool IsPublic { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } // Assuming this is a reference to a User document
    }
}
