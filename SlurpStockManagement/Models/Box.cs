using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SlurpStockManagement.Models
{
    public class Box
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public int Available { get; set; }

        public int Reserved { get; set; }

        public int Pending { get; set; }
    }
}
