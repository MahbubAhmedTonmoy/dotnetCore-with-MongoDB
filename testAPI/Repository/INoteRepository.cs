using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using testAPI.Model;

namespace testAPI.Repository
{
    public interface INoteRepository
    {
         Task<IEnumerable<Note>>GetAllNotes();
         Task<Note> GetNote(string id);
         //query after multiple parametes
         Task<IEnumerable<Note>> GetNote(string bodyText, DateTime updatedFrom, long headerSizeLimit);

         Task AddNote(Note item);
         Task<bool> RemoveNote(string id);

         //update single note
         Task<bool> UpdateNote(string id, string body);
         Task<bool> UpdateNote(string id, Note item);
         Task<bool> RemoveAllNotes();
    }
}