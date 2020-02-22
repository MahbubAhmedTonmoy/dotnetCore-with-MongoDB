using MongoDB.Driver;
using testAPI.Model;

namespace testAPI.DBContext
{
    public interface IDbContext
    {
         IMongoCollection<Game> Games {get;}
    }
}