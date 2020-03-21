using System;
using System.Collections.Generic;
using SlurpStockManagement.Models;

namespace SlurpStockManagement.Interfaces
{
    public interface ICoffeeRepository
    {
        Coffee GetCoffeeBySize(int orderSize);
        Boolean ReserveCoffee(Coffee updatedCoffee);
    }
}
