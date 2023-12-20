using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ContentCanvas.API.Model
{
    public class User
    {
        // This property represents the MongoDB ObjectId.
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }

        // Other properties as before
        [BsonRequired]
        public required string Username { get; set; }

        [BsonRequired]
        public required string Email { get; set; }

        [BsonRequired]
        public required string Password { get; set; }

        public string? Role { get; set; }
    }
}
