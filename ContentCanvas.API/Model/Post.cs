using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ContentCanvas.API.Model
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string IdObject { get; set; }

        public string Title { get; set; }

        public string? Content { get; set; }

        public bool IsPublic { get; set; }

        public string UserId { get; set; }

        public Post()
        {
            IdObject = Guid.NewGuid().ToString();
        }
    }
}
