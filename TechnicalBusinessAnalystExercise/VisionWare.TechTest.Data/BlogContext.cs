using System.Configuration;
using MongoDB.Driver;
using VisionWare.TechTest.Common.Models;

namespace VisionWare.TechTest.Data
{
    public class BlogContext
    {
        public const string CONNECTION_STRING_NAME = "Blog";
        public const string DATABASE_NAME = "blog";
        public const string POSTS_COLLECTION_NAME = "posts";
        public const string USERS_COLLECTION_NAME = "users";

        // This is ok... Normally, they would be put into
        // an IoC container.
        private static readonly IMongoClient _client;
        private static readonly IMongoDatabase _database;

        static BlogContext()
        {
            var connectionString = ConfigurationManager.ConnectionStrings[CONNECTION_STRING_NAME].ConnectionString;
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(DATABASE_NAME);
        }

        public IMongoClient Client => _client;

        public IMongoDatabase Database => _database;

        public IMongoCollection<Post> Posts => _database.GetCollection<Post>(POSTS_COLLECTION_NAME);

        public IMongoCollection<User> Users => _database.GetCollection<User>(USERS_COLLECTION_NAME);
    }
}