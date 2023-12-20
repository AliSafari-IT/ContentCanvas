using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ContentCanvas.API.Model
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }

        [BsonRequired]
        public required string Title { get; set; }

        public string? Content { get; set; }

        public bool IsPublic { get; set; }

        public required string UserId { get; set; }
    }
}
