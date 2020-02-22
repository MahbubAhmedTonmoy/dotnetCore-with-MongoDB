using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using testAPI.DBContext;
using testAPI.Model;

namespace testAPI.Repository
{
    public class NoteRepository : INoteRepository
    {
        private readonly DbContext _context;

        public NoteRepository(DbContext context)
        {
            _context = context;
        }
        public async Task AddNote(Note item)
        {
            await _context.Notes.InsertOneAsync(item);
        }

        public async Task<IEnumerable<Note>> GetAllNotes()
        {
            return await _context.Notes.Find(_ => true).ToListAsync();
        }

        public async Task<Note> GetNote(string id)
        {
            ObjectId internalId = GetInternalId(id);
            return await _context.Notes.Find(n => n.Id == id || n.InternalId == internalId).FirstOrDefaultAsync();
        }

        private ObjectId GetInternalId(string id)
        {
            ObjectId internalId;
            if(!ObjectId.TryParse(id, out internalId))
                internalId = ObjectId.Empty;
            return internalId;
        }

        public async Task<IEnumerable<Note>> GetNote(string bodyText, DateTime updatedFrom, long headerSizeLimit)
        {
            var query = _context.Notes.Find(n => n.Body.Contains(bodyText) &&
                                            n.UpdatedOn >= updatedFrom &&
                                            n.HeaderImage.ImageSize <= headerSizeLimit);
            return await query.ToListAsync();
        }

        public async Task<bool> RemoveAllNotes()
        {
            DeleteResult delete=  await _context.Notes.DeleteManyAsync(new BsonDocument());
            return  delete.IsAcknowledged && delete.DeletedCount >0;
        }

        public async Task<bool> RemoveNote(string id)
        {
            DeleteResult delete = await _context.Notes.DeleteOneAsync(Builders<Note>.Filter.Eq("Id", id));
            return  delete.IsAcknowledged && delete.DeletedCount >0;
        }

        public async Task<bool> UpdateNote(string id, string body)
        {
            var filter = Builders<Note>.Filter.Eq(s => s.Id == id);
            var update = Builders<Note>.Update.Set(s => s.Body, body);
            
            UpdateResult result = await _context.Notes.UpdateOneAsync(filter, update);

            return result.IsAcknowledged && result.ModifiedCount>0;
        }

        public async Task<bool> UpdateNote(string id, Note item)
        {
            ReplaceOneResult replace = await _context.Notes.ReplaceOneAsync(
                filter: n => n.Id == id, replacement: item
            );
            return replace.IsAcknowledged && replace.ModifiedCount >0;
        }
    }
}