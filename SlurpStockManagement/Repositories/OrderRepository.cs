using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SlurpStockManagement.Interfaces;
using SlurpStockManagement.Models;

namespace SlurpStockManagement.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _orderList;

        public OrderRepository(IOrderListDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _orderList = database.GetCollection<Order>(settings.OrderListCollectionName);
        }

        public ActionResult<List<Order>> Get() => _orderList.Find(order => true).ToList();

        public Order CreateOrder(Order order)
        {
            _orderList.InsertOne(order);
            return order;
        }

    }
}
