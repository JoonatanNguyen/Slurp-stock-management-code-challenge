using System.Collections.Generic;
using SlurpStockManagement.Models;

namespace SlurpStockManagement.ViewModels
{
    public class ReserveCoffeeRequest
    {
        public List<CoffeeOrderItem> Order { get; set; }
    }
}
