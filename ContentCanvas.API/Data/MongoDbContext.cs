using ContentCanvas.API.Model;
using MongoDB.Driver;

namespace ContentCanvas.API.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoDb"));
            _database = client.GetDatabase("ContentCanvasDB");
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
        public IMongoCollection<Post> Posts => _database.GetCollection<Post>("Posts");
        public IMongoCollection<Role> Roles => _database.GetCollection<Role>("Roles");
        public IMongoCollection<Permission> Permissions => _database.GetCollection<Permission>("Permissions");
        public IMongoCollection<UserRole> UserRoles => _database.GetCollection<UserRole>("UserRoles");
        public IMongoCollection<RolePermission> RolePermissions => _database.GetCollection<RolePermission>("RolePermissions");

    }
}
