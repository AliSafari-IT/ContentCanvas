using MongoDB.Bson.Serialization.Attributes;
using System.Security.Principal;

namespace ContentCanvas.API.Model
{
    public class Role : BaseModel
    {
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("description")]
        public string Description { get; set; }


        public Role()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();

            if (identity != null)
            {
                var chunks = identity.Name.Split("\\");
                CreatedBy = chunks.Length > 0 ? chunks[^1] : identity.Name;
            }
            else
            {
                CreatedBy = "_";
            }
        }
    }

}
