using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AuthorizationAPI.Models
{
    public class User
    {
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
