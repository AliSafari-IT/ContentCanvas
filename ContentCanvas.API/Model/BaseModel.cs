using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Security.Principal;

namespace ContentCanvas.API.Model
{
    public abstract class BaseModel
    {
        // MongoDB ObjectId
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        // GUID-based ID
        [BsonElement("idObject")]
        public string IdObject { get; set; }

        [BsonElement("createdOn")]
        public DateTime CreatedOn { get; set; }

        [BsonElement("createdBy")]
        public string? CreatedBy { get; set; }

        [BsonElement("modifiedOn")]
        public DateTime? ModifiedOn { get; set; }

        [BsonElement("modifiedBy")]
        public string? ModifiedBy { get; set; }

        [BsonElement("deletedOn")]
        public DateTime? DeletedOn { get; set; }

        [BsonElement("deletedBy")]
        public string? DeletedBy { get; set; }

        // Flag for soft deletion
        [BsonElement("isDeleted")]
        public bool IsDeleted { get; set; } = false;

        public BaseModel()
        {
            // Initialize Id with a new ObjectId and IdObject with a new GUID string
            Id = ObjectId.GenerateNewId().ToString();
            IdObject = Guid.NewGuid().ToString();
            CreatedOn = DateTime.UtcNow;
        }
    }
}
