using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SlurpStockManagement.Models;

namespace SlurpStockManagement.Interfaces
{
    public interface IOrderRepository
    {
        ActionResult<List<Order>> Get();
        Order CreateOrder(Order order);
    }
}
