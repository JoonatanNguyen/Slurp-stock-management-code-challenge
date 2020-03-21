using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SlurpStockManagement.Models;

namespace SlurpStockManagement.Interfaces
{
    public interface IReserveBoxServices
    {
        ActionResult ReserveBox(List<CoffeeOrderItem> order);
    }
}
