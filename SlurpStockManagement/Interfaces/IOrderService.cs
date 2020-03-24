using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SlurpStockManagement.Models;

namespace SlurpStockManagement.Interfaces
{
    public interface IOrderService
    {
        ActionResult<List<Order>> GetOrderList();

        void CreateOrder(List<CoffeeOrderItem> order, int boxAmount);
    }
}
