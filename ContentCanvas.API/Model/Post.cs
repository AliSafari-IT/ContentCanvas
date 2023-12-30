using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace ContentCanvas.API.Model
{
    public class Post : BaseModel
    {
        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("content")]
        public string? Content { get; set; }

        [BsonElement("isPublic")]
        public bool IsPublic { get; set; }

        [BsonElement("isPublished")]
        public bool IsPublished { get; set; }

        [BsonElement("publishedOn")]
        public DateTime? PublishedOn { get; set; }

        [BsonElement("publishedBy")]
        public string PublishedBy { get; set; }

        [BsonElement("comments")]
        public List<Comment> Comments { get; set; }

        public Post()
        {
            Comments = new List<Comment>();
        }
    }
}
