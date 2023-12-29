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

    }
}
