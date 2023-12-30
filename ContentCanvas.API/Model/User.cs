using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Security.Principal;

namespace ContentCanvas.API.Model
{
    public class User : BaseModel
    {
        [BsonElement("firstName")]
        public string FirstName { get; set; }

        [BsonElement("lastName")]
        public string? LastName { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("winUserName")]
        public string? WinUserName { get; set; }

        [BsonElement("authenticationType")]
        public string? AuthenticationType { get; set; }

        [BsonElement("isAuthenticated")]
        public bool? IsAuthenticated { get; set; }

        [BsonElement("isGuest")]
        public bool? IsGuest { get; set; }

        [BsonElement("isSystem")]
        public bool? IsSystem { get; set; }

        public User()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            if (identity != null)
            {
                var userNameParts = identity.Name.Split("\\");
                WinUserName = userNameParts.Length > 0 ? userNameParts[^1] : identity.Name;
                CreatedBy = identity.Name;
                AuthenticationType = identity.AuthenticationType;
                IsAuthenticated = identity.IsAuthenticated;
                IsGuest = identity.IsGuest;
                IsSystem = identity.IsSystem;
            }
        }
    }
}
