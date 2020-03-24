using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SlurpStockManagement.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public List<CoffeeOrderItem> OrderDetail { get; set; }

        public string OrderedAt { get; set; }

        public int BoxAmount { get; set; }
    }
}
