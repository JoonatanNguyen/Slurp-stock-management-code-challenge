using System;
using System.Collections.Generic;
using MongoDB.Driver;
using SlurpStockManagement.Interfaces;
using SlurpStockManagement.Models;

namespace SlurpStockManagement.Repositories
{
    public class CoffeeRepository : ICoffeeRepository
    {
        private readonly IMongoCollection<Coffee> _coffee;

        public CoffeeRepository(ICoffeeDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _coffee = database.GetCollection<Coffee>(settings.CoffeesCollectionName);
        }        

        public Coffee GetCoffeeBySize(int orderSize)
        {
            return _coffee.Find(coffee => coffee.OrderSize == orderSize).FirstOrDefault();
        }

        public Boolean ReserveCoffee(Coffee updatedCoffee)
        {
            return _coffee.ReplaceOne(coffee => coffee.OrderSize == updatedCoffee.OrderSize, updatedCoffee).IsAcknowledged;
        }
    }
}
