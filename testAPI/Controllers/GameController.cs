using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using testAPI.Model;
using testAPI.Repository;

namespace testAPI.Controllers
{
    [Produces("application/json")]
     [Route("api/[controller]")]
    public class GameController: Controller
    {
        private readonly IGameRepository _repo;
        public GameController(IGameRepository repo)
        {
            _repo = repo;
        }   

        [HttpGet("all")]
        public async Task<IActionResult> Get()
        {
            var result = await _repo.GetAllGames();
            return new ObjectResult(result);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var result = await _repo.GetGame(name);
            if (result == null) return NotFound();
            return new ObjectResult(result);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Game game)
        {
            await _repo.Create(game);
            return Ok("created");
        }
        [HttpPut("{name}")]
        public async Task<IActionResult> Put(string name, [FromBody]Game game)
        {
            var result =await _repo.GetGame(name);
            if(result == null) return NotFound();
            game.Id = result.Id;
            await _repo.Update(result);
            return Ok("updated");
        }

            [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            var gameFromDb = await _repo.GetGame(name);
            if (gameFromDb == null)
                return new NotFoundResult();
            await _repo.Delete(name);
            return Ok("deleted");
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("working");
        }
    }
}