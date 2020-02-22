using MongoDB.Driver;
using testAPI.Model;

namespace testAPI.DBContext
{
    public interface IGameContext
    {
         IMongoCollection<Game> Games {get;}
    }
}