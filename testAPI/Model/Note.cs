using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace testAPI.Model
{
    [BsonIgnoreExtraElements] 
    public class Note
    {
        public ObjectId InternalId {get;set;}
        public string Id {get;set;}
        public string Body {get;set;} = string.Empty;
        public DateTime UpdatedOn {get; set;} = DateTime.Now;
        public NoteImage HeaderImage {get; set;}
        public int UserId = 0;
    }
}