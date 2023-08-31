using AuthorizationAPI.Models;
using MongoDB.Driver;

namespace AuthorizationAPI.Repositories
{
    public class UserRepository
    {
        private readonly string  _connString;
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<User> userCollection;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;

            _connString = _configuration.GetConnectionString("conn_string")!;

            MongoClient client = new MongoClient(_connString);

            IMongoDatabase db = client.GetDatabase("Scrumboard");

            userCollection = db.GetCollection<User>("Users");
        }
        
        public async Task<User> GetUserByNameAsync(string name)
        {
            var filter = Builders<User>.Filter.Eq("name", name);
            return await userCollection.Find(filter).FirstOrDefaultAsync();
        }
    }
}
