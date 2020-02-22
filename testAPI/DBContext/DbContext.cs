using Microsoft.Extensions.Options;
using MongoDB.Driver;
using testAPI.connectionClass;
using testAPI.Model;

namespace testAPI.DBContext
{
    public class DbContext : IDbContext
    {
        private readonly IMongoDatabase _db;

        public DbContext(IOptions<Settings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.Database);
        }
        public IMongoCollection<Game> Games => _db.GetCollection<Game>("Games");

        public IMongoCollection<Note> Notes 
        {
            get{
                return _db.GetCollection<Note>("Notes");
            }
        }
    }
}