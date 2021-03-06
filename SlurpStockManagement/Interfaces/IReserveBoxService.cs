﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SlurpStockManagement.Models;

namespace SlurpStockManagement.Interfaces
{
    public interface IReserveBoxService
    {
        ActionResult<int> ReserveBox(List<CoffeeOrderItem> order);
        ActionResult<Box> GetBoxInStock();
    }
}
