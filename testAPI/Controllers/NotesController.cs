using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using testAPI.Model;
using testAPI.Repository;

namespace testAPI.Controllers
{
    [Route("api/[controller]")]
    public class NotesController: Controller
    {
        private readonly INoteRepository _repo;

        public NotesController(INoteRepository repo)
        {
            _repo = repo;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _repo.GetAllNotes();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _repo.GetNote(id);
            return Ok(result);
        }
        [HttpGet("{bodyText}/{updatedFrom}/{headerSizeLimit}")]
        public async Task<IActionResult> Get(string bodyText, 
                                             DateTime updatedFrom, 
                                             long headerSizeLimit)
        {
            var result = await _repo.GetNote(bodyText, updatedFrom, headerSizeLimit);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Note note)
        {
            await _repo.AddNote(note);
            return Ok("created");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id,[FromBody] string body)
        {
            await _repo.UpdateNote(id,body);
            return Ok("updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _repo.RemoveNote(id);
            return Ok("Deleted");
        }
    }
}