// Define a Permission class if you want to have fine-grained control over what each role can do
// example:
// Permission Name: "EditBlogPost"
// Description: "Allows editing of blog posts"
// ActionType: "Update"
// Resource: "BlogPosts"
using MongoDB.Bson.Serialization.Attributes;

namespace ContentCanvas.API.Model
{
    public class Permission : BaseModel
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("actionType")]
        public string ActionType { get; set; }

        [BsonElement("resource")]
        public string Resource { get; set; }
    }
}
