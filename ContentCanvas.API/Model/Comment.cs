using MongoDB.Bson.Serialization.Attributes;

namespace ContentCanvas.API.Model
{
    public class Comment : BaseModel
    {
        [BsonElement("content")]
        public string Content { get; set; }

        [BsonElement("authorId")]
        public string AuthorId { get; set; }
    }
}
