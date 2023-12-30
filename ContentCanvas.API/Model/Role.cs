using MongoDB.Bson.Serialization.Attributes;

namespace ContentCanvas.API.Model
{
    public class Role : BaseModel
    {
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("description")]
        public string Description { get; set; }
    }

}
