using System.Collections.Generic;
using System.Threading.Tasks;
using testAPI.Model;

namespace testAPI.Repository
{
    public interface IGameRepository
    {
         Task<IEnumerable<Game>> GetAllGames();
         Task<Game> GetGame(string name);
         Task Create(Game game);

         Task<bool> Update(Game game);
         Task<bool> Delete(string name);
    }
}