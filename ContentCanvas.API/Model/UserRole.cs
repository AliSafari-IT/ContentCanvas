using ContentCanvas.API.Model;
using MongoDB.Bson.Serialization.Attributes;
using System.Security.Principal;

namespace ContentCanvas.API.Model
{
    public class UserRole : BaseModel
    {
        [BsonElement("userIdObject")]
        public string UserIdObject { get; set; }
        [BsonElement("roleIdObject")]
        public string RoleIdObject { get; set; }
        public UserRole()
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
