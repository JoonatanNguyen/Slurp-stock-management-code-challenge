using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SlurpStockManagement.Models;

namespace SlurpStockManagement.Interfaces
{
    public interface IReserveCoffeeService
    {
        ActionResult ReserveCoffee(List<CoffeeOrderItem> order);
        ActionResult<List<Coffee>> GetCoffeeInStock();
    }
}
