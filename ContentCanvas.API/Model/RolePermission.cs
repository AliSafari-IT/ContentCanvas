using MongoDB.Bson.Serialization.Attributes;
using System.Security.Principal;

namespace ContentCanvas.API.Model
{
    public class RolePermission : BaseModel
    {
        [BsonElement("roleIdObject")]
        public string RoleIdObject { get; set; }

        [BsonElement("permissionIdObject")]
        public string PermissionIdObject { get; set; }

        public RolePermission()
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
