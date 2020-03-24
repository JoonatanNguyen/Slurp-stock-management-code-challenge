using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SlurpStockManagement.Interfaces;
using SlurpStockManagement.Models;

namespace SlurpStockManagement.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public ActionResult<List<Order>> GetOrderList() => _orderRepository.Get();

        public void CreateOrder(List<CoffeeOrderItem> order, int boxAmount)
        {
            _orderRepository.CreateOrder(new Order
            {
                OrderedAt = DateTimeOffset.Now.ToString(),
                BoxAmount = boxAmount,
                OrderDetail = order
            });
        }
    }
}
