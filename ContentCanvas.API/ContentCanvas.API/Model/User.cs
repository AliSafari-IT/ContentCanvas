using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ContentCanvas.API.Model
{
    public class User
    {
        // This property represents the MongoDB ObjectId.
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }  // MongoDB ObjectId

        // Other properties
        [BsonRequired]
        public string Username { get; set; }

        [BsonRequired]
        public string Email { get; set; }

        [BsonRequired]
        public string Password { get; set; }

        public string? Role { get; set; }  // Nullable property for user role
    }
}
