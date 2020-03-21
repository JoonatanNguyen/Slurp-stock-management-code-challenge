using System;
using System.Collections.Generic;
using SlurpStockManagement.Models;

namespace SlurpStockManagement.Interfaces
{
    public interface IReserveBoxServices
    {
        void ReserveBox(List<CoffeeOrderItem> order);
    }
}
